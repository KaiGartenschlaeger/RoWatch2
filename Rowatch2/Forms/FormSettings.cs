using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ExtendedControls;
using Rowatch2.Enums;
using Rowatch2.Globals;
using System.Net;
using Rowatch2.Embeding;
using Rowatch2.Librarys;
using Tools;

namespace Rowatch2.Forms
{
    internal partial class FormSettings : FormEx
    {
        #region Fields

        private ProgramSettings m_settings;
        private Paths _paths;

        private List<Panel> _panels;

        #endregion

        #region Constructor

        public FormSettings(ProgramSettings settings, Paths paths, List<PluginInfo> plugins)
        {
            InitializeComponent();

            m_settings = settings;
            InitSettings();

            _paths = paths;

            foreach (PluginInfo info in plugins)
            {
                lbxPlugins.Items.Add(info.Plugin.Name);
            }

            InitSettingsAreas();
        }

        #endregion

        #region Methods

        private void InitSettings()
        {
            // logtypes
            string[] logTypesNames = Enum.GetNames(typeof(LogType));
            int[] logTypeValues = (int[])Enum.GetValues(typeof(LogType));
            for (int i = 0; i < logTypesNames.Length; i++)
            {
                lbxLogTypes.Items.Add(logTypesNames[i]);
                if ((m_settings.LogTypes & logTypeValues[i]) == logTypeValues[i])
                {
                    lbxLogTypes.SetSelected(i, true);
                }
            }

            if (string.IsNullOrEmpty(m_settings.Language))
            {
                m_settings.Language = "de";
            }
            switch (m_settings.Language.Trim().ToLower())
            {
                case "en":
                    comboBox1.SelectedIndex = 1;
                    break;

                default:
                    comboBox1.SelectedIndex = 0;
                    break;
            }

            // game directory
            tbxGameDirectory.Text = m_settings.GameDirectory;
            tbxGameExeFilename.Text = m_settings.ClientExeFilename;

            // misc
            chbAddTimeInLog.Checked = m_settings.AddTimeInLog;

            chbOnTop.Checked = m_settings.TopMost;
            chbOpacity.Checked = m_settings.Opacity;

            tbxMobDbUrl.Text = m_settings.MobDbUrl;

            tbxPartyServerIP.Text = m_settings.PartyServerIP;

            chbMoveAllWindows.Checked = m_settings.MoveAllWindows;

            chbVersionCheck.Checked = m_settings.CheckVersion;

            // screenshot
            chbBaseupScreenshot.Checked = m_settings.BaseupScreenshot;
            nudBaseupScreenshotDelay.Value = m_settings.BaseupScreenshotDelay;
            chbJobupScreenshot.Checked = m_settings.JobupScreenshot;
            nudJobupScreenshotDelay.Value = m_settings.JobupScreenshotDelay;

            nudMinLevelScreenshots.Value = MathHelper.Clamp(m_settings.MinLevelScreenshots,
                (int)nudMinLevelScreenshots.Minimum, (int)nudMinLevelScreenshots.Maximum);

            // homunculus
            chbHomunculusRawExp.Checked = m_settings.HomunculusRawExp;

            chbHomHungryWarnSound.Checked = m_settings.HomunculusHungryWarnSound;
            nudHomunculusHungryMin.Value = m_settings.HomunculusHungryMinValue;
            chbHomHungryCloseClient.Checked = m_settings.HomunculusHungryCloseClient;

            // pet
            chbFeedAlarmPet.Checked = m_settings.PetFeedAlarmSound;
            nudMinimumPetHungry.Value = MathHelper.Clamp(m_settings.PetMinimumHungryValue,
                (int)nudMinimumPetHungry.Minimum, (int)nudMinimumPetHungry.Maximum);

            // cash register sound
            chbCashRegisterSound.Checked = m_settings.CashRegisterSound_Enabled;

            nudCashRegisterSound_MinimumZeny.Value = m_settings.CashRegisterSound_MinimumZeny;
            nudCashRegisterSound_MaximumZeny.Value = m_settings.CashRegisterSound_MaximumZeny;

            // auto wing
            cbxAutowingKey.SelectedIndex = m_settings.AutoWing.Key;
            nudAutowingSeconds.Value = m_settings.AutoWing.TimeSeconds;
            rbnAutowingAfterTime.Checked = (m_settings.AutoWing.AutowingCondition == AutowingCondition.AfterTime);
            rbnAutowingNoDmg.Checked = (m_settings.AutoWing.AutowingCondition == AutowingCondition.NoDmg);
            rbnAutowingNoKill.Checked = (m_settings.AutoWing.AutowingCondition == AutowingCondition.NoKill);

            // auto potions
            cbxAutopotKey.SelectedIndex = m_settings.AutoPotions.Key;
            nudAutopotDelay.Value = m_settings.AutoPotions.Delay;
            nudAutopotMinimumHP.Value = m_settings.AutoPotions.MinimumPercent;
            nudAutopotHealTo.Value = m_settings.AutoPotions.MaximumPercent;

            // auto key
            cbxAutokeyKey.SelectedIndex = m_settings.AutoKey.Key;
            nudAutokeyInterval.Value = m_settings.AutoKey.Interval;
        }

