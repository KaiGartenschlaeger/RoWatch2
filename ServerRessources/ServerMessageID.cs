namespace ServerRessources
{
    public enum ServerMessageId
    {
        /// <summary>
        /// Ein neuer Benutzer hat sich angemeldet.
        /// </summary>
        UserConnected,

        /// <summary>
        /// Ein Wert für einen Spieler hat sich geändert.
        /// </summary>
        PropertieChanged,

        /// <summary>
        /// Ein Spieler hat die Verbindung zum Server beendet.
        /// </summary>
        UserDisconnected
    }
}