using System;

namespace Rowatch2.Embeding
{
    public class OnLoginEventArgs : EventArgs
    {
        #region Constructor

        public OnLoginEventArgs(string username, string password)
        {
            _username = username;
            _password = password;
        }

        #endregion

        #region Properties

        private string _username;
        public string Username
        {
            get { return _username; }
            set { _username = value; }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { _password = value; }
        }

        #endregion
    }
}