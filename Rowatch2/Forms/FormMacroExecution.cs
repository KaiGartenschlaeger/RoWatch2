using ExtendedControls;
using Rowatch2.Embeding;
using Rowatch2.Enums;
using Rowatch2.Librarys;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormMacroExecution : FormEx
    {
        #region Constructor

        public FormMacroExecution(CharacterInfo charInfo, ProcessManager processManager, Macro macro)
        {
            InitializeComponent();

            m_charInfo = charInfo;
            m_processManager = processManager;
            m_macro = macro;

            lblCurrentActionValue.Text = string.Empty;
            lblCounter.Text = string.Empty;

            Text = m_macro.Description;
        }

        private void FormMacroExecution_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (bwMacro.IsBusy)
            {
                bwMacro.CancelAsync();
            }
        }

        #endregion

        #region Control Events

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            if (!m_processManager.IsActive)
            {
                MessageBox.Show("You have to start rowatch first.", "Stop",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
            else
            {
                if (!bwMacro.IsBusy)
                {
                    bwMacro.RunWorkerAsync();

                    m_executinCounter = 0;

                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                }
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            if (bwMacro.IsBusy)
            {
                bwMacro.CancelAsync();
            }
        }

        private void bwMacro_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (MacroAction action in m_macro.Actions)
            {
                if (bwMacro.CancellationPending)
                {
                    break;
                }
                else
                {
                    bool reportedWaiting = false;
                    while (!bwMacro.CancellationPending && WinAPI.GetForegroundWindow() != m_processManager.WindowHandle)
                    {
                        if (!reportedWaiting)
                        {
                            bwMacro.ReportProgress(0, "Waiting..");
                            reportedWaiting = true;
                        }
                        Thread.Sleep(10);
                    }

                    if (!bwMacro.CancellationPending)
                    {
                        switch (action.Type)
                        {
                            case MacroActionType.Delay:
                                bwMacro.ReportProgress(0, "Delay");

                                int milisecondsToWait = (int)action.Delay.Value.TotalMilliseconds;
                                DateTime startDelayTime = DateTime.Now;

                                while (milisecondsToWait > (int)DateTime.Now.Subtract(startDelayTime).TotalMilliseconds)
                                {
                                    if (bwMacro.CancellationPending)
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Thread.Sleep(10);
                                    }
                                }

                                break;

                            case MacroActionType.Key:
                                bwMacro.ReportProgress(0, "Key: " + SendInputManager.GetKeyDescription(action.Key.Value));

                                SendInputManager.SendInput(action.Key.Value);

                                break;
                        }
                    }
                }
            }

            if (bwMacro.CancellationPending)
            {
                e.Cancel = true;
            }
        }

        private void bwMacro_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            string message = e.UserState.ToString();
            if (lblCurrentActionValue.Text != message)
            {
                lblCurrentActionValue.Text = message;
            }
        }

        private void bwMacro_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            lblCurrentActionValue.Text = string.Empty;

            if (chbRepeat.Checked && !e.Cancelled)
            {
                bwMacro.RunWorkerAsync();

                m_executinCounter++;

                lblCounter.Text = "Counter: " + m_executinCounter.ToString("n0");
                lblCurrentActionValue.Text = string.Empty;
            }
            else
            {
                lblCounter.Text = string.Empty;
                lblCurrentActionValue.Text = string.Empty;

                btnStart.Enabled = true;
                btnStop.Enabled = false;
            }
        }

        #endregion

        #region Fields

        private Macro m_macro = null;

        private ProcessManager m_processManager = null;
        private CharacterInfo m_charInfo = null;

        private int m_executinCounter = 0;

        #endregion
    }
}