        private void InitSettingsAreas()
        {
            _panels = new List<Panel>();

            Size = new Size(lbxMenu.Width + 319 + 35, lbxMenu.Height + 80);

            foreach (Control control in Controls)
            {
                if (control is Panel)
                {
                    _panels.Add((Panel)control);

                    control.Location = new Point(132, 6);
                    control.Size = new Size(319, 251);
                    control.Visible = false;

                    if (control.Tag != null)
                    {
                        lbxMenu.Items.Add(control.Tag.ToString());
                    }
                    else
                    {
                        lbxMenu.Items.Add(control.Name);
                    }
                }
            }

            if (lbxMenu.Items.Contains("General"))
            {
                lbxMenu.SelectedItem = "General";
            }
            if (lbxMenu.Items.Count > 0 && lbxMenu.SelectedIndex == -1)
            {
                lbxMenu.SelectedIndex = 0;
            }
        }

        private void ApplySettings()
        {
            // logtypes
            int logTypes = 0;
            foreach (string name in lbxLogTypes.SelectedItems)
            {
                LogType type = (LogType)Enum.Parse(typeof(LogType), name);
                int value = (int)type;

                logTypes = logTypes | value;
            }

            m_settings.LogTypes = logTypes;

            // game directory
            m_settings.GameDirectory = tbxGameDirectory.Text;
            m_settings.ClientExeFilename = tbxGameExeFilename.Text;

            // misc
            m_settings.AddTimeInLog = chbAddTimeInLog.Checked;

            m_settings.TopMost = chbOnTop.Checked;
            m_settings.Opacity = chbOpacity.Checked;

            m_settings.MobDbUrl = tbxMobDbUrl.Text;

            m_settings.PartyServerIP = tbxPartyServerIP.Text;

            switch (comboBox1.SelectedIndex)
            {
                case 1:
                    m_settings.Language = "en";
                    break;

                default:
                    m_settings.Language = "de";
                    break;
            }

            m_settings.MoveAllWindows = chbMoveAllWindows.Checked;

            m_settings.CheckVersion = chbVersionCheck.Checked;

            // screenshot
            m_settings.BaseupScreenshot = chbBaseupScreenshot.Checked;
            m_settings.BaseupScreenshotDelay = (int)nudBaseupScreenshotDelay.Value;
            m_settings.JobupScreenshot = chbJobupScreenshot.Checked;
            m_settings.JobupScreenshotDelay = (int)nudJobupScreenshotDelay.Value;

            m_settings.MinLevelScreenshots = (int)nudMinLevelScreenshots.Value;

            // homunculus
            m_settings.HomunculusRawExp = chbHomunculusRawExp.Checked;

            m_settings.CashRegisterSound_Enabled = chbCashRegisterSound.Checked;
            m_settings.CashRegisterSound_MinimumZeny = (int)nudCashRegisterSound_MinimumZeny.Value;
            m_settings.CashRegisterSound_MaximumZeny = (int)nudCashRegisterSound_MaximumZeny.Value;

            m_settings.HomunculusHungryWarnSound = chbHomHungryWarnSound.Checked;
            m_settings.HomunculusHungryMinValue = (int)nudHomunculusHungryMin.Value;
            m_settings.HomunculusHungryCloseClient = chbHomHungryCloseClient.Checked;

            // pet
            m_settings.PetFeedAlarmSound = chbFeedAlarmPet.Checked;
            m_settings.PetMinimumHungryValue = (int)nudMinimumPetHungry.Value;

            // auto wing
            m_settings.AutoWing.Key = cbxAutowingKey.SelectedIndex;
            m_settings.AutoWing.TimeSeconds = (int)nudAutowingSeconds.Value;

            if (rbnAutowingAfterTime.Checked)
            {
                m_settings.AutoWing.AutowingCondition = AutowingCondition.AfterTime;
            }
            else if (rbnAutowingNoKill.Checked)
            {
                m_settings.AutoWing.AutowingCondition = AutowingCondition.NoKill;
            }
            else
            {
                m_settings.AutoWing.AutowingCondition = AutowingCondition.NoDmg;
            }

            // auto potions
            m_settings.AutoPotions.Key = cbxAutopotKey.SelectedIndex;
            m_settings.AutoPotions.Delay = (int)nudAutopotDelay.Value;
            m_settings.AutoPotions.MinimumPercent = (int)nudAutopotMinimumHP.Value;
            m_settings.AutoPotions.MaximumPercent = (int)nudAutopotHealTo.Value;

            // auto keys
            m_settings.AutoKey.Key = cbxAutokeyKey.SelectedIndex;
            m_settings.AutoKey.Interval = (int)nudAutokeyInterval.Value;
        }

