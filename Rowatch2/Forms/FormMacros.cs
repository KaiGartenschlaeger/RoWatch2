using ExtendedControls;
using Rowatch2.Embeding;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormMacros : FormEx
    {
        #region Constructor

        public FormMacros(List<Macro> macros)
        {
            InitializeComponent();

            m_macros = macros;
            foreach (Macro macro in macros)
            {
                lbxMacros.Items.Add(macro);
            }

            if (lbxMacros.Items.Count > 0)
            {
                lbxMacros.SelectedIndex = 0;
            }
            lbxMacros.Focus();
        }

        #endregion

        #region Control Events

        private void btnCreate_Click(object sender, EventArgs e)
        {
            using (FormMacro dialog = new FormMacro(null))
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    Macro macro = new Macro();
                    macro.Description = dialog.Description;
                    macro.Actions = dialog.Actions;

                    m_macros.Add(macro);
                    lbxMacros.Items.Add(macro);

                    lbxMacros.SelectedIndices.Clear();
                    lbxMacros.SelectedIndex = lbxMacros.Items.Count - 1;
                }
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            if (lbxMacros.SelectedIndex != -1)
            {
                Macro newMacro = new Macro(m_macros[lbxMacros.SelectedIndex]);
                newMacro.Description = newMacro.Description + " - copy";

                m_macros.Insert(lbxMacros.SelectedIndex + 1, newMacro);
                lbxMacros.Items.Insert(lbxMacros.SelectedIndex + 1, newMacro);

                int newIndex = lbxMacros.SelectedIndex + 1;

                lbxMacros.SelectedIndices.Clear();
                lbxMacros.SelectedIndex = newIndex;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (lbxMacros.SelectedIndex != -1)
            {
                m_macros.RemoveAt(lbxMacros.SelectedIndex);
                lbxMacros.Items.RemoveAt(lbxMacros.SelectedIndex);

                int newIndex = lbxMacros.SelectedIndex - 1;
                if (newIndex < 0)
                {
                    newIndex = 0;
                }

                if (lbxMacros.Items.Count > 0)
                {
                    lbxMacros.SelectedIndices.Clear();
                    lbxMacros.SelectedIndex = newIndex;
                }
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (lbxMacros.SelectedIndex != -1)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void lbxMacros_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lbxMacros.SelectedIndex != -1)
            {
                Macro selectedMacro = (Macro)lbxMacros.SelectedItem;
                using (FormMacro dialog = new FormMacro(selectedMacro))
                {
                    if (dialog.ShowDialog(this) == DialogResult.OK)
                    {
                        selectedMacro.Description = dialog.Description;
                        selectedMacro.Actions = dialog.Actions;

                        m_macros[lbxMacros.SelectedIndex] = selectedMacro;
                        lbxMacros.Items[lbxMacros.SelectedIndex] = selectedMacro;
                    }
                }
            }
        }

        #endregion

        #region Fields

        private List<Macro> m_macros = null;

        #endregion

        #region Properties

        public List<Macro> SelectedMacros
        {
            get
            {
                List<Macro> result = new List<Macro>();

                if (lbxMacros.SelectedIndex != -1)
                {
                    foreach (int index in lbxMacros.SelectedIndices)
                    {
                        result.Add((Macro)lbxMacros.Items[index]);
                    }
                }

                return result;
            }
        }

        #endregion
    }
}