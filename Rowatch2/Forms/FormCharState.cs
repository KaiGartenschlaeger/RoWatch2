using ExtendedControls;

namespace Rowatch2.Forms
{
    public partial class FormCharState : FormEx
    {
        #region Constructor

        public FormCharState()
        {
            InitializeComponent();
        }

        #endregion

        #region Helper

        private void RefreshHpLabel()
        {
            lblHpValue.Text = string.Format("{0:n0} / {1:n0}",
                prbHP.Value, prbHP.Maximum);
        }

        private void RefreshSPLabel()
        {
            lblSpValue.Text = string.Format("{0:n0} / {1:n0}",
                prbSP.Value, prbSP.Maximum);
        }

        #endregion

        #region Properties

        public int HP
        {
            get { return prbHP.Value; }
            set
            {
                if (prbHP.Value != value)
                {
                    prbHP.Value = value;
                    RefreshHpLabel();
                }
            }
        }

        public int MaxHP
        {
            get { return prbHP.Maximum; }
            set
            {
                if (prbHP.Maximum != value)
                {
                    prbHP.Maximum = value;
                    RefreshHpLabel();
                }
            }
        }

        public int SP
        {
            get { return prbSP.Value; }
            set
            {
                if (prbSP.Value != value)
                {
                    prbSP.Value = value;
                    RefreshSPLabel();
                }
            }
        }

        public int MaxSP
        {
            get { return prbSP.Maximum; }
            set
            {
                if (prbSP.Maximum != value)
                {
                    prbSP.Maximum = value;
                    RefreshSPLabel();
                }
            }
        }

        #endregion

        #region Methods

        public void ResetValues()
        {
            prbHP.Value = 0;
            prbHP.Maximum = 100;
            prbSP.Value = 0;
            prbSP.Maximum = 100;

            lblHpValue.Text = string.Empty;
            lblSpValue.Text = string.Empty;
        }

        #endregion
    }
}