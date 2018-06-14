using ExtendedControls;
using Rowatch2.Embeding;
using Rowatch2.Enums;
using Rowatch2.Globals;
using Rowatch2.Librarys;
using Rowatch2.Plugin;
using ServerRessources;
using SimpleNetwork;
using SimpleSettings;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Net;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Tools;
using Threading = System.Threading;

namespace Rowatch2.Forms
{
    public partial class FormMain : FormEx
    {
        #region Fields

        private bool m_extendedMode = false;

        private Point m_oldWindowLocation = Point.Empty;

        private readonly ProcessManager m_processManager = null;
        private readonly ProgramSettings m_settings = null;
        private readonly Paths m_paths = null;

        private CharacterInfo m_charInfo = null;
        private CharacterInfo m_charInfoLast = null;
        private CharacterStatistic m_charStatistic = null;

        private FormMinimized m_formMinimized = null;
        private FormParty m_formParty = null;
        private FormHomunculus m_formHomunculus = null;
        private FormPet m_formPet = null;
        private FormCharts m_formCharts = null;
        private FormExpCounter m_formExpCounter = null;
        private FormAutofeed m_formAutofeed = null;
        private FormCharState m_formCharState = null;

        private bool m_resetWhileCharacterSelection;

        private List<Mob> m_mobDb = null;
        private List<JobClass> m_jobClasses;

        private List<SkillTimer> m_skillTimer;
        private List<Macro> m_macros;

        private Dictionary<string, string> m_mapDb;

        private Dictionary<int, ExpTable> m_expTables;

        private TCPClient m_client;

        private Dictionary<long, PartyMember> m_partMembersIndex;
        private List<PartyMember> m_partyMembers;

        private Sound m_homFeedAlarmSound;
        private Sound m_cashRegisterSound;
        private Sound m_petFeedAlarmSound;

        private Dictionary<string, string> m_languageTexts;

        private ChartValues m_chartValues;

        private CharacterInformations m_pluginCharInfos;

        private List<PluginInfo> m_plugins;

        #endregion

        #region Constructor

        public FormMain(string[] args)
        {
            InitializeComponent();

            m_paths = new Paths();
            m_settings = new ProgramSettings();

            try
            {
                if (!Directory.Exists(m_paths.SettingsFolder))
                {
                    Directory.CreateDirectory(m_paths.SettingsFolder);
                }
            }
            catch (Exception)
            {
            }

            LoadSettings();

            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

            Text = Text + " Version " + version;

            ResetGui();
            InitGui();

            m_processManager = new ProcessManager();

            ApplySettings();

            LoadMobDb();
            LoadMapDb();
            LoadJobClasses();
            //LoadClients();
            LoadSkillTimer();
            LoadMacros();
            LoadExpTables();

            m_homFeedAlarmSound = new Sound();
            m_homFeedAlarmSound.Load(Properties.Resources.HonunculusHungry);

            m_cashRegisterSound = new Sound();
            m_cashRegisterSound.Load(Properties.Resources.CashRegister);

            m_petFeedAlarmSound = new Sound();
            m_petFeedAlarmSound.Load(Properties.Resources.PetHungry);

            m_partMembersIndex = new Dictionary<long, PartyMember>();
            m_partyMembers = new List<PartyMember>();

            m_client = new TCPClient();

            m_chartValues = new ChartValues();

            RefreshEventImages();

            LoadPlugins();

            AddLog(LogType.Default, GetLanguageText("Started"));

            if (args.Length >= 1 && args[0] != null && args[0].Equals("extended"))
            {
                m_extendedMode = true;
            }
        }

        #endregion

        #region Form Events

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_client != null)
            {
                m_client.Exit();
            }

            SaveMacros();
            SaveSettings();
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            RefreshOpacity();

            if (m_settings.CheckVersion)
            {
                CheckVersion();
            }


