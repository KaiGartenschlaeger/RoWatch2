using System;

namespace Rowatch2.Plugin
{
    public interface IPlugin
    {
        void Initializing();
        void Tick(CharacterInformations characterInformations);

        string Name { get; }
    }
}