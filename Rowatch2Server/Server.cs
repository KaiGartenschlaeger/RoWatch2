using System;
using System.Collections.Generic;
using System.Threading;
using ServerRessources;
using SimpleNetwork;
using Tools;

namespace RoWatch2Server
{
    public class Server
    {
        #region Fields

        private TCPServer _server;
        private Dictionary<long, User> _connections;

        #endregion

        #region Methods

        public void Run()
        {
            _connections = new Dictionary<long, User>(100);

            _server = new TCPServer();
            _server.Start(ServerSettings.Port);

            Thread thread = new Thread(ListeningThread);
            thread.Start();
        }

        private void ListeningThread()
        {
            NetworkMessage message;

            do
            {
                message = _server.GetMessage();
                switch (message.Type)
                {
                    case NetworkMessageType.ServerStarted:
                        Console.WriteLine("Server started");
                        break;
                    case NetworkMessageType.ServerStoped:
                        Console.WriteLine("Server stopped");
                        break;

                    case NetworkMessageType.Connected:
                        Console.WriteLine("Client {0} connected.", message.EndPoint);
                        if (!_connections.ContainsKey(message.Client))
                        {
                            _connections.Add(message.Client, new User());
                        }
                        break;

                    case NetworkMessageType.Disconnected:
                        Console.WriteLine("Client {0} disconnected", message.EndPoint);
                        if (_connections.ContainsKey(message.Client))
                        {
                            SendConnectionClosed(message.Client);
                            _connections.Remove(message.Client);
                        }
                        break;

                    case NetworkMessageType.FormatedMessage:
                        HandleMessage((ClientMessageId)message.ID, message);
                        break;
                }
            } while (message.Type != NetworkMessageType.ExitRequest);
        }





        private void HandleMessage(ClientMessageId messageId, NetworkMessage message)
        {
            if (_connections.ContainsKey(message.Client))
            {
                User user = _connections[message.Client];

                switch (messageId)
                {
                    case ClientMessageId.Connected:
                        HandleMessage_Connected(user, message);
                        break;
                    case ClientMessageId.PropertieChanged:
                        HandleMessage_PropertieChanged(user, message);
                        break;
                    case ClientMessageId.Disconnected:
                        HandleMessage_Disconnected(user, message);
                        break;
                }
            }
        }

        private void HandleMessage_Connected(User user, NetworkMessage message)
        {
            if (message.Parameter.Length == 15)
            {
                user.Name = message.Parameter[0];
                user.Map = message.Parameter[1];

                user.BaseLevel = StringHelper.ParseInt(message.Parameter[2], 0);
                user.JobLevel = StringHelper.ParseInt(message.Parameter[3], 0);

                user.HP = StringHelper.ParseInt(message.Parameter[4], 0);
                user.MaxHP = StringHelper.ParseInt(message.Parameter[5], 0);

                user.SP = StringHelper.ParseInt(message.Parameter[6], 0);
                user.MaxSP = StringHelper.ParseInt(message.Parameter[7], 0);

                user.BaseEXP = StringHelper.ParseInt(message.Parameter[8], 0);
                user.RequiredBaseEXP = StringHelper.ParseInt(message.Parameter[9], 0);
                user.BaseExpPerHour = StringHelper.ParseInt(message.Parameter[10], 0);

                user.JobEXP = StringHelper.ParseInt(message.Parameter[11], 0);
                user.RequiredJobEXP = StringHelper.ParseInt(message.Parameter[12], 0);
                user.JobExpPerHour = StringHelper.ParseInt(message.Parameter[13], 0);

                user.KilledMobs = StringHelper.ParseInt(message.Parameter[14], 0);
                
                SendConnectedUser(message.Client, user);
                SendConnectedPlayerList(message.Client);
            }
        }

        private void HandleMessage_PropertieChanged(User user, NetworkMessage message)
        {
            if (message.Parameter.Length == 2)
            {
                ChangedPropertieType propertieType = (ChangedPropertieType)StringHelper.ParseInt(message.Parameter[0], -1);
                string value = message.Parameter[1];

#if DEBUG
                Console.WriteLine("{0} - Propertie changed: {1},{2}",
                    message.EndPoint, propertieType, value);
#endif

                switch ((ChangedPropertieType)StringHelper.ParseInt(message.Parameter[0], -1))
                {
                    case ChangedPropertieType.CharacterSelection:
                        user.CharacterSelection = message.Parameter[1] == "1";
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;

                    case ChangedPropertieType.Name:
                        user.Name = value;
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;

                    case ChangedPropertieType.Hp:
                        user.HP = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;
                    case ChangedPropertieType.MaxHp:
                        user.MaxHP = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;

                    case ChangedPropertieType.Sp:
                        user.SP = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;
                    case ChangedPropertieType.MaxSp:
                        user.MaxSP = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;

                    case ChangedPropertieType.Map:
                        user.Map = value;
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;

                    case ChangedPropertieType.BaseLevel:
                        user.BaseLevel = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;
                    case ChangedPropertieType.JobLevel:
                        user.JobLevel = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;

                    case ChangedPropertieType.BaseExp:
                        user.BaseEXP = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;
                    case ChangedPropertieType.JobExp:
                        user.JobEXP = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;

                    case ChangedPropertieType.KilledMobs:
                        user.KilledMobs = StringHelper.ParseInt(value, 0);
                        SendChangedPropertie(message.Client, propertieType, value);
                        break;
                }
            }
        }