            //#if DEBUG
            //            AddSystemLog(SystemLogType.Default, "Default");
            //            AddSystemLog(SystemLogType.Error, "Error");
            //            AddSystemLog(SystemLogType.Notice, "Notice");
            //            AddSystemLog(SystemLogType.Warning, "Warning");
            //#endif
        }

        private void FormMain_Activated(object sender, EventArgs e)
        {
            RefreshOpacity();
        }

        private void FormMain_Deactivate(object sender, EventArgs e)
        {
            RefreshOpacity();
        }

        private void FormMain_LocationChanged(object sender, EventArgs e)
        {
            if (m_settings.MoveAllWindows && !m_oldWindowLocation.IsEmpty)
            {
                Point moved = new Point(
                    Location.X - m_oldWindowLocation.X,
                    Location.Y - m_oldWindowLocation.Y);

                foreach (Form child in OwnedForms)
                {
                    child.Location = new Point(
                        child.Location.X + moved.X,
                        child.Location.Y + moved.Y);
                }
            }

            m_oldWindowLocation = Location;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WinAPI.WM_LBUTTONDOWN:
                    WinAPI.SendMessage(Handle, WinAPI.WM_NCLBUTTONDOWN, WinAPI.HTCAPTION, 0);
                    return;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Control Events

        private void lvwLog_MouseClick(object sender, MouseEventArgs e)
        {
            lvwLog.SelectedIndices.Clear();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            Watch_Start();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Watch_Reset(true);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            using (FormSettings dialog = new FormSettings(m_settings, m_paths, m_plugins))
            {
                dialog.Location = new Point(Left + 25, Top + 25);
                dialog.OnApply += new EventHandler(_formSettings_OnApply);

                dialog.ShowDialog(this);
            }
        }

        private void _formSettings_OnApply(object sender, EventArgs e)
        {
            ApplySettings();
        }

        private void btnMisc_Click(object sender, EventArgs e)
        {
            menuMisc.Show(
                btnMisc,
                new Point(btnMisc.Width, btnMisc.Height + 2),
                ToolStripDropDownDirection.BelowLeft);
        }

        private void bwVersionCheck_DoWork(object sender, DoWorkEventArgs e)
        {
            VersionCheckResult result = new VersionCheckResult();

            try
            {
                AddSystemLog(SystemLogType.Default, GetLanguageText("VersionCheckRunning"));

                XmlDocument doc = new XmlDocument();
                doc.Load("http://www.kaisnet.de/data/version/rowatch2.xml");

                XmlNode nodeVersion = doc["version"];
                if (nodeVersion != null)
                {
                    result.CurrentVersion = new Version(
                        Convert.ToInt32(nodeVersion["majorVersion"].InnerText),
                        Convert.ToInt32(nodeVersion["minorVersion"].InnerText),
                        Convert.ToInt32(nodeVersion["buildVersion"].InnerText),
                        Convert.ToInt32(nodeVersion["revisionVersion"].InnerText));

                    result.Succeeded = true;
                }
            }
            catch (Exception)
            {
                result.Succeeded = false;
            }

            e.Result = result;
        }

        private void bwVersionCheck_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!e.Cancelled && e.Error == null)
            {
                VersionCheckResult result = (VersionCheckResult)e.Result;
                if (result.Succeeded)
                {
                    Version runningVersion = Assembly.GetExecutingAssembly().GetName().Version;
                    if (runningVersion < result.CurrentVersion)
                    {
                        AddSystemLog(SystemLogType.Notice, GetLanguageText("VersionCheckUpdateFound"));
                    }
                    else
                    {
                        AddSystemLog(SystemLogType.Default, GetLanguageText("VersionCheckNoUpdate"));
                    }
                }
            }
            else
            {
                AddSystemLog(SystemLogType.Error, GetLanguageText("VersionCheckFailed"));
            }
        }

        #endregion

        #region Menu Events

        private void mniCharState_Click(object sender, EventArgs e)
        {
            if (!Helper.IsWindow(m_formCharState))
            {
                m_formCharState = new FormCharState();
                ShowDialogForm(m_formCharState);
            }
        }

        private void mniUpdateAddresses_Click(object sender, EventArgs e)
        {
            try
            {
                AddLog(LogType.Default, GetLanguageText("StartingAddressesUpdate"));

                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri("http://www.kaisnet.de/data/version/rowatch_clients.xml"),
                    Path.Combine(Application.StartupPath, "DB/clients.tmp"));

                client.DownloadFileCompleted += client_DownloadFileCompleted;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), GetLanguageText("ErrorTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void mniAutofeed_Click(object sender, EventArgs e)
        {
            if (!Helper.IsWindow(m_formAutofeed))
            {
                m_formAutofeed = new FormAutofeed(m_processManager, m_charInfo);
                ShowDialogForm(m_formAutofeed);
            }
        }

        private void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                try
                {
                    string tmpPath = Path.Combine(Application.StartupPath, @"DB\clients.tmp");
                    string currentPath = Path.Combine(Application.StartupPath, @"DB\clients.xml");

                    string md51 = Helper.GetFileMD5(tmpPath);
                    string md52 = Helper.GetFileMD5(currentPath);

                    if (md51 != md52)
                    {
                        bool hasReadOnly = false;

                        FileAttributes attributes = File.GetAttributes(currentPath);
                        if ((attributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                        {
                            hasReadOnly = true;

                            if (MessageBox.Show(GetLanguageText("AdressFileIsReadonly").Replace("{path}", currentPath), GetLanguageText("Warning"),
                                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                            {
                                if (File.Exists(tmpPath))
                                {
                                    File.Delete(tmpPath);
                                }

                                return;
                            }

                            File.SetAttributes(currentPath, attributes &= ~FileAttributes.ReadOnly);
                        }

                        File.Copy(tmpPath, currentPath, true);

                        if (hasReadOnly)
                        {
                            File.SetAttributes(currentPath, attributes | FileAttributes.ReadOnly);
                        }

                        if (File.Exists(tmpPath))
                        {
                            File.Delete(tmpPath);
                        }

                        MessageBox.Show(GetLanguageText("AddressesUpdated"), GetLanguageText("InfoTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(GetLanguageText("NoAddressUpdate"), GetLanguageText("InfoTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), GetLanguageText("ErrorTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(GetLanguageText("AddressesUpdateFailed"), GetLanguageText("FailedTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuMisc_Opening(object sender, CancelEventArgs e)
        {
            mniHomunculusWatch.Visible = (m_processManager.IsActive && m_charInfo.SupportHomunculus);

            mniLog.Text = lvwLog.Visible ? GetLanguageText("HideLog") : GetLanguageText("ShowLog");

            mniHideClient.Visible = m_processManager.IsActive;
            mniOptimizeClientSize.Visible = m_processManager.IsActive;

            mniMobSearch.Visible = m_processManager.IsActive;

            mniClearLog.Visible = (lvwLog.Items.Count > 0);

            mniServerLogin.Text = m_client.Connected ? GetLanguageText("Disconnect") : GetLanguageText("Connect");
            mniParty.Visible = (m_formParty == null || m_formParty.IsDisposed || m_formParty.Disposing);

            mniHomunculusWatch.Visible = m_processManager.IsActive && (m_formHomunculus == null || m_formHomunculus.IsDisposed || m_formHomunculus.Disposing);
            mniPetWatch.Visible = m_processManager.IsActive && (m_formPet == null || m_formPet.IsDisposed || m_formPet.Disposing);

            mniCharts.Visible = (m_formCharts == null || m_formCharts.IsDisposed || m_formCharts.Disposing);

            mniExpCounter.Visible = (m_processManager.IsActive && (m_formExpCounter == null || m_formExpCounter.IsDisposed || m_formExpCounter.Disposing));

            mniAutofeed.Visible = (m_processManager.IsActive && m_extendedMode && (m_formAutofeed == null || m_formAutofeed.IsDisposed || m_formAutofeed.Disposing));

            mniCharState.Visible = (m_processManager.IsActive && !Helper.IsWindow(m_formCharState));

            //mniSkillTimer.Enabled = (m_processManager.IsActive);
            //mniMacro.Enabled = (m_processManager.IsActive);
        }

        private void mniMobSearch_Click(object sender, EventArgs e)
        {
            FormMobs dialog = new FormMobs(m_mobDb, m_charInfo, m_settings);
            ShowDialogForm(dialog);
        }

        private void mniCharCalculator_Click(object sender, EventArgs e)
        {
            FormCharCalculator dialog = new FormCharCalculator(m_jobClasses);
            ShowDialogForm(dialog);
        }

        private void mniHideClient_Click(object sender, EventArgs e)
        {
            if (m_processManager.WindowHandle != IntPtr.Zero)
            {
                WinAPI.ShowWindow(m_processManager.WindowHandle, WinAPI.WindowShowStyle.Minimize);
            }
        }

        private void mniOptimizeClientSize_Click(object sender, EventArgs e)
        {
            if (m_processManager.WindowHandle != IntPtr.Zero)
            {
                WinAPI.ShowWindow(m_processManager.WindowHandle, WinAPI.WindowShowStyle.Maximize);
                //WinAPI.MoveWindow(_process.WindowHandle, 0, 0, 640, 480, false);
            }
        }

        private void mniLog_Click(object sender, EventArgs e)
        {
            if (!lvwLog.Visible)
            {
                Size = new Size(400, 300);
                MinimumSize = new Size(400, 300);
                MaximumSize = new Size(400, 600);
            }
            else
            {
                Size = new Size(400, 205);
                MinimumSize = new Size(400, 205);
                MaximumSize = new Size(400, 205);
            }

            lvwLog.Visible = !lvwLog.Visible;
        }

        private void mniClearLog_Click(object sender, EventArgs e)
        {
            lvwLog.Items.Clear();
        }

        private void mniQuickwatch_Click(object sender, EventArgs e)
        {
            if (m_formMinimized == null)
            {
                m_formMinimized = new FormMinimized();
                m_formMinimized.Location = new Point(Left + 50, Top + 50);
                m_formMinimized.OnClose += new EventHandler(_formMinimized_OnClose);
                m_formMinimized.Show(this);
                Visible = false;
            }
        }

        private void _formMinimized_OnClose(object sender, EventArgs e)
        {
            m_formMinimized = null;
            Visible = true;
        }

        private void mniOpenClient_Click(object sender, EventArgs e)
        {
            bool ok = CheckGameDirectory() && CheckGameExePath();
            if (ok)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = m_settings.ClientExeFilename;
                    startInfo.WorkingDirectory = m_settings.GameDirectory;

                    Process.Start(startInfo);
                }
                catch (Exception)
                {
                    MessageBox.Show(GetLanguageText("ClientStartFailed"), GetLanguageText("ErrorTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void mniOpenSetup_Click(object sender, EventArgs e)
        {
            bool ok = CheckGameDirectory() && CheckSetupExePath();
            if (ok)
            {
                try
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = m_settings.SetupExeFilename;
                    startInfo.WorkingDirectory = m_settings.GameDirectory;

                    Process.Start(startInfo);
                }
                catch (Exception)
                {
                    MessageBox.Show(GetLanguageText("SetupStartFailed"), GetLanguageText("ErrorTitle"),
                         MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void mniServerLogin_Click(object sender, EventArgs e)
        {
            if (!m_client.Connected)
            {
                // Connect to Server
                m_client.Connect(IPAddress.Parse(m_settings.PartyServerIP), ServerSettings.Port);
            }
            else
            {
                // Disconnect from Server
                m_client.Disconnect();
            }
        }

        private void mniParty_Click(object sender, EventArgs e)
        {
            if (!Helper.IsWindow(m_formParty))
            {
                m_formParty = new FormParty(m_partyMembers);
                ShowDialogForm(m_formParty);
            }
        }

        private void mniHomunculusWatch_Click(object sender, EventArgs e)
        {
            if (!Helper.IsWindow(m_formHomunculus))
            {
                m_formHomunculus = new FormHomunculus();
                ShowDialogForm(m_formHomunculus);
            }
        }

        private void mniPetWatch_Click(object sender, EventArgs e)
        {
            if (!Helper.IsWindow(m_formPet))
            {
                m_formPet = new FormPet();
                ShowDialogForm(m_formPet);
            }

        }

        private void mniCharts_Click(object sender, EventArgs e)
        {
            if (!Helper.IsWindow(m_formCharts))
            {
                m_formCharts = new FormCharts(m_chartValues);
                ShowDialogForm(m_formCharts);
            }
        }

        private void mniExpCounter_Click(object sender, EventArgs e)
        {
            if (!Helper.IsWindow(m_formExpCounter))
            {
                m_formExpCounter = new FormExpCounter();
                ShowDialogForm(m_formExpCounter);
            }
        }

        private void mniSkillTimer_Click(object sender, EventArgs e)
        {
            using (FormAddSkillTimer dialog = new FormAddSkillTimer(m_skillTimer))
            {
                if (ShowDialogForm(dialog, true) == DialogResult.OK)
                {
                    FormSkillTimer timerDialog = new FormSkillTimer(m_charInfo, m_processManager, dialog.SelectedTimer);
                    ShowDialogForm(timerDialog);
                }
            }
        }

        private void mniMacro_Click(object sender, EventArgs e)
        {
            using (FormMacros dialog = new FormMacros(m_macros))
            {
                if (ShowDialogForm(dialog, true) == DialogResult.OK)
                {
                    foreach (Macro macro in dialog.SelectedMacros)
                    {
                        FormMacroExecution macroDialog = new FormMacroExecution(m_charInfo, m_processManager, macro);

                        macroDialog.CenterForm(this);
                        macroDialog.Show(this);
                    }
                }
            }
        }

        #endregion

        #region Watch Timer

        private void Watch_Start()
        {
            if (!timerWatch.Enabled)
            {
                //
                // select process to watch
                //
                int selectedProcessIndex = 0;

                string processName = Path.GetFileNameWithoutExtension(m_settings.ClientExeFilename);
                if (string.IsNullOrEmpty(processName))
                {
                    processName = "Ragexe";
                }

                Process[] processes = Process.GetProcessesByName(processName);

                if (processes.Length == 0)
                {
                    MessageBox.Show(GetLanguageText("ClientNotFound"), GetLanguageText("FailedTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return;
                }
                else if (processes.Length > 1)
                {
                    using (FormChooseClient dialog = new FormChooseClient())
                    {
                        for (int i = 0; i < processes.Length; i++)
                        {
                            WinAPI.SetWindowText(processes[i].MainWindowHandle, "Ragnarok " + (i + 1));

                            dialog.AddClient("Ragnarok " + (i + 1));
                        }

                        if (dialog.ShowDialog(this) == DialogResult.Cancel)
                        {
                            return;
                        }

                        selectedProcessIndex = dialog.SelectedClient;
                    }
                }

                //
                // load addresses
                //
                try
                {
                    string md5 = HashHelper.ToMD5(File.ReadAllBytes(processes[selectedProcessIndex].MainModule.FileName));

                    Dictionary<string, Addresses> cliens = LoadClients();
                    if (cliens.ContainsKey(md5))
                    {
                        m_settings.Addresses = cliens[md5];
                    }
                    else
                    {
                        MessageBox.Show(GetLanguageText("NotSupportedClient").Replace("{hash}", md5).Replace("{hashes}", cliens.Count.ToString("n0")), GetLanguageText("StopTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);

                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), GetLanguageText("ErrorTitle"),
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //
                // open process
                //
                switch (m_processManager.OpenProcess(processes[selectedProcessIndex]))
                {
                    case OpenProcessResult.Successfull:
                        AddLog(LogType.Default, GetLanguageText("StartWatching"));

                        btnStart.Text = GetLanguageText("StopWatching");
                        btnReset.Enabled = true;

                        Watch_Reset(true);

                        timerWatch.Start();
                        timerExpHour.Start();

                        m_resetWhileCharacterSelection = true;

                        m_chartValues.ResetAll();

                        SendToServerConnected();

                        break;

                    case OpenProcessResult.NoAccess:
                        MessageBox.Show(GetLanguageText("OpenProcessFailed"), GetLanguageText("FailedTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                    case OpenProcessResult.WindowNotFound:
                        MessageBox.Show(GetLanguageText("ClientNotFound"), GetLanguageText("FailedTitle"),
                            MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        break;
                }
            }
            else
            {
                Watch_Stop();
            }
        }

        private void Watch_Stop()
        {
            // stop timer
            timerWatch.Stop();
            timerExpHour.Stop();

            // enable buttons
            btnStart.Text = "Start";
            btnReset.Enabled = false;

            ResetGui();

            SendToServerDisconnected();
        }

        private void Watch_Reset(bool addLogText)
        {
            if (m_processManager.IsActive)
            {
                if (addLogText)
                {
                    AddLog(LogType.Default, GetLanguageText("Reset"));
                }

                m_charInfo = new CharacterInfo();
                m_charInfoLast = new CharacterInfo();
                m_charStatistic = null;

                m_settings.AutoPotions.Key = 0;
                m_settings.AutoWing.Key = 0;
                m_settings.AutoKey.Key = 0;

                m_charInfo.LastKill = null;
                m_charInfo.LastDmg = null;

                m_charInfo.StartTime = DateTime.Now;

                m_chartValues.ResetAll();
                if (Helper.IsWindow(m_formCharts))
                {
                    m_formCharts.RefreshChart();
                }

                RefreshEventImages();

                WatchTimer_ReadCharInfo();
                m_charInfoLast.CopyFrom(m_charInfo);
            }
        }


        private bool WatchTimer_ReadCharInfo()
        {
            bool result;

            try
            {
                m_charInfo.Name = m_processManager.ReadString(m_settings.Addresses.Name, 80);

                m_charInfo.JobClass = (JobClassId)m_processManager.ReadInt32(m_settings.Addresses.JobClass);

                m_charInfo.BaseLevel = m_processManager.ReadInt32(m_settings.Addresses.BaseLevel);
                m_charInfo.JobLevel = m_processManager.ReadInt32(m_settings.Addresses.JobLevel);

                m_charInfo.HP = m_processManager.ReadInt32(m_settings.Addresses.Hp);
                m_charInfo.MaxHP = m_processManager.ReadInt32(m_settings.Addresses.HpMax);

                m_charInfo.SP = m_processManager.ReadInt32(m_settings.Addresses.Sp);
                m_charInfo.MaxSP = m_processManager.ReadInt32(m_settings.Addresses.SpMax);

                m_charInfo.BaseExp = m_processManager.ReadInt32(m_settings.Addresses.BaseExp);
                m_charInfo.BaseExpRequired = m_processManager.ReadInt32(m_settings.Addresses.BaseExpRequired);
                m_charInfo.JobExp = m_processManager.ReadInt32(m_settings.Addresses.JobExp);
                m_charInfo.JobExpRequired = m_processManager.ReadInt32(m_settings.Addresses.JobExpRequired);

                m_charInfo.Map = m_processManager.ReadString(m_settings.Addresses.Map, 150);

                m_charInfo.Zeny = m_processManager.ReadInt32(m_settings.Addresses.Zeny);

                if (m_settings.Addresses.SupportHomunculus)
                {
                    m_charInfo.Homunculus_Name = m_processManager.ReadString(m_settings.Addresses.Homu_Name, 80);
                    m_charInfo.Homunculus_HP = m_processManager.ReadInt32(m_settings.Addresses.Homu_HP);
                    m_charInfo.Homunculus_MaxHP = m_processManager.ReadInt32(m_settings.Addresses.Homu_MaxHP);
                    m_charInfo.Homunculus_Exp = m_processManager.ReadInt32(m_settings.Addresses.Homu_Exp);
                    m_charInfo.Homunculus_MaxExp = m_processManager.ReadInt32(m_settings.Addresses.Homu_MaxExp);
                    m_charInfo.Homunculus_Hungry = m_processManager.ReadInt32(m_settings.Addresses.Homu_Hungry);
                    m_charInfo.Homunculus_Friendly = m_processManager.ReadInt32(m_settings.Addresses.Homu_Friendly);
                }

                if (m_settings.Addresses.SupportPet)
                {
                    m_charInfo.PetName = m_processManager.ReadString(m_settings.Addresses.PetName, 80);
                    m_charInfo.PetFriendly = m_processManager.ReadInt32(m_settings.Addresses.PetFriendly);
                    m_charInfo.PetHungry = m_processManager.ReadInt32(m_settings.Addresses.PetHungry);
                }

                result = true;
            }
            catch (Exception)
            {
                AddLog(LogType.Default, GetLanguageText("ReadMemoryError"));

                Watch_Stop();

                result = false;
            }

            return result;
        }

        private bool WatchTimer_CheckProcess()
        {
            bool result = m_processManager.IsActive;
            if (!m_processManager.IsActive)
            {
                AddLog(LogType.Default, GetLanguageText("ClientClosed"));

                Watch_Stop();
            }

            return result;
        }

        private void WatchTimer_CalculateCompleted()
        {
            string completeExpText = string.Empty;

            int jobClass = (int)m_charInfo.JobClass;
            if (m_expTables.ContainsKey(jobClass))
            {
                ExpTable exp = m_expTables[jobClass];

                if (m_charInfo.BaseLevel > 1 && m_charInfo.BaseLevel - 2 < exp.LevelBase.Count)
                {
                    long baseExpComplete = exp.SumBase;
                    long baseExpHave = exp.LevelBase[m_charInfo.BaseLevel - 2] + m_charInfo.BaseExp;
                    long baseExpLeft = baseExpComplete - baseExpHave;

                    float percent = (float)(baseExpComplete - baseExpLeft) / baseExpComplete;
                    if (percent > 1f)
                    {
                        percent = 1f;
                    }

                    completeExpText = percent.ToString("p2");
                }
            }

            lblCompleted.Text = completeExpText;
        }

        //
        // Schritt 1
        // Vergleich der Daten, Auswertungen.
        //
        private void WatchTimer_Calculate()
        {
            m_charInfo.CharacterSelection = m_charInfo.Map.StartsWith("login", StringComparison.OrdinalIgnoreCase);
            if (m_charInfo.CharacterSelection)
            {
                if (!m_resetWhileCharacterSelection)
                {
                    m_resetWhileCharacterSelection = true;

                    AddLog(LogType.Default, GetLanguageText("CharSelection"));

                    Watch_Reset(true);

                    SendToServerDisconnected();
                }
            }
            else
            {
                // reset after characterselection
                if (m_resetWhileCharacterSelection)
                {
                    m_resetWhileCharacterSelection = false;

                    Watch_Reset(false);

                    WatchTimer_CalculateCompleted();

                    SendToServerConnected();
                }

                // calculate elapsed time
                m_charInfo.ElapsedTime = DateTime.Now.Subtract(m_charInfo.StartTime);

                // check for level up
                m_charInfo.BaseLevelUp = (m_charInfo.BaseLevel > m_charInfoLast.BaseLevel);
                if (m_charInfo.BaseLevelUp)
                {
                    m_charInfo.GainedBaseLevels = m_charInfo.BaseLevel - m_charInfoLast.BaseLevel;
                }
                else
                {
                    m_charInfo.GainedBaseLevels = 0;
                }

                m_charInfo.JobLevelUp = (m_charInfo.JobLevel > m_charInfoLast.JobLevel);
                if (m_charInfo.JobLevelUp)
                {
                    m_charInfo.GainedJobLevels = m_charInfo.JobLevel - m_charInfoLast.JobLevel;
                }
                else
                {
                    m_charInfo.GainedJobLevels = 0;
                }

                // calculate gained exp
                int gainedBaseExp;
                if (m_charInfo.BaseLevelUp)
                {
                    gainedBaseExp = m_charInfoLast.BaseExpRequired - m_charInfoLast.BaseExp + m_charInfo.BaseExp;
                }
                else
                {
                    gainedBaseExp = m_charInfo.BaseExp - m_charInfoLast.BaseExp;
                }

                int gainedJobExp;
                if (m_charInfo.JobLevelUp)
                {
                    gainedJobExp = m_charInfoLast.JobExpRequired - m_charInfoLast.JobExp + m_charInfo.JobExp;
                }
                else
                {
                    gainedJobExp = m_charInfo.JobExp - m_charInfoLast.JobExp;
                }

                m_charInfo.GainedBaseExp += gainedBaseExp;
                m_charInfo.GainedJobExp += gainedJobExp;

                if (gainedBaseExp > 0)
                {
                    //TODO: Move to Logging Method
                    if (m_charInfo.BaseExpRequired > 0)
                    {
                        double difference = m_charInfo.BaseExpRequired - m_charInfo.BaseExp;
                        double killsNeeded = Math.Ceiling(difference / gainedBaseExp);

                        AddLog(LogType.BaseExpGained, GetLanguageText("BaseExpGained"),
                            GetPositivValue(gainedBaseExp),
                            ((float)gainedBaseExp / m_charInfo.BaseExpRequired),
                            killsNeeded);
                    }
                    else
                    {
                        AddLog(LogType.BaseExpGained, GetLanguageText("BaseExpGainedMaxLevel"),
                            GetPositivValue(gainedBaseExp));
                    }

                    if (m_formExpCounter != null && !m_formExpCounter.IsDisposed && !m_formExpCounter.Disposing)
                    {
                        m_formExpCounter.AddExp(gainedBaseExp);
                    }
                }
                else if (gainedBaseExp < 0)
                {
                    //TODO: Move to Logging Method
                    AddLog(LogType.BaseExpLost, GetLanguageText("BaseExpLost"),
                        GetPositivValue(gainedBaseExp));
                }

                if (gainedJobExp > 0)
                {
                    //TODO: Move to Logging Method
                    if (m_charInfo.JobExpRequired > 0)
                    {
                        double difference = m_charInfo.JobExpRequired - m_charInfo.JobExp;
                        double killsNeeded = Math.Ceiling(difference / gainedJobExp);

                        AddLog(LogType.JobExpGained, GetLanguageText("JobExpGained"),
                            GetPositivValue(gainedJobExp),
                            ((float)gainedJobExp / m_charInfo.JobExpRequired),
                            killsNeeded);
                    }
                    else
                    {
                        AddLog(LogType.JobExpGained, GetLanguageText("JobExpGainedMaxLevel"),
                           GetPositivValue(gainedJobExp));
                    }
                }
                else if (gainedJobExp < 0)
                {
                    //TODO: Move to Logging Method
                    AddLog(LogType.JobExpLost, GetLanguageText("JobExpLost"),
                        GetPositivValue(gainedJobExp));
                }

                // refresh base exp
                if (m_charInfo.BaseExpRequired > 0)
                {
                    m_charInfo.LeftBaseExp = m_charInfo.BaseExpRequired - m_charInfo.BaseExp;
                    m_charInfo.LeftBaseExpPercent = 1f - ((float)m_charInfo.BaseExp / m_charInfo.BaseExpRequired);

                    m_charInfo.BaseExpPercent = (float)m_charInfo.BaseExp / m_charInfo.BaseExpRequired;
                }
                else
                {
                    m_charInfo.LeftBaseExp = 0;
                    m_charInfo.LeftBaseExpPercent = 0f;
                    m_charInfo.BaseExpPercent = 1f;
                }

                m_charInfo.BaseExpPerHour = (int)(m_charInfo.GainedBaseExp / m_charInfo.ElapsedTime.TotalHours);
                m_charInfo.BasePercentPerHour = (float)m_charInfo.BaseExpPerHour / m_charInfo.BaseExpRequired;

                // left base time
                if (m_charInfo.BaseExpPerHour > 0)
                {
                    int expPerSecond = ((m_charInfo.BaseExpPerHour / 60) / 60);
                    double requiredSeconds = (double)m_charInfo.LeftBaseExp / expPerSecond;

                    if (requiredSeconds < TimeSpan.MaxValue.TotalSeconds)
                    {
                        m_charInfo.LeftBaseTime = TimeSpan.FromSeconds(requiredSeconds);
                    }
                }
                else
                {
                    m_charInfo.LeftBaseTime = TimeSpan.Zero;
                }

                // refresh job exp
                if (m_charInfo.JobExpRequired > 0)
                {
                    m_charInfo.LeftJobExp = m_charInfo.JobExpRequired - m_charInfo.JobExp;
                    m_charInfo.LeftJobExpPercent = 1f - ((float)m_charInfo.JobExp / m_charInfo.JobExpRequired);

                    m_charInfo.JobExpPercent = (float)m_charInfo.JobExp / m_charInfo.JobExpRequired;
                }
                else
                {
                    m_charInfo.LeftJobExp = 0;
                    m_charInfo.LeftJobExpPercent = 0f;
                    m_charInfo.JobExpPercent = 1f;
                }

                m_charInfo.JobExpPerHour = (int)(m_charInfo.GainedJobExp / m_charInfo.ElapsedTime.TotalHours);
                m_charInfo.JobPercentPerHour = (float)m_charInfo.JobExpPerHour / m_charInfo.JobExpRequired;

                // left job time
                if (m_charInfo.JobExpPerHour > 0)
                {
                    int expPerSecond = ((m_charInfo.JobExpPerHour / 60) / 60);
                    double requiredSeconds = (double)m_charInfo.LeftJobExp / expPerSecond;

                    if (requiredSeconds < TimeSpan.MaxValue.TotalSeconds)
                    {
                        m_charInfo.LeftJobTime = TimeSpan.FromSeconds(requiredSeconds);
                    }
                }
                else
                {
                    m_charInfo.LeftJobTime = TimeSpan.Zero;
                }

                // mob kill counter
                if (gainedBaseExp > 0 || gainedJobExp > 0)
                {
                    m_charInfo.KilledMobs++;
                    m_charInfo.LastKill = DateTime.Now;
                }

                m_charInfo.KilledMobsHour = (int)Math.Round(m_charInfo.KilledMobs / m_charInfo.ElapsedTime.TotalHours, MidpointRounding.AwayFromZero);

                // calculate zeny
                m_charInfo.EarnedZeny = m_charInfo.Zeny - m_charInfoLast.Zeny;

                // calculate hp/sp
                if (!m_charInfo.BaseLevelUp)
                {
                    m_charInfo.GainedHP = m_charInfo.HP - m_charInfoLast.HP;
                    m_charInfo.GainedSP = m_charInfo.SP - m_charInfoLast.SP;
                }
                else
                {
                    m_charInfo.GainedHP = 0;
                    m_charInfo.GainedSP = 0;
                }

                if (m_charInfo.GainedHP != 0 && m_charInfo.GainedHP < 0)
                {
                    m_charInfo.LastDmg = DateTime.Now;
                }
                else if (m_charInfo.SupportHomunculus
                    && m_charInfoLast.Homunculus_HP > m_charInfo.Homunculus_HP)
                {
                    m_charInfo.LastDmg = DateTime.Now;
                }

                if (m_charInfo.Map != m_charInfoLast.Map)
                {
                    //TODO: Move to Logging Method
                    AddLog(LogType.MapChange, GetLanguageText("MapChanged"),
                        GetMapname(m_charInfo.Map));
                }

                m_charInfo.HPPercent = (int)(((float)m_charInfo.HP / m_charInfo.MaxHP) * 100);
                m_charInfo.SPPercent = (int)(((float)m_charInfo.SP / m_charInfo.MaxSP) * 100);

                if (m_charStatistic != null)
                {
                    m_charStatistic.Refresh(
                        m_charInfo, 25);
                }

                if (m_charInfoLast.JobClass != m_charInfo.JobClass
                    || m_charInfoLast.BaseExp != m_charInfo.BaseExp
                    || m_charInfoLast.JobExp != m_charInfo.JobExp
                    || m_charInfoLast.BaseLevel != m_charInfo.BaseLevel
                    || m_charInfoLast.JobLevel != m_charInfo.JobLevel)
                {
                    WatchTimer_CalculateCompleted();
                }

                // homunculus
                int gainedHomunculusExp = 0;
                if (m_charInfo.SupportHomunculus)
                {
                    m_charInfo.Homunculus_PercentageHP = Helper.GetPercentInteger(m_charInfo.Homunculus_HP, m_charInfo.Homunculus_MaxHP);
                    m_charInfo.Homunculus_PercentageFriendly = Helper.GetPercentInteger(m_charInfo.Homunculus_Friendly, 1000);
                    m_charInfo.Homunculus_PercentageHungry = Helper.GetPercentInteger(m_charInfo.Homunculus_Hungry, 100);
                    m_charInfo.Homunculus_PercentageExp = Helper.GetPercent(m_charInfo.Homunculus_Exp, m_charInfo.Homunculus_MaxExp);

                    gainedHomunculusExp = m_charInfo.Homunculus_Exp - m_charInfoLast.Homunculus_Exp;
                    if (gainedHomunculusExp > 0)
                    {
                        double difference = m_charInfo.Homunculus_MaxExp - m_charInfo.Homunculus_Exp;
                        double killsNeeded = Math.Ceiling(difference / gainedHomunculusExp);

                        AddLog(LogType.Homunculus, GetLanguageText("HomExpGained"),
                            GetPositivValue(gainedHomunculusExp),
                            ((float)gainedHomunculusExp / m_charInfo.Homunculus_MaxExp),
                            killsNeeded);
                    }
                    else
                    {
                        gainedHomunculusExp = 0;
                    }

                    // feed alarm
                    if (m_settings.HomunculusHungryWarnSound)
                    {
                        if (m_charInfo.Homunculus_PercentageHungry < m_settings.HomunculusHungryMinValue)
                        {
                            if (!m_homFeedAlarmSound.IsPlaying)
                            {
                                m_homFeedAlarmSound.Play(true);
                            }
                        }
                        else
                        {
                            if (m_homFeedAlarmSound.IsPlaying)
                            {
                                m_homFeedAlarmSound.Stop();
                            }
                        }
                    }

                    if (m_settings.HomunculusHungryCloseClient)
                    {
                        if (m_charInfo.Homunculus_PercentageHungry < m_settings.HomunculusHungryMinValue
                            && WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
                        {
                            WinAPI.SendMessage(m_processManager.WindowHandle, 0x10, 0, 0);
                        }
                    }
                }

                if (m_formPet != null && !m_formPet.IsDisposed && !m_formPet.Disposing)
                {
                    if (m_settings.Addresses.SupportPet && m_settings.PetFeedAlarmSound)
                    {
                        if (m_charInfo.PetHungry <= m_settings.PetMinimumHungryValue)
                        {
                            if (!m_petFeedAlarmSound.IsPlaying)
                            {
                                m_petFeedAlarmSound.Play(true);
                            }
                        }
                        else
                        {
                            if (m_petFeedAlarmSound.IsPlaying)
                            {
                                m_petFeedAlarmSound.Stop();
                            }
                        }
                    }
                }

                // charts
                if (m_chartValues.LastMinute != DateTime.Now.Minute)
                {
                    m_chartValues.LastMinute = DateTime.Now.Minute;

                    // base exp / minute
                    m_chartValues.GainedBaseExpValues.Add(m_chartValues.GainedBaseExp);
                    m_chartValues.GainedJobExpValues.Add(m_chartValues.GainedJobExp);
                    m_chartValues.KilledMobsValues.Add(m_chartValues.KilledMobs);
                    m_chartValues.GainedHomunculusExpValues.Add(m_chartValues.GainedHomunculusExp);

                    m_chartValues.ResetLastValues();

                    if (m_formCharts != null && !m_formCharts.IsDisposed && !m_formCharts.Disposing)
                    {
                        m_formCharts.RefreshChart();
                    }
                }
                else
                {
                    m_chartValues.GainedBaseExp += gainedBaseExp;
                    m_chartValues.GainedJobExp += gainedJobExp;

                    if (gainedBaseExp > 0 || gainedJobExp > 0)
                    {
                        m_chartValues.KilledMobs++;
                    }

                    m_chartValues.GainedHomunculusExp += gainedHomunculusExp;
                }
            }
        }

        //
        // Schritt 2
        // Log-Einträge hinzufügen.
        //
        private void WatchTimer_Loging()
        {
            // base / job level up
            if (m_charInfo.BaseLevelUp && m_charInfo.GainedBaseLevels > 0)
            {
                AddLog(LogType.BaseLevelUp, GetLanguageText("BaseLevelUP"),
                    m_charInfoLast.BaseLevel,
                    m_charInfo.BaseLevel);
            }

            if (m_charInfo.JobLevelUp && m_charInfo.GainedJobLevels > 0)
            {
                AddLog(LogType.JobLevelUp, GetLanguageText("JobLevelUP"),
                    m_charInfoLast.JobLevel,
                    m_charInfo.JobLevel);
            }

            // zeny
            if (m_charInfo.EarnedZeny > 0)
            {
                if (m_settings.CashRegisterSound_Enabled)
                {
                    bool playSound = true;

                    if (m_settings.CashRegisterSound_MinimumZeny != 0 && m_charInfo.EarnedZeny < m_settings.CashRegisterSound_MinimumZeny)
                    {
                        playSound = false;
                    }
                    if (m_settings.CashRegisterSound_MaximumZeny != 0 && m_charInfo.EarnedZeny > m_settings.CashRegisterSound_MaximumZeny)
                    {
                        playSound = false;
                    }

                    if (playSound)
                    {
                        m_cashRegisterSound.Play(false);
                    }
                }

                AddLog(LogType.ZenyGained, GetLanguageText("ZenyEarned"),
                    GetPositivValue(m_charInfo.EarnedZeny));
            }
            else if (m_charInfo.EarnedZeny < 0)
            {
                AddLog(LogType.ZenyLost, GetLanguageText("ZenyLost"),
                    GetPositivValue(m_charInfo.EarnedZeny));
            }

            // hp / sp
            if (!m_charInfo.BaseLevelUp && !m_charInfo.JobLevelUp)
            {
                if (m_charInfo.GainedHP > 0)
                {
                    AddLog(LogType.HpGained, GetLanguageText("HPGained"),
                        GetPositivValue(m_charInfo.GainedHP));
                }
                else if (m_charInfo.GainedHP < 0)
                {
                    AddLog(LogType.HpLost, GetLanguageText("HPLost"),
                        GetPositivValue(m_charInfo.GainedHP));
                }

                if (m_charInfo.GainedSP > 0)
                {
                    AddLog(LogType.SpGained, GetLanguageText("SPGained"),
                        GetPositivValue(m_charInfo.GainedSP));
                }
                else if (m_charInfo.GainedSP < 0)
                {
                    AddLog(LogType.SpLost, GetLanguageText("SPLost"),
                        GetPositivValue(m_charInfo.GainedSP));
                }
            }

            // homunculus
            if (m_settings.Addresses.SupportHomunculus)
            {
            }

            // pet
            if (m_settings.Addresses.SupportPet)
            {
            }
        }

        //
        // Schritt 3
        // Oberfläche aktualisieren.
        //
        private void WatchTimer_RefreshGui()
        {
            if (m_charInfo.CharacterSelection)
            {
                SetLabelText(lblName, "Character-Selection");
                SetLabelText(lblCompleted, string.Empty);
                SetLabelText(lblTime, string.Empty);

                SetLabelText(lblKilledMobs, string.Empty);

                prgBaseEXP.Value = 0;
                SetLabelText(lblBaseExpValues, string.Empty);
                SetLabelText(lblBasePercent, string.Empty);

                prgJobExp.Value = 0;
                SetLabelText(lblJobExpValues, string.Empty);
                SetLabelText(lblJobPercent, string.Empty);

                SetLabelText(lblGainedBaseExp, string.Empty);
                SetLabelText(lblGainedJobExp, string.Empty);

                SetLabelText(lblRequiredBaseExp, string.Empty);
                SetLabelText(lblRequiredJobExp, string.Empty);

                if (m_formMinimized != null)
                {
                    m_formMinimized.BaseExpValues = string.Empty;
                    m_formMinimized.BaseExpPercent = string.Empty;
                    m_formMinimized.JobExpValues = string.Empty;
                    m_formMinimized.JobExpPercent = string.Empty;

                    m_formMinimized.GainedBaseExp = string.Empty;
                    m_formMinimized.GainedJobExp = string.Empty;

                    m_formMinimized.RequiredBaseExp = string.Empty;
                    m_formMinimized.RequiredJobExp = string.Empty;
                }

                if (Helper.IsWindow(m_formHomunculus))
                {
                    m_formHomunculus.ResetValues();
                }
                if (Helper.IsWindow(m_formPet))
                {
                    m_formPet.ResetValues();
                }
                if (Helper.IsWindow(m_formCharState))
                {
                    m_formCharState.ResetValues();
                }
            }
            else
            {
                WatchTimer_SendChangedProperties();

                string name = string.Format("{0} {1} {2}/{3} ({4})",
                    m_charInfo.JobClass.ToString(),
                    m_charInfo.Name.Replace("_", " "),
                    m_charInfo.BaseLevel,
                    m_charInfo.JobLevel,
                    GetMapname(m_charInfo.Map));

                string time = string.Format("{0:00}:{1:00}:{2:00}",
                    m_charInfo.ElapsedTime.Hours,
                    m_charInfo.ElapsedTime.Minutes,
                    m_charInfo.ElapsedTime.Seconds);

                string baseExpValues = string.Format("{0:n0}/{1:n0}",
                    m_charInfo.BaseExp,
                    m_charInfo.BaseExpRequired);

                string baseExpPercent = m_charInfo.BaseExpPercent.ToString("p2");

                string jobExpValues = string.Format("{0:n0}/{1:n0}",
                    m_charInfo.JobExp,
                    m_charInfo.JobExpRequired);

                string jobExpPercent = m_charInfo.JobExpPercent.ToString("p2");

                string gainedBaseExp = m_charInfo.GainedBaseExp.ToString("n0");
                string gainedJobExp = m_charInfo.GainedJobExp.ToString("n0");

                string requiredBaseExp = string.Format("{0:n0} ({1:p2})",
                    m_charInfo.LeftBaseExp,
                    m_charInfo.LeftBaseExpPercent);

                string requiredJobExp = string.Format("{0:n0} ({1:p2})",
                    m_charInfo.LeftJobExp,
                    m_charInfo.LeftJobExpPercent);

                // refresh
                SetLabelText(lblName, name);

                SetLabelText(lblTime, time);

                SetLabelText(lblBaseExpValues, baseExpValues);
                SetLabelText(lblBasePercent, baseExpPercent);

                int baseExpPercentFull = Convert.ToInt32(m_charInfo.BaseExpPercent * 100);
                if (baseExpPercentFull >= 0 && baseExpPercentFull <= 100)
                {
                    prgBaseEXP.Value = baseExpPercentFull;
                }

                SetLabelText(lblJobExpValues, jobExpValues);
                SetLabelText(lblJobPercent, jobExpPercent);

                int jobExpPercentFull = Convert.ToInt32(m_charInfo.JobExpPercent * 100);
                if (jobExpPercentFull >= 0 && jobExpPercentFull <= 100)
                {
                    prgJobExp.Value = jobExpPercentFull;
                }

                SetLabelText(lblGainedBaseExp, gainedBaseExp);
                SetLabelText(lblGainedJobExp, gainedJobExp);

                SetLabelText(lblRequiredBaseExp, requiredBaseExp);
                SetLabelText(lblRequiredJobExp, requiredJobExp);

                if (m_formMinimized != null)
                {
                    m_formMinimized.BaseExpValues = baseExpValues;
                    m_formMinimized.BaseExpPercent = baseExpPercent;
                    m_formMinimized.JobExpValues = jobExpValues;
                    m_formMinimized.JobExpPercent = jobExpPercent;

                    m_formMinimized.GainedBaseExp = gainedBaseExp;
                    m_formMinimized.GainedJobExp = gainedJobExp;

                    m_formMinimized.RequiredBaseExp = requiredBaseExp;
                    m_formMinimized.RequiredJobExp = requiredJobExp;
                }

                // homunculus
                if (Helper.IsWindow(m_formHomunculus))
                {
                    if (!m_charInfo.SupportHomunculus)
                    {
                        m_formHomunculus.ResetValues();
                    }
                    else
                    {
                        m_formHomunculus.Homunculus_Name = m_charInfo.Homunculus_Name;

                        m_formHomunculus.Homunculus_Hp = m_charInfo.Homunculus_PercentageHP;
                        m_formHomunculus.Homunculus_HpText = string.Format("{0:n0} / {1:n0}",
                            m_charInfo.Homunculus_HP, m_charInfo.Homunculus_MaxHP);

                        m_formHomunculus.Homunculus_Exp = (int)(m_charInfo.Homunculus_PercentageExp * 100);

                        if (m_settings.HomunculusRawExp)
                        {
                            m_formHomunculus.Homunculus_ExpText = string.Format("{0:n0} / {1:n0}",
                                m_charInfo.Homunculus_Exp, m_charInfo.Homunculus_MaxExp);
                        }
                        else
                        {
                            m_formHomunculus.Homunculus_ExpText = m_charInfo.Homunculus_PercentageExp.ToString("p2");
                        }

                        m_formHomunculus.Homunculus_Friendly = m_charInfo.Homunculus_PercentageFriendly;
                        m_formHomunculus.Homunculus_FriendlyText = string.Format("{0:n0} / {1:n0}",
                            m_charInfo.Homunculus_Friendly, 1000);

                        m_formHomunculus.Homunculus_Hungry = m_charInfo.Homunculus_PercentageHungry;
                        m_formHomunculus.Homunculus_HungryText = string.Format("{0:n0} / {1:n0}",
                            m_charInfo.Homunculus_Hungry, 100);
                    }
                }

                if (Helper.IsWindow(m_formPet))
                {
                    m_formPet.PetName = m_charInfo.PetName;
                    m_formPet.PetFriendly = m_charInfo.PetFriendly;
                    m_formPet.PetHungry = m_charInfo.PetHungry;
                }

                if (Helper.IsWindow(m_formCharState))
                {
                    m_formCharState.MaxHP = m_charInfo.MaxHP;
                    m_formCharState.HP = m_charInfo.HP;
                    m_formCharState.MaxSP = m_charInfo.MaxSP;
                    m_formCharState.SP = m_charInfo.SP;
                }
            }
        }

        //
        // Party
        // Sendet die veränderten Werte zum Server.
        // Wird in WatchTimer_Calculate() aufgerufen.
        //
        private void WatchTimer_SendChangedProperties()
        {
            // char name
            if (m_charInfo.Name != m_charInfoLast.Name)
            {
                SendToServerPropertieChanged(ChangedPropertieType.Name, m_charInfo.Name);
            }

            // job class
            if (m_charInfo.JobClass != m_charInfoLast.JobClass)
            {
                SendToServerPropertieChanged(ChangedPropertieType.CharClass, ((int)m_charInfo.JobClass).ToString());
            }

            // map
            if (m_charInfo.Map != m_charInfoLast.Map)
            {
                SendToServerPropertieChanged(ChangedPropertieType.Map, GetMapname(m_charInfo.Map));
            }

            // hp
            if (m_charInfo.HP != m_charInfoLast.HP)
            {
                SendToServerPropertieChanged(ChangedPropertieType.Hp, m_charInfo.HP.ToString());
            }
            if (m_charInfo.MaxHP != m_charInfoLast.MaxHP)
            {
                SendToServerPropertieChanged(ChangedPropertieType.MaxHp, m_charInfo.MaxHP.ToString());
            }

            // sp
            if (m_charInfo.SP != m_charInfoLast.SP)
            {
                SendToServerPropertieChanged(ChangedPropertieType.Sp, m_charInfo.SP.ToString());
            }
            if (m_charInfo.MaxSP != m_charInfoLast.MaxSP)
            {
                SendToServerPropertieChanged(ChangedPropertieType.MaxSp, m_charInfo.MaxSP.ToString());
            }

            // level
            if (m_charInfo.BaseLevel != m_charInfoLast.BaseLevel)
            {
                SendToServerPropertieChanged(ChangedPropertieType.BaseLevel, m_charInfo.BaseLevel.ToString());
            }
            if (m_charInfo.JobLevel != m_charInfoLast.JobLevel)
            {
                SendToServerPropertieChanged(ChangedPropertieType.JobLevel, m_charInfo.JobLevel.ToString());
            }

            // base exp
            if (m_charInfo.BaseExp != m_charInfoLast.BaseExp)
            {
                SendToServerPropertieChanged(ChangedPropertieType.BaseExp, m_charInfo.BaseExp.ToString());
            }
            if (m_charInfo.BaseExpRequired != m_charInfoLast.BaseExpRequired)
            {
                SendToServerPropertieChanged(ChangedPropertieType.BaseExpRequired, m_charInfo.BaseExpRequired.ToString());
            }

            // job exp
            if (m_charInfo.JobExp != m_charInfoLast.JobExp)
            {
                SendToServerPropertieChanged(ChangedPropertieType.JobExp, m_charInfo.JobExp.ToString());
            }
            if (m_charInfo.JobExpRequired != m_charInfoLast.JobExpRequired)
            {
                SendToServerPropertieChanged(ChangedPropertieType.JobExpRequired, m_charInfo.JobExpRequired.ToString());
            }

            // killed mobs
            if (m_charInfo.KilledMobs != m_charInfoLast.KilledMobs)
            {
                SendToServerPropertieChanged(ChangedPropertieType.KilledMobs, m_charInfoLast.KilledMobs.ToString());
            }
        }


        private void timerWatch_Tick(object sender, EventArgs e)
        {
            try
            {
                // check process
                if (!WatchTimer_CheckProcess())
                {
                    return;
                }

                // read data
                if (!WatchTimer_ReadCharInfo())
                {
                    return;
                }

                // process any things
                WatchTimer_Calculate();
                WatchTimer_Loging();
                WatchTimer_RefreshGui();

                #region Screenshots

                if (m_charInfo.BaseLevel >= m_settings.MinLevelScreenshots)
                {
                    if (m_settings.BaseupScreenshot && m_charInfo.BaseLevelUp)
                    {
                        if (WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
                        {
                            AddLog(LogType.Default, GetLanguageText("CreateScreenshot"));

                            TakeScreenshot(m_settings.BaseupScreenshotDelay);
                        }
                    }
                    else if (m_settings.JobupScreenshot && m_charInfo.JobLevelUp)
                    {
                        if (WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
                        {
                            AddLog(LogType.Default, GetLanguageText("CreateScreenshot"));

                            TakeScreenshot(m_settings.JobupScreenshotDelay);
                        }
                    }
                }

                #endregion

                #region Auto Potions

                if (m_settings.AutoPotions.Key > 0 && !m_charInfo.CharacterSelection)
                {
                    if (!timerAutopotions.Enabled
                        && m_charInfo.HPPercent <= m_settings.AutoPotions.MinimumPercent)
                    {
                        AddLog(LogType.Default, "Start Autoheal");

                        timerAutopotions.Interval = m_settings.AutoPotions.Delay;
                        timerAutopotions.Enabled = true;
                    }
                }

                #endregion

                #region Auto Wing

                if (m_settings.AutoWing.Key > 0 && !m_charInfo.CharacterSelection)
                {
                    int seconds = 0;
                    switch (m_settings.AutoWing.AutowingCondition)
                    {
                        case AutowingCondition.AfterTime:
                            seconds = (int)DateTime.Now.Subtract(m_settings.AutoWing.LastUse).TotalSeconds;
                            break;

                        case AutowingCondition.NoDmg:
                            if (m_charInfo.LastDmg.HasValue)
                            {
                                seconds = (int)DateTime.Now.Subtract(m_charInfo.LastDmg.Value).TotalSeconds;
                            }
                            break;

                        case AutowingCondition.NoKill:
                            if (m_charInfo.LastKill.HasValue)
                            {
                                seconds = (int)DateTime.Now.Subtract(m_charInfo.LastKill.Value).TotalSeconds;
                            }
                            break;
                    }

                    if (seconds > m_settings.AutoWing.TimeSeconds)
                    {
                        UseFlyWing();
                    }
                }

                #endregion

                #region Auto Key

                if (m_settings.AutoKey.Key > 0 && !m_charInfo.CharacterSelection)
                {
                    int miliseconds = (int)DateTime.Now.Subtract(m_settings.AutoKey.LastUse).TotalMilliseconds;
                    if (miliseconds >= m_settings.AutoKey.Interval)
                    {
                        UseAutoKey();
                    }
                }

                #endregion

                #region Call Plugin Methods

                RefreshPluginCharInformations();
                foreach (PluginInfo plugin in m_plugins)
                {
                    if (!plugin.Disabled)
                    {
                        try
                        {
                            plugin.Plugin.Tick(m_pluginCharInfos);
                        }
                        catch (Exception)
                        {
                            plugin.Disabled = true;
                        }
                    }
                }

                #endregion

                // remember last values
                m_charInfoLast.CopyFrom(m_charInfo);

            }
            catch (Exception ex)
            {
                Watch_Stop();

                MessageBox.Show(ex.ToString(), GetLanguageText("ErrorTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void RefreshPluginCharInformations()
        {
            if (m_pluginCharInfos == null)
            {
                m_pluginCharInfos = new CharacterInformations();
            }

            m_pluginCharInfos.Name = m_charInfo.Name;

            m_pluginCharInfos.Job = (Job)(int)m_charInfo.JobClass;

            m_pluginCharInfos.HP = m_charInfo.HP;
            m_pluginCharInfos.MaxHP = m_charInfo.MaxHP;

            m_pluginCharInfos.SP = m_charInfo.SP;
            m_pluginCharInfos.MaxSP = m_charInfo.MaxSP;

            m_pluginCharInfos.BaseLevel = m_charInfo.BaseLevel;
            m_pluginCharInfos.JobLevel = m_charInfo.JobLevel;

            m_pluginCharInfos.BaseExp = m_charInfo.BaseExp;
            m_pluginCharInfos.BaseExpRequired = m_charInfo.BaseExpRequired;

            m_pluginCharInfos.JobExp = m_charInfo.JobExp;
            m_pluginCharInfos.JobExpRequired = m_charInfo.JobExpRequired;

            m_pluginCharInfos.Zeny = m_charInfo.Zeny;

            m_pluginCharInfos.Map = m_charInfo.Map;
        }


        private void timerAutopotions_Tick(object sender, EventArgs e)
        {
            if (!m_charInfo.CharacterSelection
                && m_charInfo.HPPercent < m_settings.AutoPotions.MaximumPercent)
            {
                if (WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
                {
                    UseHealPotion();
                }
            }
            else
            {
                timerAutopotions.Enabled = false;
            }
        }

        private void timerExpHour_Tick(object sender, EventArgs e)
        {
            if (!m_charInfo.CharacterSelection)
            {
                string killedMobs = string.Format(GetLanguageText("KilledMobs"),
                    m_charInfo.KilledMobs,
                    m_charInfo.KilledMobsHour);

                string baseExpHour = string.Empty;
                if (m_charInfo.BaseExpRequired > 0)
                {
                    baseExpHour = string.Format("{0:n0} / {1:p2}",
                        m_charInfo.BaseExpPerHour,
                        m_charInfo.BasePercentPerHour);
                }
                else
                {
                    baseExpHour = m_charInfo.BaseExpPerHour.ToString("n0");
                }

                string jobExpHour = string.Empty;
                if (m_charInfo.JobExpRequired > 0)
                {
                    jobExpHour = string.Format("{0:n0} / {1:p2}",
                        m_charInfo.JobExpPerHour,
                        m_charInfo.JobPercentPerHour);
                }
                else
                {
                    jobExpHour = m_charInfo.JobExpPerHour.ToString("n0");
                }

                string leftBaseTime;
                if (m_charInfo.LeftBaseTime.Days > 0)
                {
                    leftBaseTime = string.Format("{0:00}.{1:00}:{2:00}:{3:00}",
                        m_charInfo.LeftBaseTime.Days,
                        m_charInfo.LeftBaseTime.Hours,
                        m_charInfo.LeftBaseTime.Minutes,
                        m_charInfo.LeftBaseTime.Seconds);
                }
                else
                {
                    leftBaseTime = string.Format("{0:00}:{1:00}:{2:00}",
                        m_charInfo.LeftBaseTime.Hours,
                        m_charInfo.LeftBaseTime.Minutes,
                        m_charInfo.LeftBaseTime.Seconds);
                }

                string leftJobTime;
                if (m_charInfo.LeftJobTime.Days > 0)
                {
                    leftJobTime = string.Format("{0:00}.{1:00}:{2:00}:{3:00}",
                        m_charInfo.LeftJobTime.Days,
                        m_charInfo.LeftJobTime.Hours,
                        m_charInfo.LeftJobTime.Minutes,
                        m_charInfo.LeftJobTime.Seconds);
                }
                else
                {
                    leftJobTime = string.Format("{0:00}:{1:00}:{2:00}",
                        m_charInfo.LeftJobTime.Hours,
                        m_charInfo.LeftJobTime.Minutes,
                        m_charInfo.LeftJobTime.Seconds);
                }

                // refresh
                SetLabelText(lblKilledMobs, killedMobs);

                SetLabelText(lblBaseExpHour, baseExpHour);
                SetLabelText(lblJobExpHour, jobExpHour);

                SetLabelText(lblRequiredBaseTime, leftBaseTime);
                SetLabelText(lblRequiredJobTime, leftJobTime);

                if (m_formMinimized != null)
                {
                    m_formMinimized.KilledMobs = killedMobs;

                    m_formMinimized.BaseExpHour = baseExpHour;
                    m_formMinimized.JobExpHour = jobExpHour;
                }
            }
            else
            {
                SetLabelText(lblKilledMobs, string.Empty);

                SetLabelText(lblBaseExpHour, string.Empty);
                SetLabelText(lblJobExpHour, string.Empty);

                SetLabelText(lblRequiredBaseTime, string.Empty);
                SetLabelText(lblRequiredJobTime, string.Empty);

                if (m_formMinimized != null)
                {
                    m_formMinimized.KilledMobs = string.Empty;

                    m_formMinimized.BaseExpHour = string.Empty;
                    m_formMinimized.JobExpHour = string.Empty;
                }
            }
        }

        private void timerPartyServer_Tick(object sender, EventArgs e)
        {
            if (m_client != null)
            {
                NetworkMessage message = m_client.GetAsyncMessage();
                if (message != null)
                {
                    switch (message.Type)
                    {
                        case NetworkMessageType.Connected:
                            m_partyMembers.Clear();
                            m_partMembersIndex.Clear();

                            SendToServerConnected();

                            AddLog(LogType.Server, GetLanguageText("ConnectSuccessfull"));

                            break;

                        case NetworkMessageType.ConnectionFailed:
                            AddLog(LogType.Server, GetLanguageText("ConnectFailed"));
                            break;

                        case NetworkMessageType.Disconnected:
                            m_partyMembers.Clear();
                            m_partMembersIndex.Clear();

                            SendToServerDisconnected();

                            AddLog(LogType.Server, GetLanguageText("ConnectionLost"));

                            break;

                        case NetworkMessageType.FormatedMessage:
                            HandleMessage(message);
                            break;
                    }
                }
            }
        }

        #endregion

        #region Methods

        private void CheckVersion()
        {
            if (!bwVersionCheck.IsBusy)
            {
                bwVersionCheck.RunWorkerAsync();
            }
        }

        private DialogResult ShowDialogForm(Form dialog, bool modal = false)
        {
            dialog.FormClosing += dialog_FormClosing;

            if (dialog.StartPosition == FormStartPosition.Manual)
            {
                if (m_settings.WindowPositions.ContainsKey(dialog.Name))
                {
                    Rectangle winrect = m_settings.WindowPositions[dialog.Name];
                    dialog.Location = winrect.Location;
                    dialog.Size = winrect.Size;

                    Screen screen = Screen.FromHandle(dialog.Handle);

                    if (dialog.Left < 0)
                    {
                        dialog.Left = 0;
                    }
                    if (dialog.Top < 0)
                    {
                        dialog.Top = 0;
                    }

                    if (dialog.Left + dialog.Width > screen.WorkingArea.Width)
                    {
                        dialog.Left = screen.Bounds.Width - dialog.Width;
                    }
                    if (dialog.Top + dialog.Height > screen.Bounds.Height)
                    {
                        dialog.Top = screen.Bounds.Height - dialog.Height;
                    }
                }
                else
                {
                    dialog.CenterForm(this);
                }
            }

            if (!modal)
            {
                dialog.Show(this);
                return DialogResult.OK;
            }
            else
            {
                return dialog.ShowDialog(this);
            }
        }

        private void dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form dialog = (Form)sender;

            if (m_settings.WindowPositions.ContainsKey(dialog.Name))
            {
                m_settings.WindowPositions[dialog.Name] = new Rectangle(dialog.Left, dialog.Top, dialog.Width, dialog.Height);
            }
            else
            {
                m_settings.WindowPositions.Add(dialog.Name,
                    new Rectangle(dialog.Left, dialog.Top, dialog.Width, dialog.Height));
            }
        }

        private void LoadMobDb()
        {
            m_mobDb = new List<Mob>(10000);
            try
            {
                using (StreamReader reader = new StreamReader(m_paths.MobDbFile))
                {
                    string currentLine;
                    while (!reader.EndOfStream)
                    {
                        currentLine = reader.ReadLine();
                        if (!string.IsNullOrEmpty(currentLine))
                        {
                            currentLine = currentLine.Trim();
                            if (!currentLine.StartsWith("//"))
                            {
                                Mob mob = Mob.Load(currentLine);
                                if (mob != null)
                                {
                                    m_mobDb.Add(mob);
                                }
                            }
                        }
                    }

                }
            }
            catch (Exception)
            {
                AddLog(LogType.Default, GetLanguageText("MobDBLoadFailed"));
                return;
            }

            AddLog(LogType.Default, GetLanguageText("MobDBLoaded"),
                m_mobDb.Count);
        }

        private void LoadMapDb()
        {
            m_mapDb = new Dictionary<string, string>(1000);
            try
            {
                using (StreamReader reader = new StreamReader(m_paths.MapDbFile))
                {
                    string currentLine;
                    while (!reader.EndOfStream)
                    {
                        currentLine = reader.ReadLine();
                        if (!string.IsNullOrEmpty(currentLine))
                        {
                            currentLine = currentLine.Trim();
                            if (!currentLine.StartsWith("//") && currentLine.IndexOf('=') > 0)
                            {
                                string[] mapParts = currentLine.Split(new char[] { '=' }, StringSplitOptions.RemoveEmptyEntries);
                                if (mapParts.Length == 2)
                                {
                                    string mapName = mapParts[0].Trim().ToLower();
                                    if (!m_mapDb.ContainsKey(mapName))
                                    {
                                        m_mapDb.Add(mapName, mapParts[1].Trim());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                AddLog(LogType.Default, GetLanguageText("MapDBLoadFailed"));
                return;
            }

            AddLog(LogType.Default, GetLanguageText("MapDBLoaded"),
                m_mapDb.Count);
        }

        private void LoadSkillTimer()
        {
            m_skillTimer = new List<SkillTimer>();

            // add basic timer
            SkillTimer timerNew = new SkillTimer();
            timerNew.Name = GetLanguageText("TimerNewName");
            timerNew.Intervall = 120;
            timerNew.Color = Color.SkyBlue;
            m_skillTimer.Add(timerNew);

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_paths.SkillTimerFile);

                XmlNode nodeTimers = doc["timers"];
                if (nodeTimers != null)
                {
                    foreach (XmlNode nodeTimer in nodeTimers.ChildNodes)
                    {
                        if (nodeTimer.Name.Equals("timer"))
                        {
                            string name = Helper.GetNodeValue(nodeTimer, "name");
                            int intervall = StringHelper.ParseInt(Helper.GetNodeValue(nodeTimer, "intervall"), -1);
                            if (!string.IsNullOrEmpty(name) && intervall > 0)
                            {
                                Keys skillKey = Keys.F1;
                                string key = Helper.GetNodeValue(nodeTimer, "key");
                                if (!string.IsNullOrEmpty(key))
                                {
                                    try
                                    {
                                        skillKey = (Keys)Enum.Parse(typeof(Keys), key);
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }

                                Color skillColor = Color.SkyBlue;
                                string color = Helper.GetNodeValue(nodeTimer, "color");
                                if (!string.IsNullOrEmpty(color))
                                {
                                    if (!color.StartsWith("#"))
                                    {
                                        color = "#" + color;
                                    }

                                    skillColor = System.Drawing.ColorTranslator.FromHtml(color);
                                }

                                SkillTimer skillTimer = new SkillTimer();
                                skillTimer.Name = name;
                                skillTimer.Intervall = intervall;
                                skillTimer.Key = skillKey;
                                skillTimer.Color = skillColor;

                                m_skillTimer.Add(skillTimer);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show(GetLanguageText("ErrorLoadingSkillTimer"), GetLanguageText("ErrorTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadMacros()
        {
            m_macros = new List<Macro>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_paths.MacrosFile);

                XmlNode nodeMacros = doc["macros"];
                if (nodeMacros != null && nodeMacros.Attributes["version"] != null)
                {
                    int version = StringHelper.Parse<int>(nodeMacros.Attributes["version"].InnerText, -1);
                    if (version >= 2)
                    {
                        foreach (XmlNode nodeMacro in nodeMacros.ChildNodes)
                        {
                            if (nodeMacro.Name.Equals("macro", StringComparison.OrdinalIgnoreCase))
                            {
                                try
                                {
                                    Macro macro = new Macro();
                                    macro.Description = nodeMacro["name"].InnerText.Trim();

                                    XmlNode nodeActions = nodeMacro["actions"];
                                    foreach (XmlNode nodeAction in nodeActions.ChildNodes)
                                    {
                                        if (nodeAction.Name.Equals("action", StringComparison.OrdinalIgnoreCase))
                                        {
                                            MacroAction action = new MacroAction();

                                            switch (nodeAction["type"].InnerText.Trim().ToLower())
                                            {
                                                case "key":
                                                    action.Type = MacroActionType.Key;
                                                    action.Key = (VKey)Enum.Parse(typeof(VKey), nodeAction["key"].InnerText.Trim());
                                                    break;

                                                case "delay":
                                                    action.Type = MacroActionType.Delay;
                                                    action.Delay = TimeSpan.FromMilliseconds(
                                                        StringHelper.Parse<double>(nodeAction["delay"].InnerText.Trim(), 100));
                                                    break;
                                            }

                                            macro.Actions.Add(action);
                                        }
                                    }

                                    m_macros.Add(macro);
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private ExpTable LoadExpTable(XmlNode node)
        {
            ExpTable result = null;

            if (node != null)
            {
                XmlNode nodeClasses = node["jobs"];
                XmlNode nodeBase = node["base"];

                if (nodeClasses != null && nodeBase != null)
                {
                    result = new ExpTable();

                    foreach (XmlNode nodeJob in nodeClasses.ChildNodes)
                    {
                        if (nodeJob.Name.Equals("job", StringComparison.OrdinalIgnoreCase))
                        {
                            int jobId = StringHelper.ParseInt(nodeJob.InnerText, -1);
                            if (jobId > 0 && !result.SupportedClasses.Contains(jobId))
                            {
                                result.SupportedClasses.Add(jobId);
                            }
                        }
                    }

                    foreach (XmlNode nodeExp in nodeBase.ChildNodes)
                    {
                        if (nodeExp.Name.Equals("lvl", StringComparison.OrdinalIgnoreCase))
                        {
                            int lvlExp = StringHelper.ParseInt(nodeExp.InnerText, -1);
                            if (lvlExp > 0)
                            {
                                result.SumBase += lvlExp;
                                result.LevelBase.Add(result.SumBase);
                            }
                        }
                    }
                }
            }

            return result;
        }

        private void LoadExpTables()
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_paths.ExpTable);

                m_expTables = new Dictionary<int, ExpTable>();

                XmlNode nodeRoot = doc["exp"];
                if (nodeRoot != null)
                {
                    foreach (XmlNode nodeTable in nodeRoot.ChildNodes)
                    {
                        if (nodeTable.Name.Equals("table", StringComparison.OrdinalIgnoreCase))
                        {
                            ExpTable table = LoadExpTable(nodeTable);
                            if (table != null)
                            {
                                foreach (int job in table.SupportedClasses)
                                {
                                    m_expTables.Add(job, table);
                                }
                            }
                        }
                    }
                }

                AddLog(LogType.Default, GetLanguageText("EXPTablesLoadOK").Replace(@"{classes}", m_expTables.Count.ToString()));
            }
            catch (Exception)
            {
                AddLog(LogType.Default, GetLanguageText("EXPTablesLoadFailed"));
            }
        }

        private Dictionary<string, Addresses> LoadClients()
        {
            Dictionary<string, Addresses> result = new Dictionary<string, Addresses>();

            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_paths.AddressesFile);

                XmlNode nodeClients = doc["Clients"];
                if (nodeClients != null)
                {
                    HashSet<string> hashes = new HashSet<string>();

                    foreach (XmlNode nodeClient in nodeClients.ChildNodes)
                    {
                        if (nodeClient.Name.Equals("client", StringComparison.OrdinalIgnoreCase))
                        {
                            hashes.Clear();

                            string currentMd5 = Helper.GetNodeValue(nodeClient, "hash");
                            if (!string.IsNullOrEmpty(currentMd5))
                            {
                                hashes.Add(currentMd5);
                            }

                            XmlNode nodeHashes = nodeClient["hashes"];
                            if (nodeHashes != null)
                            {
                                foreach (XmlNode nodeHash in nodeHashes.ChildNodes)
                                {
                                    if (nodeHash.Name.Equals("hash", StringComparison.OrdinalIgnoreCase))
                                    {
                                        if (!hashes.Contains(nodeHash.InnerText))
                                        {
                                            hashes.Add(nodeHash.InnerText);
                                        }
                                    }
                                }
                            }

                            if (hashes.Count > 0)
                            {
                                XmlNode nodeAddresses = nodeClient["Addresses"];
                                if (nodeAddresses != null)
                                {
                                    Addresses currentClient = new Addresses();
                                    currentClient.Hash = currentMd5;
                                    currentClient.Name = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "Name"));
                                    currentClient.BaseExp = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "BExp"));
                                    currentClient.BaseExpRequired = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "BExpR"));
                                    currentClient.JobExp = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "JExp"));
                                    currentClient.JobExpRequired = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "JExpR"));
                                    currentClient.Map = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "Map"));
                                    currentClient.Zeny = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "Zeny"));
                                    currentClient.BaseLevel = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "BLvl"));
                                    currentClient.JobLevel = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "JLvl"));
                                    currentClient.JobClass = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "JobCls"));
                                    currentClient.Hp = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "HP"));
                                    currentClient.HpMax = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "MaxHP"));
                                    currentClient.Sp = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "SP"));
                                    currentClient.SpMax = Helper.GetHexValue(Helper.GetNodeValue(nodeAddresses, "MaxSP"));

                                    XmlNode nodeHomunculus = nodeAddresses["Homunculus"];
                                    if (nodeHomunculus != null)
                                    {
                                        currentClient.Homu_Name = Helper.GetHexValue(Helper.GetNodeValue(nodeHomunculus, "Name"));
                                        currentClient.Homu_HP = Helper.GetHexValue(Helper.GetNodeValue(nodeHomunculus, "HP"));
                                        currentClient.Homu_MaxHP = Helper.GetHexValue(Helper.GetNodeValue(nodeHomunculus, "MaxHP"));
                                        currentClient.Homu_Hungry = Helper.GetHexValue(Helper.GetNodeValue(nodeHomunculus, "Hungry"));
                                        currentClient.Homu_Exp = Helper.GetHexValue(Helper.GetNodeValue(nodeHomunculus, "Exp"));
                                        currentClient.Homu_MaxExp = Helper.GetHexValue(Helper.GetNodeValue(nodeHomunculus, "ExpR"));
                                        currentClient.Homu_Friendly = Helper.GetHexValue(Helper.GetNodeValue(nodeHomunculus, "Friendly"));

                                        currentClient.SupportHomunculus = true;
                                    }

                                    XmlNode nodePet = nodeAddresses["Pet"];
                                    if (nodePet != null)
                                    {
                                        currentClient.PetName = Helper.GetHexValue(Helper.GetNodeValue(nodePet, "Name"));
                                        currentClient.PetFriendly = Helper.GetHexValue(Helper.GetNodeValue(nodePet, "Friendly"));
                                        currentClient.PetHungry = Helper.GetHexValue(Helper.GetNodeValue(nodePet, "Hungry"));

                                        currentClient.SupportPet = true;
                                    }

                                    foreach (string hash in hashes)
                                    {
                                        result.Add(hash, currentClient);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), GetLanguageText("ErrorTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }

        private int[] StringCsvToIntArray(string value)
        {
            int[] result = new int[0];

            string[] values = value.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            result = new int[values.Length];

            for (int i = 0; i < values.Length; i++)
            {
                int.TryParse(values[i].Trim(), out result[i]);
            }

            return result;
        }

        private void LoadJobClasses()
        {
            m_jobClasses = new List<JobClass>();
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(m_paths.JobClassesFile);

                foreach (XmlNode nodeJobClass in doc.GetElementsByTagName("JobClass"))
                {
                    JobClass currentJobClass = new JobClass();
                    currentJobClass.Name = Helper.GetNodeValue(nodeJobClass, "Name");

                    currentJobClass.MaxBLvl = Convert.ToInt32(Helper.GetNodeValue(nodeJobClass, "MaxBLvl"));
                    currentJobClass.MaxJLvl = Convert.ToInt32(Helper.GetNodeValue(nodeJobClass, "MaxJLvl"));

                    currentJobClass.MaxStats = Convert.ToInt32(Helper.GetNodeValue(nodeJobClass, "MaxStats"));
                    currentJobClass.Statspoints = Convert.ToInt32(Helper.GetNodeValue(nodeJobClass, "StPoints"));

                    XmlNode nodeJobBoni = nodeJobClass["JobBoni"];
                    if (nodeJobBoni != null)
                    {
                        XmlNode nodeStr = nodeJobBoni["Str"];
                        if (nodeStr != null)
                        {
                            currentJobClass.JobBonus.Add(new JobBonus(StatsType.Str, StringCsvToIntArray(nodeStr.InnerText)));
                        }
                        XmlNode nodeAgi = nodeJobBoni["Agi"];
                        if (nodeAgi != null)
                        {
                            currentJobClass.JobBonus.Add(new JobBonus(StatsType.Agi, StringCsvToIntArray(nodeAgi.InnerText)));
                        }
                        XmlNode nodeVit = nodeJobBoni["Vit"];
                        if (nodeVit != null)
                        {
                            currentJobClass.JobBonus.Add(new JobBonus(StatsType.Vit, StringCsvToIntArray(nodeVit.InnerText)));
                        }
                        XmlNode nodeInt = nodeJobBoni["Int"];
                        if (nodeInt != null)
                        {
                            currentJobClass.JobBonus.Add(new JobBonus(StatsType.Int, StringCsvToIntArray(nodeInt.InnerText)));
                        }
                        XmlNode nodeDex = nodeJobBoni["Dex"];
                        if (nodeDex != null)
                        {
                            currentJobClass.JobBonus.Add(new JobBonus(StatsType.Dex, StringCsvToIntArray(nodeDex.InnerText)));
                        }
                        XmlNode nodeLuk = nodeJobBoni["Luk"];
                        if (nodeLuk != null)
                        {
                            currentJobClass.JobBonus.Add(new JobBonus(StatsType.Luk, StringCsvToIntArray(nodeLuk.InnerText)));
                        }
                    }

                    m_jobClasses.Add(currentJobClass);
                }
            }
            catch (Exception)
            {
                AddLog(LogType.Default, GetLanguageText("JobClsLoadingFailed"));
                return;
            }

            AddLog(LogType.Default, GetLanguageText("JobClsLoaded"),
                m_jobClasses.Count);
        }

        private void UseHealPotion()
        {
            if (!m_charInfo.CharacterSelection
                && WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
            {
                int delayMiliseconds = (int)DateTime.Now.Subtract(m_settings.AutoKey.LastUse).TotalMilliseconds;
                if (delayMiliseconds >= m_settings.AutoPotions.Delay)
                {
                    VKey key = (VKey)((int)VKey.F1 + (m_settings.AutoPotions.Key - 1));
                    SendInputManager.SendInput(key);
                }
            }
        }

        private void UseFlyWing()
        {
            if (!m_charInfo.CharacterSelection
                && WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
            {
                AddLog(LogType.Default, GetLanguageText("UseAutoWing"));

                VKey key = (VKey)((int)VKey.F1 + (m_settings.AutoWing.Key - 1));
                SendInputManager.SendInput(key);

                m_settings.AutoWing.LastUse = DateTime.Now;

                m_charInfo.LastDmg = DateTime.Now;
                m_charInfo.LastKill = DateTime.Now;
            }
        }

        private void UseAutoKey()
        {
            if (!m_charInfo.CharacterSelection
                && WinAPI.GetForegroundWindow() == m_processManager.WindowHandle)
            {
                AddLog(LogType.Default, GetLanguageText("UseAutoKey"));

                VKey key = (VKey)((int)VKey.F1 + (m_settings.AutoKey.Key - 1));
                SendInputManager.SendInput(key);

                m_settings.AutoKey.LastUse = DateTime.Now;
            }
        }

        private void TakeScreenshot(int delay)
        {
            Threading.Thread thread = new Threading.Thread(new Threading.ParameterizedThreadStart(TakeScreenshotThread));
            thread.Start(delay);
        }

        private void TakeScreenshotThread(object delay)
        {
            Thread.Sleep(Convert.ToInt32(delay));
            SendInputManager.SendInput(VKey.SNAPSHOT);
        }

        private bool CheckGameDirectory()
        {
            if (string.IsNullOrEmpty(m_settings.GameDirectory))
            {
                MessageBox.Show(GetLanguageText("ChooseGameDirectory"), GetLanguageText("StopTitle"),
                     MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return false;
            }

            if (!Directory.Exists(m_settings.GameDirectory))
            {
                MessageBox.Show(GetLanguageText("InvalidGameDirectory"), GetLanguageText("StopTitle"),
                    MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return false;
            }

            return true;
        }

        private bool CheckGameExePath()
        {
            if (!File.Exists(Path.Combine(m_settings.GameDirectory, m_settings.ClientExeFilename)))
            {
                MessageBox.Show(GetLanguageText("GameFileNotFound"), GetLanguageText("StopTitle"),
                     MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return false;
            }
            return true;
        }

        private bool CheckSetupExePath()
        {
            if (!File.Exists(Path.Combine(m_settings.GameDirectory, m_settings.SetupExeFilename)))
            {
                MessageBox.Show(GetLanguageText("SetupFileNotFound"), GetLanguageText("StopTitle"),
                     MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return false;
            }
            return true;
        }

        private string GetMapname(string name)
        {
            string result = name;

            int pointPos = name.IndexOf('.');
            if (pointPos > 0)
            {
                string mapName = name.Substring(0, pointPos).ToLower();

                if (m_mapDb.ContainsKey(mapName))
                {
                    result = m_mapDb[mapName];
                }
            }

            return result;
        }

        private Color GetColor(LogType type)
        {
            Color result;
            switch (type)
            {
                case LogType.BaseExpGained:
                case LogType.JobExpGained:

                case LogType.HpGained:
                case LogType.SpGained:

                case LogType.ZenyGained:
                    result = Color.FromArgb(215, 255, 213);
                    break;

                case LogType.HpLost:
                case LogType.SpLost:

                case LogType.BaseExpLost:
                case LogType.JobExpLost:

                case LogType.ZenyLost:
                    result = Color.FromArgb(255, 213, 213);
                    break;

                case LogType.MapChange:
                    result = Color.FromArgb(252, 255, 213);
                    break;

                case LogType.BaseLevelUp:
                case LogType.JobLevelUp:
                    result = Color.FromArgb(184, 214, 255);
                    break;

                default:
                    result = Color.White;
                    break;
            }

            return result;
        }

        private void AddLog(LogType type, string text, params object[] parameter)
        {
            if ((m_settings.LogTypes & (int)type) == (int)type)
            {
                ListViewItem item = new ListViewItem();
                item.BackColor = GetColor(type);

                item.Text = string.Empty;

                if (m_settings.AddTimeInLog)
                {
                    item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss") + " - " + string.Format(text, parameter));
                }
                else
                {
                    item.SubItems.Add(string.Format(text, parameter));
                }

                lvwLog.Items.Add(item);
                lvwLog.EnsureVisible(lvwLog.Items.Count - 1);

                lvwLog.Columns[1].Width = lvwLog.ClientSize.Width;
            }
        }

        private delegate void AddSystemLogDelegate(SystemLogType type, string text, params object[] parameter);
        private void AddSystemLog(SystemLogType type, string text, params object[] parameter)
        {
            if (InvokeRequired)
            {
                Invoke(new AddSystemLogDelegate(AddSystemLog), type, text, parameter);
            }
            else
            {
                ListViewItem item = new ListViewItem();

                switch (type)
                {
                    default:
                    case SystemLogType.Default:
                        item.BackColor = Color.White;
                        break;
                    case SystemLogType.Error:
                        item.BackColor = Color.FromArgb(255, 255, 180, 180);
                        break;
                    case SystemLogType.Warning:
                        item.BackColor = Color.FromArgb(255, 255, 255, 135);
                        break;
                    case SystemLogType.Notice:
                        item.BackColor = Color.FromArgb(255, 147, 218, 255);
                        break;
                }

                item.Text = string.Empty;

                if (m_settings.AddTimeInLog)
                {
                    item.SubItems.Add(DateTime.Now.ToString("HH:mm:ss") + " - " + string.Format(text, parameter));
                }
                else
                {
                    item.SubItems.Add(string.Format(text, parameter));
                }

                lvwLog.Items.Add(item);
                lvwLog.EnsureVisible(lvwLog.Items.Count - 1);

                lvwLog.Columns[1].Width = lvwLog.ClientSize.Width;
            }
        }

        private int GetPositivValue(int value)
        {
            int result;
            if (value < 0)
            {
                result = -value;
            }
            else
            {
                result = value;
            }

            return result;
        }

        private void InitGui()
        {
            btnReset.Enabled = false;
        }

        private void ResetGui()
        {
            lblName.Text = string.Empty;

            lblCompleted.Text = string.Empty;

            lblTime.Text = string.Empty;

            lblBaseExpValues.Text = string.Empty;
            lblBasePercent.Text = string.Empty;
            prgBaseEXP.Value = 0;
            lblJobExpValues.Text = string.Empty;
            lblJobPercent.Text = string.Empty;
            prgJobExp.Value = 0;

            lblBaseExpHour.Text = string.Empty;
            lblJobExpHour.Text = string.Empty;

            lblGainedBaseExp.Text = string.Empty;
            lblGainedJobExp.Text = string.Empty;

            lblRequiredBaseExp.Text = string.Empty;
            lblRequiredJobExp.Text = string.Empty;

            lblKilledMobs.Text = string.Empty;

            lblRequiredBaseTime.Text = string.Empty;
            lblRequiredJobTime.Text = string.Empty;

            if (m_formHomunculus != null && !m_formHomunculus.IsDisposed && !m_formHomunculus.Disposing)
            {
                m_formHomunculus.ResetValues();
            }
            if (m_formPet != null && !m_formPet.IsDisposed && !m_formPet.Disposing)
            {
                m_formPet.ResetValues();
            }
        }

        private void LoadSettings()
        {
            Settings settings = new Settings(m_paths.SettingsFile);

            settings.SetGroup("MainWindow");
            Left = settings.ReadValueInt("Left", Screen.PrimaryScreen.WorkingArea.Width / 2 - Width / 2);
            Top = settings.ReadValueInt("Top", Screen.PrimaryScreen.WorkingArea.Height / 2 - Height / 2);
            Height = settings.ReadValueInt("Height", Height);

            Helper.FixWindowPosition(this);

            settings.SetGroup("Process");
            m_settings.WindowTitle = settings.ReadValueString("Title", "Ragnarok");

            settings.SetGroup("LogTypes");
            m_settings.LogTypes = settings.ReadValueInt("LogTypes",
                (int)(LogType.Default | LogType.BaseExpGained | LogType.BaseExpLost | LogType.JobExpGained | LogType.JobExpLost | LogType.ZenyGained | LogType.ZenyLost));

            settings.SetGroup("Addresses");
            m_settings.Addresses.Name = settings.ReadValueInt("Name", 0);
            m_settings.Addresses.BaseLevel = settings.ReadValueInt("BaseLevel", 0);
            m_settings.Addresses.JobLevel = settings.ReadValueInt("JobLevel", 0);
            m_settings.Addresses.BaseExp = settings.ReadValueInt("BaseExp", 0x8EE00C);
            m_settings.Addresses.BaseExpRequired = settings.ReadValueInt("BaseExpRequired", 0);
            m_settings.Addresses.JobExp = settings.ReadValueInt("JobExp", 0);
            m_settings.Addresses.JobExpRequired = settings.ReadValueInt("JobExpRequired", 0);
            m_settings.Addresses.JobClass = settings.ReadValueInt("JobClass", 0);
            m_settings.Addresses.Zeny = settings.ReadValueInt("Zeny", 0);
            m_settings.Addresses.Map = settings.ReadValueInt("Map", 0);
            m_settings.Addresses.Hp = settings.ReadValueInt("Hp", 0);
            m_settings.Addresses.HpMax = settings.ReadValueInt("HpMax", 0);
            m_settings.Addresses.Sp = settings.ReadValueInt("Sp", 0);
            m_settings.Addresses.SpMax = settings.ReadValueInt("SpMax", 0);

            settings.SetGroup("GameDirectory");
            m_settings.GameDirectory = settings.ReadValueString("Directory", string.Empty);
            m_settings.ClientExeFilename = settings.ReadValueString("ClientFilename", "Ragnarok.exe");
            m_settings.SetupExeFilename = settings.ReadValueString("SetupFilename", "Setup.exe");

            settings.SetGroup("Misc");
            m_settings.AddTimeInLog = settings.ReadValueBool("AddTimeInLog", true);

            m_settings.TopMost = settings.ReadValueBool("OnTop", false);
            m_settings.Opacity = settings.ReadValueBool("Opacity", false);

            m_settings.MobDbUrl = settings.ReadValueString("MobDbUrl", "http://rode-r.doddlercon.com/monster/view/$mobID$");

            m_settings.PartyServerIP = settings.ReadValueString("PartyServerIP", "127.0.0.1");

            m_settings.Language = settings.ReadValueString("Language", "de");
            ApplyLanguage();

            m_settings.MoveAllWindows = settings.ReadValueBool("MoveAllWindows", true);

            m_settings.CheckVersion = settings.ReadValueBool("CheckVersion", true);

            settings.SetGroup("CashRegisterSound");
            m_settings.CashRegisterSound_Enabled = settings.ReadValueBool("Enabled", true);
            m_settings.CashRegisterSound_MinimumZeny = settings.ReadValueInt("MinimumValue", 10000);
            m_settings.CashRegisterSound_MaximumZeny = settings.ReadValueInt("MaximumValue", 0);

            settings.SetGroup("Screenshot");
            m_settings.BaseupScreenshot = settings.ReadValueBool("BaseupScreenshot", true);
            m_settings.BaseupScreenshotDelay = settings.ReadValueInt("BaseupScreenshotDelay", 1200);
            m_settings.JobupScreenshot = settings.ReadValueBool("JobupScreenshot", false);
            m_settings.JobupScreenshotDelay = settings.ReadValueInt("JobupScreenshotDelay", 500);
            m_settings.MinLevelScreenshots = settings.ReadValueInt("MinLevelScreenshots", 40);

            settings.SetGroup("AutoPotions");
            m_settings.AutoPotions.Key = 0;
            m_settings.AutoPotions.MinimumPercent = settings.ReadValueInt("MinimumPercent", 50);
            m_settings.AutoPotions.MaximumPercent = settings.ReadValueInt("MaximumPercent", 95);
            m_settings.AutoPotions.Delay = settings.ReadValueInt("Delay", 500);

            settings.SetGroup("AutoWing");
            m_settings.AutoWing.Key = 0;
            m_settings.AutoWing.TimeSeconds = settings.ReadValueInt("Time", 60);
            m_settings.AutoWing.AutowingCondition = (AutowingCondition)settings.ReadValueInt("Condition", 1);
            m_settings.AutoWing.LastUse = DateTime.Now;

            settings.SetGroup("AutoKey");
            m_settings.AutoKey.Key = 0;
            m_settings.AutoKey.Interval = settings.ReadValueInt("Interval", 1000);
            m_settings.AutoKey.LastUse = DateTime.Now;

            settings.SetGroup("Homunculus");
            m_settings.HomunculusRawExp = settings.ReadValueBool("RawExp", false);

            m_settings.HomunculusHungryWarnSound = settings.ReadValueBool("HungryWarningSound", true);
            m_settings.HomunculusHungryWarningSound = settings.ReadValueString("HungryWarningSoundFile", "homunculus_hungry.wav");
            m_settings.HomunculusHungryMinValue = settings.ReadValueInt("HungryWarningMinValue", 15);
            m_settings.HomunculusHungryCloseClient = settings.ReadValueBool("HungryCloseClient", false);

            settings.SetGroup("Pet");
            m_settings.PetFeedAlarmSound = settings.ReadValueBool("PetHungrySound", true);
            m_settings.PetMinimumHungryValue = settings.ReadValueInt("PetMinimumHungryValue", 24);

            settings.SetGroup("Windows");

            int windowsCount = settings.ReadValueInt("Count", 0);
            for (int i = 1; i <= windowsCount; i++)
            {
                string[] winValues = settings.ReadValueString("Window" + i, string.Empty).Split(';');
                if (winValues.Length >= 5)
                {
                    Rectangle winrect = new Rectangle();
                    winrect.X = Convert.ToInt32(winValues[1]);
                    winrect.Y = Convert.ToInt32(winValues[2]);
                    winrect.Width = Convert.ToInt32(winValues[3]);
                    winrect.Height = Convert.ToInt32(winValues[4]);

                    m_settings.WindowPositions.Add(winValues[0], winrect);
                }
            }
        }

        private void LoadPlugins()
        {
            m_plugins = new List<PluginInfo>();

            string path = Path.Combine(Application.StartupPath, "Plugins");

            // check directory
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
            }
            catch (Exception)
            {
            }

            // load plugins
            try
            {
                string[] files = Directory.GetFiles(path, "*.dll");

                foreach (string file in files)
                {
                    string extension = Path.GetExtension(file).Replace(".", "").ToLower();
                    if (extension == "dll")
                    {
                        Assembly pluginAssembly = Assembly.LoadFile(file);
                        foreach (Type pluginType in pluginAssembly.GetTypes())
                        {
                            if (pluginType.IsPublic && !pluginType.IsAbstract)
                            {
                                Type pluginInterface = pluginType.GetInterface("Rowatch2.Plugin.IPlugin", false);
                                if (pluginInterface != null)
                                {
                                    PluginInfo info = new PluginInfo();
                                    info.Path = Path.GetFileName(path);

                                    try
                                    {
                                        object pluginInstance = Activator.CreateInstance(pluginAssembly.GetType(pluginType.ToString()));
                                        if (pluginInstance != null)
                                        {
                                            info.Plugin = pluginInstance as IPlugin;

                                            if (info.Plugin != null)
                                            {
                                                m_plugins.Add(info);

                                                info.Plugin.Initializing();
                                            }
                                        }

                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void ApplyLanguage()
        {
            string language = m_settings.Language;
            if (language.Equals("de"))
            {
                language = "de-DE";
            }
            else if (language.Equals("en"))
            {
                language = "en-GB";
            }

            CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
            try
            {
                cultureInfo = new CultureInfo(language);
                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;
            }
            catch (Exception)
            {
            }

            m_languageTexts = new Dictionary<string, string>();

            ResourceManager manager = new ResourceManager("Rowatch2.Strings", GetType().Assembly);

            ResourceSet set = manager.GetResourceSet(cultureInfo, true, true);
            foreach (DictionaryEntry entry in set)
            {
                if (!m_languageTexts.ContainsKey(entry.Key.ToString()))
                {
                    m_languageTexts.Add(entry.Key.ToString(), entry.Value.ToString());
                }
            }
        }

        private string GetLanguageText(string id)
        {
            string result;
            if (m_languageTexts.ContainsKey(id))
            {
                result = m_languageTexts[id].Replace("\\n", Environment.NewLine);
            }
            else
            {
                result = string.Empty;
            }

            return result;
        }

        private void SaveMacros()
        {
            try
            {
                XmlWriterSettings settings = new XmlWriterSettings()
                {
                    Indent = true,
                    Encoding = Encoding.UTF8
                };

                using (XmlWriter writer = XmlWriter.Create(File.Create(m_paths.MacrosFile), settings))
                {
                    writer.WriteStartDocument();
                    writer.WriteStartElement("macros");

                    writer.WriteAttributeString("version", "2");

                    foreach (Macro macro in m_macros)
                    {
                        writer.WriteStartElement("macro");

                        writer.WriteElementString("name", macro.Description);

                        writer.WriteStartElement("actions");
                        foreach (MacroAction action in macro.Actions)
                        {
                            writer.WriteStartElement("action");

                            writer.WriteElementString("type", action.Type.ToString());
                            switch (action.Type)
                            {
                                case MacroActionType.Delay:
                                    writer.WriteElementString("delay", action.Delay.Value.TotalMilliseconds.ToString());
                                    break;
                                case MacroActionType.Key:
                                    writer.WriteElementString("key", action.Key.Value.ToString());
                                    break;
                            }

                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();

                        writer.WriteEndElement();
                    }

                    writer.WriteEndElement();
                    writer.WriteEndDocument();
                }
            }
            catch (Exception)
            {
                // todo: log error message
            }
        }

        private void SaveSettings()
        {
            if (!Directory.Exists(m_paths.SettingsFolder))
            {
                Directory.CreateDirectory(m_paths.SettingsFolder);
            }

            Settings settings = new Settings(m_paths.SettingsFile);

            settings.SetGroup("MainWindow");
            settings.WriteValue("Left", Left);
            settings.WriteValue("Top", Top);
            settings.WriteValue("Height", Height);

            settings.SetGroup("Process");
            settings.WriteValue("Title", m_settings.WindowTitle);

            settings.SetGroup("LogTypes");
            settings.WriteValue("LogTypes", m_settings.LogTypes);

            settings.SetGroup("Addresses");
            settings.WriteValue("Name", m_settings.Addresses.Name);
            settings.WriteValue("BaseLevel", m_settings.Addresses.BaseLevel);
            settings.WriteValue("JobLevel", m_settings.Addresses.JobLevel);
            settings.WriteValue("BaseExp", m_settings.Addresses.BaseExp);
            settings.WriteValue("BaseExpRequired", m_settings.Addresses.BaseExpRequired);
            settings.WriteValue("JobExp", m_settings.Addresses.JobExp);
            settings.WriteValue("JobExpRequired", m_settings.Addresses.JobExpRequired);
            settings.WriteValue("JobClass", m_settings.Addresses.JobClass);
            settings.WriteValue("Zeny", m_settings.Addresses.Zeny);
            settings.WriteValue("Map", m_settings.Addresses.Map);
            settings.WriteValue("Hp", m_settings.Addresses.Hp);
            settings.WriteValue("HpMax", m_settings.Addresses.HpMax);
            settings.WriteValue("Sp", m_settings.Addresses.Sp);
            settings.WriteValue("SpMax", m_settings.Addresses.SpMax);

            settings.SetGroup("GameDirectory");
            settings.WriteValue("Directory", m_settings.GameDirectory);
            settings.WriteValue("ClientFilename", m_settings.ClientExeFilename);
            settings.WriteValue("SetupFilename", m_settings.SetupExeFilename);

            settings.SetGroup("Misc");
            settings.WriteValue("AddTimeInLog", m_settings.AddTimeInLog);

            settings.WriteValue("OnTop", m_settings.TopMost);
            settings.WriteValue("Opacity", m_settings.Opacity);

            settings.WriteValue("MobDbUrl", m_settings.MobDbUrl);

            settings.WriteValue("PartyServerIP", m_settings.PartyServerIP);

            settings.WriteValue("Language", m_settings.Language);

            settings.WriteValue("MoveAllWindows", m_settings.MoveAllWindows);

            settings.WriteValue("CheckVersion", m_settings.CheckVersion);

            settings.SetGroup("CashRegisterSound");
            settings.WriteValue("Enabled", m_settings.CashRegisterSound_Enabled);
            settings.WriteValue("MinimumValue", m_settings.CashRegisterSound_MinimumZeny);
            settings.WriteValue("MaximumValue", m_settings.CashRegisterSound_MaximumZeny);

            settings.SetGroup("Screenshot");
            settings.WriteValue("BaseupScreenshot", m_settings.BaseupScreenshot);
            settings.WriteValue("BaseupScreenshotDelay", m_settings.BaseupScreenshotDelay);
            settings.WriteValue("JobupScreenshot", m_settings.JobupScreenshot);
            settings.WriteValue("JobupScreenshotDelay", m_settings.JobupScreenshotDelay);
            settings.WriteValue("MinLevelScreenshots", m_settings.MinLevelScreenshots);

            settings.SetGroup("AutoPotions");
            settings.WriteValue("Key", m_settings.AutoPotions.Key);
            settings.WriteValue("MinimumPercent", m_settings.AutoPotions.MinimumPercent);
            settings.WriteValue("MaximumPercent", m_settings.AutoPotions.MaximumPercent);
            settings.WriteValue("Delay", m_settings.AutoPotions.Delay);

            settings.SetGroup("AutoWing");
            settings.WriteValue("Key", m_settings.AutoWing.Key);
            settings.WriteValue("Time", m_settings.AutoWing.TimeSeconds);
            settings.WriteValue("Condition", (int)m_settings.AutoWing.AutowingCondition);

            settings.SetGroup("AutoKey");
            settings.WriteValue("Interval", m_settings.AutoKey.Interval);
            settings.WriteValue("Key", m_settings.AutoKey.Key);

            settings.SetGroup("Homunculus");
            settings.WriteValue("RawExp", m_settings.HomunculusRawExp);

            settings.WriteValue("HungryWarningSound", m_settings.HomunculusHungryWarnSound);
            settings.WriteValue("HungryWarningSoundFile", m_settings.HomunculusHungryWarningSound);
            settings.WriteValue("HungryWarningMinValue", m_settings.HomunculusHungryMinValue);
            settings.WriteValue("HungryCloseClient", m_settings.HomunculusHungryCloseClient);

            settings.SetGroup("Pet");
            settings.WriteValue("PetHungrySound", m_settings.PetFeedAlarmSound);
            settings.WriteValue("PetMinimumHungryValue", m_settings.PetMinimumHungryValue);

            settings.SetGroup("Windows");
            settings.WriteValue("Count", m_settings.WindowPositions.Count);

            int winIndex = 0;
            foreach (var pos in m_settings.WindowPositions)
            {
                winIndex++;

                settings.WriteValue("Window" + winIndex, string.Format("{0};{1};{2};{3};{4}",
                    pos.Key,
                    pos.Value.Location.X, pos.Value.Location.Y,
                    pos.Value.Size.Width, pos.Value.Size.Height));
            }

            settings.Save();
        }

        private void ApplySettings()
        {
            TopMost = m_settings.TopMost;
            RefreshOpacity();

            m_settings.AutoWing.LastUse = DateTime.Now;
            m_settings.AutoKey.LastUse = DateTime.Now;

            RefreshEventImages();
        }

        private void RefreshEventImages()
        {
            pbxAutoWings.Visible = (m_settings.AutoWing.Key > 0);
            pbxAutoPotions.Visible = (m_settings.AutoPotions.Key > 0);
        }

        private void SetLabelText(Label label, string text)
        {
            if (label.Text != null && label.Text != text)
            {
                label.Text = text;
            }
        }

        private void RefreshOpacity()
        {
            if (!m_settings.Opacity || WinAPI.GetForegroundWindow() == Handle)
            {
                Opacity = 1f;
            }
            else
            {
                Opacity = 0.65f;
            }
        }

        private bool IsPartyWindow()
        {
            return m_formParty != null && !m_formParty.IsDisposed && !m_formParty.Disposing;
        }

        #endregion

        #region Client Methods

        private void HandleMessage(NetworkMessage message)
        {
#if DEBUG
            AddLog(LogType.Server, "Message: {0}", ((ServerMessageId)message.ID).ToString());
#endif

            switch ((ServerMessageId)message.ID)
            {
                case ServerMessageId.UserConnected:
                    HandleMessage_UserConnected(message);
                    break;

                case ServerMessageId.PropertieChanged:
                    HandleMessage_PropertieChanged(message);
                    break;

                case ServerMessageId.UserDisconnected:
                    HandleMessage_UserDisconnected(message);
                    break;
            }
        }

        private void HandleMessage_UserConnected(NetworkMessage message)
        {
#if DEBUG
            AddLog(LogType.Server, "User Connected: {0}", message.Parameter[0]);
#endif

            if (message.Parameter.Length == 16)
            {
                int clientId = StringHelper.ParseInt(message.Parameter[0], 0);
                if (clientId != 0)
                {
                    if (!m_partMembersIndex.ContainsKey(clientId))
                    {
                        PartyMember connectedMember = new PartyMember();

                        m_partMembersIndex.Add(clientId, connectedMember);
                        m_partyMembers.Add(connectedMember);

                        connectedMember.Name = message.Parameter[1];
                        connectedMember.Map = message.Parameter[2];

                        connectedMember.BaseLevel = StringHelper.ParseInt(message.Parameter[3], 0);
                        connectedMember.JobLevel = StringHelper.ParseInt(message.Parameter[4], 0);

                        connectedMember.HP = StringHelper.ParseInt(message.Parameter[5], 0);
                        connectedMember.MaxHP = StringHelper.ParseInt(message.Parameter[6], 0);

                        connectedMember.SP = StringHelper.ParseInt(message.Parameter[7], 0);
                        connectedMember.MaxSP = StringHelper.ParseInt(message.Parameter[8], 0);

                        connectedMember.BaseExp = StringHelper.ParseInt(message.Parameter[9], 0);
                        connectedMember.BaseExpRequired = StringHelper.ParseInt(message.Parameter[10], 0);
                        connectedMember.BaseExpPerHour = StringHelper.ParseInt(message.Parameter[11], 0);

                        connectedMember.JobExp = StringHelper.ParseInt(message.Parameter[12], 0);
                        connectedMember.JobExpRequired = StringHelper.ParseInt(message.Parameter[13], 0);
                        connectedMember.JobExpPerHour = StringHelper.ParseInt(message.Parameter[14], 0);

                        connectedMember.KilledMobs = StringHelper.ParseInt(message.Parameter[15], 0);

#if DEBUG
                        AddLog(LogType.Server, "Partymember {0} has connected",
                               connectedMember.Name);
#endif

                        if (IsPartyWindow())
                        {
                            m_formParty.RefreshPartyMember();
                        }
                    }
                }
            }
        }

        private void HandleMessage_UserDisconnected(NetworkMessage message)
        {
            if (message.Parameter.Length == 1)
            {
                int clientId = StringHelper.ParseInt(message.Parameter[0], 0);
                if (clientId != 0 && m_partMembersIndex.ContainsKey(clientId))
                {
                    PartyMember disconnectedMember = m_partMembersIndex[clientId];

                    m_partyMembers.Remove(disconnectedMember);
                    m_partMembersIndex.Remove(clientId);

                    if (IsPartyWindow())
                    {
                        m_formParty.RefreshPartyMember();
                    }
                }
            }
        }

        private void HandleMessage_PropertieChanged(NetworkMessage message)
        {
            if (message.Parameter.Length == 3)
            {
                int clientId = StringHelper.ParseInt(message.Parameter[0], 0);
                if (clientId != 0 && m_partMembersIndex.ContainsKey(clientId))
                {
                    PartyMember member = m_partMembersIndex[clientId];

                    ChangedPropertieType propertieType = (ChangedPropertieType)StringHelper.ParseInt(message.Parameter[1], -1);
                    string value = message.Parameter[2];

#if DEBUG
                    AddLog(LogType.Server, "Partymembers {0} changed to {1}", propertieType, value);
#endif

                    switch (propertieType)
                    {
                        case ChangedPropertieType.Name:
                            member.Name = value;
                            break;
                        case ChangedPropertieType.Map:
                            member.Map = GetMapname(value);
                            break;

                        case ChangedPropertieType.Hp:
                            member.HP = StringHelper.ParseInt(value, 0);
                            break;
                        case ChangedPropertieType.MaxHp:
                            member.MaxHP = StringHelper.ParseInt(value, 0);
                            break;

                        case ChangedPropertieType.Sp:
                            member.SP = StringHelper.ParseInt(value, 0);
                            break;
                        case ChangedPropertieType.MaxSp:
                            member.MaxSP = StringHelper.ParseInt(value, 0);
                            break;

                        case ChangedPropertieType.BaseLevel:
                            member.BaseLevel = StringHelper.ParseInt(value, 0);
                            break;
                        case ChangedPropertieType.JobLevel:
                            member.JobLevel = StringHelper.ParseInt(value, 0);
                            break;

                        case ChangedPropertieType.BaseExp:
                            member.BaseExp = StringHelper.ParseInt(value, 0);
                            break;
                        case ChangedPropertieType.BaseExpRequired:
                            member.BaseExpRequired = StringHelper.ParseInt(value, 0);
                            break;
                        case ChangedPropertieType.BaseExpPerHour:
                            member.BaseExpPerHour = StringHelper.ParseInt(value, 0);
                            break;

                        case ChangedPropertieType.JobExp:
                            member.JobExp = StringHelper.ParseInt(value, 0);
                            break;
                        case ChangedPropertieType.JobExpRequired:
                            member.JobExpRequired = StringHelper.ParseInt(value, 0);
                            break;
                        case ChangedPropertieType.JobExpPerHour:
                            member.JobExpPerHour = StringHelper.ParseInt(value, 0);
                            break;

                        case ChangedPropertieType.KilledMobs:
                            member.KilledMobs = StringHelper.ParseInt(value, 0);
                            break;
                    }

                    if (IsPartyWindow())
                    {
                        m_formParty.RefreshPartyMember();
                    }
                }
            }
        }





        private void SendToServerConnected()
        {
            if (m_client != null && m_client.Connected && m_processManager.IsActive)
            {
                SendToServer(ClientMessageId.Connected,
                    m_charInfo.Name,
                    GetMapname(m_charInfo.Map),

                    m_charInfo.BaseLevel.ToString(),
                    m_charInfo.JobLevel.ToString(),

                    m_charInfo.HP.ToString(),
                    m_charInfo.MaxHP.ToString(),

                    m_charInfo.SP.ToString(),
                    m_charInfo.MaxSP.ToString(),

                    m_charInfo.BaseExp.ToString(),
                    m_charInfo.BaseExpRequired.ToString(),
                    m_charInfo.BaseExpPerHour.ToString(),

                    m_charInfo.JobExp.ToString(),
                    m_charInfo.JobExpRequired.ToString(),
                    m_charInfo.JobExpPerHour.ToString(),

                    m_charInfo.KilledMobs.ToString());
            }
        }

        private void SendToServerDisconnected()
        {
            if (m_client != null && m_client.Connected)
            {
                SendToServer(ClientMessageId.Disconnected);
            }
        }

        private void SendToServerPropertieChanged(ChangedPropertieType type, string value)
        {
            if (m_client != null && m_client.Connected && m_processManager.IsActive)
            {
                SendToServer(ClientMessageId.PropertieChanged, ((int)type).ToString(), value);
            }
        }

        private void SendToServer(ClientMessageId id, params string[] parameter)
        {
            if (m_client != null && m_client.Connected)
            {
                m_client.Send((ushort)id, parameter);
            }
        }

        #endregion
    }
}