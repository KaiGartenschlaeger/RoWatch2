using Rowatch2.Librarys;

namespace Rowatch2.Embeding
{
    public class VKeyItem
    {
        public VKeyItem(VKey key)
        {
            m_key = key;
            m_desription = SendInputManager.GetKeyDescription(key);
        }

        private VKey m_key = VKey.F1;
        public VKey Key
        {
            get { return m_key; }
            set
            {
                m_key = value;
                m_desription = SendInputManager.GetKeyDescription(value);
            }
        }

        private string m_desription = string.Empty;
        public string Description
        {
            get { return m_desription; }
        }

        public override string ToString()
        {
            return m_desription;
        }
    }
}