namespace Rowatch2.Plugin
{
    public class CharacterInformations
    {
        public string Name { get; set; }

        public Job Job { get; set; }

        public int HP { get; set; }
        public int MaxHP { get; set; }

        public int SP { get; set; }
        public int MaxSP { get; set; }

        public int BaseLevel { get; set; }
        public int JobLevel { get; set; }

        public int BaseExp { get; set; }
        public int BaseExpRequired { get; set; }

        public int JobExp { get; set; }
        public int JobExpRequired { get; set; }

        public int Zeny { get; set; }

        public string Map { get; set; }
    }
}