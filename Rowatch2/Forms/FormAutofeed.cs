using ExtendedControls;
using Rowatch2.Embeding;
using Rowatch2.Enums;
using Rowatch2.Librarys;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormAutofeed : FormEx
    {
        #region Fields

        private ProcessManager m_process;
        private CharacterInfo m_charInfo;

        private AutoFeedSetup m_setup = new AutoFeedSetup();
        private bool m_recordingActive = false;
        private FeedRecordStep m_recordStep = FeedRecordStep.NotStarted;

        #endregion

        #region Constructor

        public FormAutofeed(ProcessManager processManager, CharacterInfo charInfo)
        {
            InitializeComponent();

            m_process = processManager;
            m_charInfo = charInfo;

            MouseHook.MouseEvent += InterceptMouse_MouseEvent;

            lblText.Text = string.Empty;

            lblFeedButtonValue.Text = string.Empty;
            lblConfirmButtonValue.Text = string.Empty;
        }

        #endregion

        #region Form Events

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            MouseHook.Stop();
        }

        #endregion

        #region Hook Events

        private void InterceptMouse_MouseEvent(object sender, MouseHookEventArgs e)
        {
            if (m_recordingActive && (e.Type == MouseHookEventType.LButtonDown || e.Type == MouseHookEventType.RButtonDown))
            {
                switch (m_recordStep)
                {
                    case FeedRecordStep.FeedButtonLocation:
                        m_setup.LocationFeedButton = new Point(e.X, e.Y);
                        lblFeedButtonValue.Text = string.Format("X:{0}, Y:{1}", e.X, e.Y);
                        m_recordStep = FeedRecordStep.FeedConfirmButtonLocation;
                        lblText.Text = "Confirm button drücken";
                        break;
                    case FeedRecordStep.FeedConfirmButtonLocation:
                        m_setup.LocationFeedConfirmButton = new Point(e.X, e.Y);
                        lblConfirmButtonValue.Text = string.Format("X:{0}, Y:{1}", e.X, e.Y);

                        lblText.Text = string.Empty;

                        m_recordingActive = false;
                        MouseHook.Stop();
                        break;
                }
            }
        }

        #endregion

        #region Control Events

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timerFeed.Enabled)
            {
                timerFeed.Stop();

                btnStart.Text = "Starten";
                nudMinFeedValue.Enabled = true;
            }
            else
            {
                if (m_setup.LocationFeedButton.IsEmpty || m_setup.LocationFeedConfirmButton.IsEmpty)
                {
                    MessageBox.Show("Sie müsen erst die Einstellungen festlegen.", "Ungültige Aktion",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    m_setup.LastFeedTime = DateTime.Now;
                    m_setup.MaxFeedValue = (int)nudMinFeedValue.Value;

                    timerFeed.Start();

                    btnStart.Text = "Anhalten";
                    nudMinFeedValue.Enabled = false;
                }
            }
        }

        private void btnRecord_Click(object sender, EventArgs e)
        {
            if (!m_recordingActive)
            {
                MouseHook.Start();

                m_recordingActive = true;

                m_recordStep = FeedRecordStep.FeedButtonLocation;
                lblText.Text = "Feed button drücken";
            }
        }

        #endregion

        #region Timer Events

        private void timerFeed_Tick(object sender, EventArgs e)
        {
            if (m_charInfo.Homunculus_Hungry <= m_setup.MaxFeedValue)
            {
                switch (m_setup.Action)
                {
                    case FeedAction.FeedButton:
                        if (WinAPI.GetActiveWindow() != m_process.WindowHandle)
                        {
                            WinAPI.SetActiveWindow(m_process.WindowHandle);

                            lblText.Text = "FeedButton";

                            SendInputManager.MouseClick(m_setup.LocationFeedButton.X, m_setup.LocationFeedButton.Y, 100);

                            m_setup.Action = FeedAction.Delay;
                            m_setup.DelayTime = DateTime.Now;
                        }
                        break;

                    case FeedAction.Delay:
                        if (DateTime.Now.Subtract(m_setup.DelayTime).TotalSeconds >= 4)
                        {
                            lblText.Text = "ConfirmButton";

                            m_setup.Action = FeedAction.FeedConfirmButton;
                        }
                        else
                        {
                            lblText.Text = string.Format("Delay: {0:n0}",
                                4 - DateTime.Now.Subtract(m_setup.DelayTime).TotalSeconds);
                        }
                        break;

                    case FeedAction.FeedConfirmButton:
                        if (WinAPI.GetActiveWindow() != m_process.WindowHandle)
                        {
                            WinAPI.SetActiveWindow(m_process.WindowHandle);

                            lblText.Text = string.Empty;

                            SendInputManager.MouseClick(m_setup.LocationFeedConfirmButton.X, m_setup.LocationFeedConfirmButton.Y, 100);

                            m_setup.LastFeedTime = DateTime.Now;
                            m_setup.Action = FeedAction.FeedButton;
                        }
                        break;
                }
            }
        }

        #endregion
    }
}