        private void HandleMessage_Disconnected(User user, NetworkMessage message)
        {
            SendConnectionClosed(message.Client);
        }




        /// <summary>
        /// Sendet eine Nachricht an alle Clients, dass sich ein neuer Benutzer verbunden hat.
        /// </summary>
        /// <param name="client">Client der die Verbindung hergestellt hat.</param>
        /// <param name="user">Benutzer der sich verbunden hat.</param>
        private void SendConnectedUser(long client, User user)
        {
            foreach (long otherClient in _connections.Keys)
            {
                if (otherClient != client)
                {
                    Send(ServerMessageId.UserConnected, otherClient,
                        client.ToString(),

                        user.Name,
                        user.Map,

                        user.BaseLevel.ToString(),
                        user.JobLevel.ToString(),

                        user.HP.ToString(),
                        user.MaxHP.ToString(),

                        user.SP.ToString(),
                        user.MaxSP.ToString(),

                        user.BaseEXP.ToString(),
                        user.RequiredBaseEXP.ToString(),
                        user.BaseExpPerHour.ToString(),

                        user.JobEXP.ToString(),
                        user.RequiredJobEXP.ToString(),
                        user.JobExpPerHour.ToString(),

                        user.KilledMobs.ToString());
                }
            }
        }

        /// <summary>
        /// Sendet an einen Client, eine Liste aller verbundenen Clients.
        /// </summary>
        /// <param name="client">Client dem die Liste gesendet werden soll.</param>
        private void SendConnectedPlayerList(long client)
        {
            foreach (var otherClient in _connections)
            {
                if (otherClient.Key != client)
                {
                    Send(ServerMessageId.UserConnected, client,
                        otherClient.Key.ToString(),

                        otherClient.Value.Name,
                        otherClient.Value.Map,

                        otherClient.Value.BaseLevel.ToString(),
                        otherClient.Value.JobLevel.ToString(),

                        otherClient.Value.HP.ToString(),
                        otherClient.Value.MaxHP.ToString(),

                        otherClient.Value.SP.ToString(),
                        otherClient.Value.MaxSP.ToString(),

                        otherClient.Value.BaseEXP.ToString(),
                        otherClient.Value.RequiredBaseEXP.ToString(),
                        otherClient.Value.BaseExpPerHour.ToString(),

                        otherClient.Value.JobEXP.ToString(),
                        otherClient.Value.RequiredJobEXP.ToString(),
                        otherClient.Value.JobExpPerHour.ToString(),

                        otherClient.Value.KilledMobs.ToString());
                }
            }
        }

        /// <summary>
        /// Sendet eine Nachricht an alle Client, dass sich ein wert eines Benutzers geändert hat.
        /// </summary>
        /// <param name="client">Benutzer bei dem sich ein Wert verändert hat.</param>
        /// <param name="type">Typ des Wertes.</param>
        /// <param name="value">Wert der gesendet werden soll.</param>
        private void SendChangedPropertie(long client, ChangedPropertieType type, string value)
        {
            foreach (long otherClient in _connections.Keys)
            {
                if (otherClient != client)
                {
                    Send(ServerMessageId.PropertieChanged, otherClient,
                        client.ToString(),
                        Convert.ToInt32(type).ToString(),
                        value);
                }
            }
        }

        /// <summary>
        /// Sendet an alle Spieler dass der angegebene Client die Verbindung unterbrochen hat.
        /// </summary>
        /// <param name="client">Client der die Verbindung unterbrochen hat.</param>
        private void SendConnectionClosed(long client)
        {
            foreach (long otherClient in _connections.Keys)
            {
                if (otherClient != client)
                {
                    Send(ServerMessageId.UserDisconnected, otherClient,
                        client.ToString());
                }
            }
        }





        /// <summary>
        /// Sendet die Nachricht zum Client.
        /// </summary>
        /// <param name="id">Typ der Nachricht.</param>
        /// <param name="client">Der Client, an dem die Nachricht gesendet wird.</param>
        /// <param name="parameter">Parameter die in der Nachricht geschrieben werden.</param>
        private void Send(ServerMessageId id, long client, params string[] parameter)
        {
            _server.Send(client, (ushort)id, parameter);
        }

        #endregion
    }
}