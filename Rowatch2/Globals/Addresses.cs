namespace Rowatch2.Globals
{
    public class Addresses
    {
        public string Hash { get; set; }

        public int Name { get; set; }
        public int BaseLevel { get; set; }
        public int JobLevel { get; set; }
        public int BaseExp { get; set; }
        public int BaseExpRequired { get; set; }
        public int JobExp { get; set; }
        public int JobExpRequired { get; set; }
        public int JobClass { get; set; }
        public int Zeny { get; set; }
        public int Map { get; set; }
        public int Hp { get; set; }
        public int HpMax { get; set; }
        public int Sp { get; set; }
        public int SpMax { get; set; }

        public bool SupportHomunculus { get; set; }
        public int Homu_Name { get; set; }
        public int Homu_HP { get; set; }
        public int Homu_MaxHP { get; set; }
        public int Homu_Hungry { get; set; }
        public int Homu_Exp { get; set; }
        public int Homu_MaxExp { get; set; }
        public int Homu_Friendly { get; set; }

        public bool SupportPet { get; set; }
        public int PetName { get; set; }
        public int PetFriendly { get; set; }
        public int PetHungry { get; set; }
    }
}