using ExtendedControls;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormMinimized : FormEx
    {
        #region Constructor

        public FormMinimized()
        {
            InitializeComponent();

            InitGUI();
        }

        #endregion

        #region Form Methods

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_LBUTTONDOWN:
                    Window_MessageSend(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
                    return;
            }
            base.WndProc(ref m);
        }

        private void FormMinimized_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Methods

        private void InitGUI()
        {
            Opacity = 0.65f;

            lblBaseExpValues.Text = string.Empty;
            lblBasePercent.Text = string.Empty;
            lblJobExpValues.Text = string.Empty;
            lblJobPercent.Text = string.Empty;

            lblBaseExpHour.Text = string.Empty;
            lblJobExpHour.Text = string.Empty;

            lblGainedBaseExp.Text = string.Empty;
            lblGainedJobExp.Text = string.Empty;

            lblRequiredBaseExp.Text = string.Empty;
            lblRequiredJobExp.Text = string.Empty;

            lblKilledMobs.Text = string.Empty;
        }

        private void SetLabelText(Label label, string text)
        {
            if (label.Text != text)
            {
                label.Text = text;
            }
        }

        #endregion

        #region Control Events

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (OnClose != null)
            {
                OnClose(this, EventArgs.Empty);
            }

            Close();
        }

        #endregion

        #region Events

        public event EventHandler OnClose;

        #endregion

        #region Properties

        public string BaseExpValues
        {
            get { return lblBaseExpValues.Text; }
            set { SetLabelText(lblBaseExpValues, value); }
        }
        public string JobExpValues
        {
            get { return lblJobExpValues.Text; }
            set { SetLabelText(lblJobExpValues, value); }
        }

        public string BaseExpPercent
        {
            get { return lblBasePercent.Text; }
            set { SetLabelText(lblBasePercent, value); }
        }
        public string JobExpPercent
        {
            get { return lblJobPercent.Text; }
            set { SetLabelText(lblJobPercent, value); }
        }

        public string RequiredBaseExp
        {
            get { return lblRequiredBaseExp.Text; }
            set { SetLabelText(lblRequiredBaseExp, value); }
        }
        public string RequiredJobExp
        {
            get { return lblRequiredJobExp.Text; }
            set { SetLabelText(lblRequiredJobExp, value); }
        }

        public string GainedBaseExp
        {
            get { return lblGainedBaseExp.Text; }
            set { SetLabelText(lblGainedBaseExp, value); }
        }
        public string GainedJobExp
        {
            get { return lblGainedJobExp.Text; }
            set { SetLabelText(lblGainedJobExp, value); }
        }

        public string BaseExpHour
        {
            get { return lblBaseExpHour.Text; }
            set { SetLabelText(lblBaseExpHour, value); }
        }
        public string JobExpHour
        {
            get { return lblJobExpHour.Text; }
            set { SetLabelText(lblJobExpHour, value); }
        }

        public string KilledMobs
        {
            get { return lblKilledMobs.Text; }
            set { SetLabelText(lblKilledMobs, value); }
        }

        #endregion

        #region WinAPI

        private const int WM_NCLBUTTONDOWN = 0x00A1;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_WINDOWPOSCHANGING = 0x0046;
        private const int HTCAPTION = 0x2;

        [DllImport("user32.dll", EntryPoint = "SendMessage")]
        private static extern int Window_MessageSend([In] IntPtr hWnd, [In] int message, [In] int wParam, [In] int lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();

        #endregion
    }
}