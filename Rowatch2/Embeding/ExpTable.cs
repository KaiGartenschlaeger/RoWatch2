using System.Collections.Generic;

namespace Rowatch2.Embeding
{
    internal class ExpTable
    {
        public ExpTable()
        {
            m_levelBase = new List<long>();
            m_supportedClasses = new HashSet<int>();
        }

        #region Properties

        private long m_sumBase;
        public long SumBase
        {
            get { return m_sumBase; }
            set { m_sumBase = value; }
        }

        private List<long> m_levelBase;
        public List<long> LevelBase
        {
            get { return m_levelBase; }
            set { m_levelBase = value; }
        }

        private HashSet<int> m_supportedClasses;
        public HashSet<int> SupportedClasses
        {
            get { return m_supportedClasses; }
            set { m_supportedClasses = value; }
        }

        #endregion
    }
}