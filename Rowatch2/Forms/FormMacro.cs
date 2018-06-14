using ExtendedControls;
using Rowatch2.Embeding;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormMacro : FormEx
    {
        #region Fields

        private const string NEW_ACTION = "(add action)";

        #endregion

        #region Constructor

        public FormMacro(Macro macro)
        {
            InitializeComponent();

            m_actions = new List<MacroAction>();
            if (macro != null)
            {
                tbxName.Text = macro.Description;

                foreach (MacroAction action in macro.Actions)
                {
                    MacroAction newAction = new MacroAction(action);

                    m_actions.Add(newAction);
                    lbxKeys.Items.Add(newAction);
                }
            }

            tbxName.Select(0, 0);

            lbxKeys.Items.Add(NEW_ACTION);
            lbxKeys.SelectedIndex = lbxKeys.Items.Count - 1;
        }

        private void FormMacro_Shown(object sender, EventArgs e)
        {
            lbxKeys.Focus();
        }

        #endregion

        #region Helper

        private void RefreshItemButtons()
        {
            btnClear.Enabled = (lbxKeys.Items.Count > 1);
            btnCopy.Enabled = (lbxKeys.SelectedIndex < lbxKeys.Items.Count - 1);
            btnDelete.Enabled = (lbxKeys.SelectedIndex < lbxKeys.Items.Count - 1);
            btnUp.Enabled = (lbxKeys.SelectedIndex > 0 && lbxKeys.SelectedIndex != lbxKeys.Items.Count - 1);
            btnDown.Enabled = (lbxKeys.SelectedIndex < lbxKeys.Items.Count - 2);
        }

        #endregion

        #region Control Events

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxName.Text) && m_actions.Count > 0)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lbxKeys_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            MacroAction selectedAction = lbxKeys.SelectedItem as MacroAction;
            using (FormMacroAction dialog = new FormMacroAction(selectedAction))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    if (selectedAction != null)
                    {
                        lbxKeys.Items[lbxKeys.SelectedIndex] = dialog.Action;
                        m_actions[lbxKeys.SelectedIndex] = dialog.Action;
                    }
                    else
                    {
                        lbxKeys.Items.Insert(lbxKeys.Items.Count - 1, dialog.Action);
                        m_actions.Add(dialog.Action);
                    }
                }
            }
        }

        private void lbxKeys_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshItemButtons();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (lbxKeys.SelectedIndex != -1 && lbxKeys.SelectedIndex != lbxKeys.Items.Count - 1)
            {
                MacroAction selectedAction = (MacroAction)lbxKeys.SelectedItem;

                MacroAction newAction = new MacroAction(selectedAction);

                m_actions.Insert(lbxKeys.SelectedIndex + 1, newAction);
                lbxKeys.Items.Insert(lbxKeys.SelectedIndex + 1, newAction);

                lbxKeys.SelectedIndex++;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbxKeys.SelectedIndex != -1 && lbxKeys.SelectedIndex != lbxKeys.Items.Count - 1)
            {
                int newIndex = lbxKeys.SelectedIndex - 1;

                m_actions.RemoveAt(lbxKeys.SelectedIndex);
                lbxKeys.Items.RemoveAt(lbxKeys.SelectedIndex);

                if (newIndex < 0)
                {
                    newIndex = 0;
                }

                if (lbxKeys.Items.Count > 0)
                {
                    lbxKeys.SelectedIndex = newIndex;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            m_actions.Clear();

            lbxKeys.Items.Clear();

            lbxKeys.Items.Add(NEW_ACTION);
            lbxKeys.SelectedIndex = 0;
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            if (lbxKeys.SelectedIndex > 0 && lbxKeys.SelectedIndex != lbxKeys.Items.Count - 1)
            {
                m_actions.Reverse(lbxKeys.SelectedIndex - 1, 2);

                lbxKeys.Items[lbxKeys.SelectedIndex - 1] = m_actions[lbxKeys.SelectedIndex - 1];
                lbxKeys.Items[lbxKeys.SelectedIndex] = m_actions[lbxKeys.SelectedIndex];

                lbxKeys.SelectedIndex--;
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (lbxKeys.SelectedIndex < lbxKeys.Items.Count - 2)
            {
                m_actions.Reverse(lbxKeys.SelectedIndex, 2);

                lbxKeys.Items[lbxKeys.SelectedIndex] = m_actions[lbxKeys.SelectedIndex];
                lbxKeys.Items[lbxKeys.SelectedIndex + 1] = m_actions[lbxKeys.SelectedIndex + 1];

                lbxKeys.SelectedIndex++;
            }
        }

        #endregion

        #region Properties

        public string Description
        {
            get { return tbxName.Text; }
        }

        private List<MacroAction> m_actions;
        public List<MacroAction> Actions
        {
            get { return m_actions; }
        }

        #endregion
    }
}