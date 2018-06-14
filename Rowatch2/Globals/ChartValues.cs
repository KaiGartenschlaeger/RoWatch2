using System;
using System.Collections.Generic;

namespace Rowatch2.Globals
{
    public class ChartValues
    {
        public ChartValues()
        {
            GainedBaseExpValues = new List<int>();
            GainedJobExpValues = new List<int>();
            KilledMobsValues = new List<int>();
            GainedHomunculusExpValues = new List<int>();
        }

        public int LastMinute { get; set; }

        public int GainedBaseExp { get; set; }
        public List<int> GainedBaseExpValues { get; set; }

        public int GainedJobExp { get; set; }
        public List<int> GainedJobExpValues { get; set; }

        public int KilledMobs { get; set; }
        public List<int> KilledMobsValues { get; set; }

        public int GainedHomunculusExp { get; set; }
        public List<int> GainedHomunculusExpValues { get; set; }

        public void ResetLastValues()
        {
            LastMinute = DateTime.Now.Minute;

            GainedBaseExp = 0;
            GainedJobExp = 0;
            KilledMobs = 0;
            GainedHomunculusExp = 0;
        }

        public void ResetAll()
        {
            ResetLastValues();

            GainedBaseExpValues.Clear();
            GainedJobExpValues.Clear();
            KilledMobsValues.Clear();
            GainedHomunculusExpValues.Clear();
        }
    }
}