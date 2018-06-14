using Rowatch2.Enums;

namespace Rowatch2.Embeding
{
    public class JobBonus
    {
        public JobBonus(StatsType statType, params int[] jobLevels)
        {
            _statsType = statType;
            _jobLevels = jobLevels;
        }

        private StatsType _statsType;
        public StatsType StatsType
        {
            get { return _statsType; }
            set { _statsType = value; }
        }

        private int[] _jobLevels;
        public int[] JobLevels
        {
            get { return _jobLevels; }
            set { _jobLevels = value; }
        }
    }
}