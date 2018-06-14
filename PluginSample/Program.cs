using Rowatch2.Plugin;
using System;

namespace PluginSample
{
    public class Program : IPlugin
    {
        FormDebug _form;
        int _lastExp = 0;

        // open sample window to display some data
        public void Initializing()
        {
            _form = new FormDebug();
            _form.Show();

            _form.AddToLog("Initializing plugin");
        }

        public void Tick(CharacterInformations characterInformations)
        {
            // warning: if an exception is occured, the plugin will be deactivated by rowatch.
            // throw new Exception("test 123");

            if (_lastExp < characterInformations.BaseExp)
            {
                _form.AddToLog(string.Format("Gained {0:n0} base exp",
                    characterInformations.BaseExp - _lastExp));

                _lastExp = characterInformations.BaseExp;
            }
        }

        public string Name
        {
            get { return "Sample Plugin"; }
        }
    }
}