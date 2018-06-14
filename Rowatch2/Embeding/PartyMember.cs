using System;

namespace Rowatch2.Embeding
{
    public class PartyMember
    {
        public string Name { get; set; }
        public string Map { get; set; }

        public int BaseLevel { get; set; }
        public int JobLevel { get; set; }

        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int SP { get; set; }
        public int MaxSP { get; set; }

        public int BaseExp { get; set; }
        public int BaseExpRequired { get; set; }
        public int BaseExpPerHour { get; set; }
        
        public int JobExp { get; set; }
        public int JobExpRequired { get; set; }
        public int JobExpPerHour { get; set; }

        public int KilledMobs { get; set; }
    }
}