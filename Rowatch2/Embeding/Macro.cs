using System.Collections.Generic;

namespace Rowatch2.Embeding
{
    public class Macro
    {
        public Macro()
        {
            m_actions = new List<MacroAction>();
        }

        public Macro(Macro macro)
            : this()
        {
            m_description = macro.Description;
            foreach (MacroAction action in macro.Actions)
            {
                m_actions.Add(new MacroAction(action));
            }
        }


        private string m_description;
        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        private List<MacroAction> m_actions;
        public List<MacroAction> Actions
        {
            get { return m_actions; }
            set { m_actions = value; }
        }

        public override string ToString()
        {
            return m_description;
        }
    }
}
