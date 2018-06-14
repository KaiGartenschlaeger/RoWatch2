using System;

namespace RoWatch2Server
{
    public class User
    {
        public bool CharacterSelection { get; set; }

        public string Name { get; set; }
        public string Map { get; set; }

        public int CharClass { get; set; }

        public int BaseLevel { get; set; }
        public int JobLevel { get; set; }

        public int HP { get; set; }
        public int MaxHP { get; set; }
        public int SP { get; set; }
        public int MaxSP { get; set; }

        public int BaseEXP { get; set; }
        public int RequiredBaseEXP { get; set; }
        public int BaseExpPerHour { get; set; }

        public int JobEXP { get; set; }
        public int RequiredJobEXP { get; set; }
        public int JobExpPerHour { get; set; }

        public int KilledMobs { get; set; }
    }
}