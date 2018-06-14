using ExtendedControls;
using Rowatch2.Embeding;
using Rowatch2.Enums;
using Rowatch2.Librarys;
using System;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormMacroAction : FormEx
    {
        #region Constructor

        public FormMacroAction(MacroAction action)
        {
            InitializeComponent();

            Array vkeys = Enum.GetValues(typeof(VKey));
            for (int i = 0; i < vkeys.Length; i++)
            {
                VKey key = (VKey)vkeys.GetValue(i);

                cbxKeys.Items.Add(new VKeyItem(key));
                if (key == VKey.F1)
                {
                    cbxKeys.SelectedIndex = i;
                }
            }

            if (action == null)
            {
                chbKey.Checked = true;
            }
            else
            {
                if (action.Delay.HasValue)
                {
                    chbDelay.Checked = true;

                    nudMinutes.Value = (int)action.Delay.Value.Minutes;
                    nudSeconds.Value = (int)action.Delay.Value.Seconds;
                    nudMiliseconds.Value = (int)action.Delay.Value.Milliseconds;
                }
                else
                {
                    chbKey.Checked = true;

                    cbxKeys.SelectedIndex = 0;
                    for (int i = 0; i < vkeys.Length; i++)
                    {
                        VKey key = (VKey)vkeys.GetValue(i);
                        if (key == action.Key.Value)
                        {
                            cbxKeys.SelectedIndex = i;
                        }
                    }
                }
            }
        }

        #endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if ((chbDelay.Checked && (nudMinutes.Value > 0 || nudSeconds.Value > 0 || nudMiliseconds.Value > 0))
                || (chbKey.Checked && cbxKeys.SelectedIndex != -1))
            {
                m_action = new MacroAction();

                if (chbKey.Checked)
                {
                    m_action.Type = MacroActionType.Key;
                    m_action.Key = ((VKeyItem)cbxKeys.SelectedItem).Key;
                }
                else
                {
                    m_action.Type = MacroActionType.Delay;
                    m_action.Delay = new TimeSpan(0, 0, 
                        (int)nudMinutes.Value, (int)nudSeconds.Value, (int)nudMiliseconds.Value);
                }

                DialogResult = DialogResult.OK;
                Close();
            }
        }

        #region Properties

        private MacroAction m_action = null;
        public MacroAction Action
        {
            get { return m_action; }
            set { m_action = value; }
        }

        #endregion

        bool m_typeChanging = false;

        private void chbDelay_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_typeChanging)
            {
                m_typeChanging = true;

                chbDelay.Checked = true;
                chbKey.Checked = false;

                m_typeChanging = false;
            }
        }

        private void chbKey_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_typeChanging)
            {
                m_typeChanging = true;

                chbKey.Checked = true;
                chbDelay.Checked = false;

                m_typeChanging = false;
            }
        }
    }
}