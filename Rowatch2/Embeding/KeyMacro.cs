using Rowatch2.Librarys;
using System;

namespace Rowatch2.Embeding
{
    public class KeyMacro
    {
        public string Name { get; set; }
        public VKey Key { get; set; }
        public bool CheckFocus { get; set; }
        public TimeSpan Intervall { get; set; }


        public override string ToString()
        {
            return Name;
        }
    }
}