using ExtendedControls;
using Rowatch2.Embeding;
using Rowatch2.Librarys;
using System;
using System.Windows.Forms;

namespace Rowatch2.Forms
{
    public partial class FormSkillTimer : FormEx
    {
        #region Constructor

        public FormSkillTimer(CharacterInfo charInfo, ProcessManager processManager, SkillTimer timer)
        {
            InitializeComponent();

            m_charInfo = charInfo;
            m_processManager = processManager;

            m_timer = timer;
            Text = timer.Name;
            nudIntervall.Value = timer.Intervall;

            cbxKey.SelectedIndex = 1;

            prbHP.StartColor = timer.Color;
            prbHP.EndColor = timer.Color;

            int key = (int)timer.Key - (int)Keys.F1;
            if (key >= 0 && key <= 8)
            {
                cbxKey.SelectedIndex = key;
            }

            m_startKey = timer.Key;

            lblLeft.Text = string.Empty;
        }

        #endregion

        #region Control Events

        private void cbxKey_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_startKey = (Keys)((int)Keys.F1 + cbxKey.SelectedIndex);
        }

        private void timerRefresh_Tick(object sender, EventArgs e)
        {
            int seconds = (int)DateTime.Now.Subtract(m_startTime).TotalSeconds;
            int secondsLeft = m_intervall - seconds;

            if (m_lastPercent != secondsLeft)
            {
                m_lastPercent = secondsLeft;

                if (secondsLeft > 0)
                {
                    prbHP.Value = 100 - (int)(((float)seconds / m_intervall) * 100);
                    lblLeft.Text = TimeSpan.FromSeconds(secondsLeft).ToString();
                }
                else
                {
                    prbHP.Value = 0;
                    lblLeft.Text = string.Empty;
                    timerRefresh.Stop();
                }
            }
        }

        private void timerKey_Tick(object sender, EventArgs e)
        {
            if (!m_charInfo.CharacterSelection && WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
            {
                if (WinAPI.GetAsyncKeyState(m_startKey) != 0)
                {
                    m_startTime = DateTime.Now;
                    m_intervall = (int)nudIntervall.Value;
                    prbHP.Value = 100;
                    m_lastPercent = -1;
                    lblLeft.Text = TimeSpan.FromSeconds(m_intervall).ToString();
                    timerRefresh.Enabled = true;
                }
            }
        }

        #endregion

        #region Fields

        private SkillTimer m_timer;
        private ProcessManager m_processManager;
        private CharacterInfo m_charInfo;
        private DateTime m_startTime;
        private Keys m_startKey;
        private int m_lastPercent;
        private int m_intervall;

        #endregion
    }
}