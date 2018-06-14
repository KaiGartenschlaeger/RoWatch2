using System;

namespace Rowatch2.Globals
{
    public class AutoWing
    {
        public int Key { get; set; }
        public int TimeSeconds { get; set; }
        public AutowingCondition AutowingCondition { get; set; }
        public DateTime LastUse { get; set; }
    }
}