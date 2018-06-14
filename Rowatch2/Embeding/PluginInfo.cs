using Rowatch2.Plugin;

namespace Rowatch2.Embeding
{
    class PluginInfo
    {
        public string Path { get; set; }
        public IPlugin Plugin { get; set; }
        public bool Disabled { get; set; }
    }
}