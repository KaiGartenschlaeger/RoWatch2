using System.Collections.Generic;

namespace Rowatch2.Embeding
{
    public class JobClass
    {
        public JobClass()
        {
            JobBonus = new List<JobBonus>();
        }

        public string Name { get; set; }

        public int MaxBLvl { get; set; }
        public int MaxJLvl { get; set; }
        
        public int Statspoints { get; set; }
        public int MaxStats { get; set; }
        
        public List<JobBonus> JobBonus { get; set; }

        public int Weight { get; set; }

        public double HPFactor { get; set; }
        public double SPFactor { get; set; }
        
        public override string ToString()
        {
            return Name;
        }
    }
}