using ExtendedControls;
using Rowatch2.Embeding;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormAddSkillTimer : FormEx
    {
        #region Constructor

        public FormAddSkillTimer(List<SkillTimer> timer)
        {
            InitializeComponent();

            m_timer = timer;
            foreach (SkillTimer t in timer)
            {
                lbxTimer.Items.Add(t);
            }

            CheckSelection();
        }

        #endregion

        #region Helper

        private void CheckSelection()
        {
            btnAdd.Enabled = (lbxTimer.SelectedIndex != -1);
        }

        private void Accept()
        {
            if (lbxTimer.SelectedIndex != -1)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        #endregion

        #region Control Events

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Accept();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void lbxTimer_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckSelection();
        }

        private void lbxTimer_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Accept();
        }

        #endregion

        #region Fields

        private List<SkillTimer> m_timer = null;

        #endregion

        #region Properties

        public SkillTimer SelectedTimer
        {
            get
            {
                if (lbxTimer.SelectedIndex != -1)
                {
                    return m_timer[lbxTimer.SelectedIndex];
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion
    }
}