        private void ChangeGameDirectory()
        {
            using (FolderBrowserDialog dialog = new FolderBrowserDialog())
            {
                if (!string.IsNullOrEmpty(m_settings.GameDirectory))
                {
                    dialog.SelectedPath = m_settings.GameDirectory;
                }

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    tbxGameDirectory.Text = dialog.SelectedPath;
                }
            }
        }

        #endregion

        #region Control Events

        private void lbxMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = lbxMenu.SelectedIndex;
            if (index >= 0 && _panels.Count > index)
            {
                foreach (Panel panel in _panels)
                {
                    panel.Visible = false;
                }
                _panels[index].Visible = true;
            }
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, System.EventArgs e)
        {
            if (ValidateSettings())
            {
                ApplySettings();

                if (OnApply != null)
                {
                    OnApply(this, EventArgs.Empty);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private bool ValidateSettings()
        {
            IPAddress address;
            if (!IPAddress.TryParse(tbxPartyServerIP.Text.Trim(), out address))
            {
                MessageBox.Show("The ip-address entered by party-server is invalid, please enter a correct ip.", "Wrong IP",
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return false;
            }

            if (nudCashRegisterSound_MinimumZeny.Value != 0 && nudCashRegisterSound_MaximumZeny.Value != 0)
            {
                if (nudCashRegisterSound_MaximumZeny.Value < nudCashRegisterSound_MinimumZeny.Value)
                {
                    MessageBox.Show("Maximum zeny value must be greater than minimum zeny value.", "Cashregister sound",
                        MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return false;
                }
            }

            return true;
        }

        private void btnGameDirectory_Click(object sender, EventArgs e)
        {
            ChangeGameDirectory();
        }

        private void tbxGameDirectory_Enter(object sender, EventArgs e)
        {
            btnGameDirectory.Focus();
            ChangeGameDirectory();
        }

        #endregion

        #region Events

        public event EventHandler OnApply;

        #endregion
    }
}