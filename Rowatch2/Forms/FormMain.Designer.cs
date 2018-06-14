namespace Rowatch2.Forms
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.lblName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.prgBaseEXP = new System.Windows.Forms.ProgressBar();
            this.prgJobExp = new System.Windows.Forms.ProgressBar();
            this.lblBaseExpValues = new System.Windows.Forms.Label();
            this.lblJobExpValues = new System.Windows.Forms.Label();
            this.timerWatch = new System.Windows.Forms.Timer(this.components);
            this.lblBasePercent = new System.Windows.Forms.Label();
            this.lblJobPercent = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblGainedBaseExp = new System.Windows.Forms.Label();
            this.lblGainedJobExp = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblKilledMobs = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.lblBaseExpHour = new System.Windows.Forms.Label();
            this.lblJobExpHour = new System.Windows.Forms.Label();
            this.timerExpHour = new System.Windows.Forms.Timer(this.components);
            this.btnMisc = new System.Windows.Forms.Button();
            this.lvwLog = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.menuMisc = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mniLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mniClearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mniExpCounter = new System.Windows.Forms.ToolStripMenuItem();
            this.mniQuickwatch = new System.Windows.Forms.ToolStripMenuItem();
            this.mniCharts = new System.Windows.Forms.ToolStripMenuItem();
            this.mniCharState = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.mniServerLogin = new System.Windows.Forms.ToolStripMenuItem();
            this.mniParty = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.mniUpdateAddresses = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHomunculusWatch = new System.Windows.Forms.ToolStripMenuItem();
            this.mniAutofeed = new System.Windows.Forms.ToolStripMenuItem();
            this.mniPetWatch = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.mniOpenClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mniHideClient = new System.Windows.Forms.ToolStripMenuItem();
            this.mniOptimizeClientSize = new System.Windows.Forms.ToolStripMenuItem();
            this.mniOpenSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.mniSkillTimer = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMacro = new System.Windows.Forms.ToolStripMenuItem();
            this.mniMobSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mniCharCalculator = new System.Windows.Forms.ToolStripMenuItem();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRequiredBaseExp = new System.Windows.Forms.Label();
            this.lblRequiredJobExp = new System.Windows.Forms.Label();
            this.timerPartyServer = new System.Windows.Forms.Timer(this.components);
            this.lblRequiredBaseTime = new System.Windows.Forms.Label();
            this.lblRequiredJobTime = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.timerAutopotions = new System.Windows.Forms.Timer(this.components);
            this.pbxAutoWings = new System.Windows.Forms.PictureBox();
            this.pbxAutoPotions = new System.Windows.Forms.PictureBox();
            this.lblCompleted = new System.Windows.Forms.Label();
            this.bwVersionCheck = new System.ComponentModel.BackgroundWorker();
            this.menuMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutoWings)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutoPotions)).BeginInit();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoEllipsis = true;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            this.lblName.UseCompatibleTextRendering = true;
            // 
            // label3
            // 
            this.label3.AutoEllipsis = true;
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Name = "label3";
            // 
            // label4
            // 
            this.label4.AutoEllipsis = true;
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Name = "label4";
            // 
            // prgBaseEXP
            // 
            resources.ApplyResources(this.prgBaseEXP, "prgBaseEXP");
            this.prgBaseEXP.Name = "prgBaseEXP";
            // 
            // prgJobExp
            // 
            resources.ApplyResources(this.prgJobExp, "prgJobExp");
            this.prgJobExp.Name = "prgJobExp";
            // 
            // lblBaseExpValues
            // 
            resources.ApplyResources(this.lblBaseExpValues, "lblBaseExpValues");
            this.lblBaseExpValues.Name = "lblBaseExpValues";
            // 
            // lblJobExpValues
            // 
            resources.ApplyResources(this.lblJobExpValues, "lblJobExpValues");
            this.lblJobExpValues.Name = "lblJobExpValues";
            // 
            // timerWatch
            // 
            this.timerWatch.Interval = 25;
            this.timerWatch.Tick += new System.EventHandler(this.timerWatch_Tick);
            // 
            // lblBasePercent
            // 
            resources.ApplyResources(this.lblBasePercent, "lblBasePercent");
            this.lblBasePercent.Name = "lblBasePercent";
            // 
            // lblJobPercent
            // 
            resources.ApplyResources(this.lblJobPercent, "lblJobPercent");
            this.lblJobPercent.Name = "lblJobPercent";
            // 
            // lblTime
            // 
            resources.ApplyResources(this.lblTime, "lblTime");
            this.lblTime.Name = "lblTime";
            // 
            // label2
            // 
            this.label2.AutoEllipsis = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblGainedBaseExp
            // 
            resources.ApplyResources(this.lblGainedBaseExp, "lblGainedBaseExp");
            this.lblGainedBaseExp.Name = "lblGainedBaseExp";
            // 
            // lblGainedJobExp
            // 
            resources.ApplyResources(this.lblGainedJobExp, "lblGainedJobExp");
            this.lblGainedJobExp.Name = "lblGainedJobExp";
            // 
            // label7
            // 
            this.label7.AutoEllipsis = true;
            this.label7.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lblKilledMobs
            // 
            resources.ApplyResources(this.lblKilledMobs, "lblKilledMobs");
            this.lblKilledMobs.Name = "lblKilledMobs";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // label8
            // 
            this.label8.AutoEllipsis = true;
            this.label8.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // lblBaseExpHour
            // 
            resources.ApplyResources(this.lblBaseExpHour, "lblBaseExpHour");
            this.lblBaseExpHour.Name = "lblBaseExpHour";
            // 
            // lblJobExpHour
            // 
            resources.ApplyResources(this.lblJobExpHour, "lblJobExpHour");
            this.lblJobExpHour.Name = "lblJobExpHour";
            // 
            // timerExpHour
            // 
            this.timerExpHour.Interval = 1000;
            this.timerExpHour.Tick += new System.EventHandler(this.timerExpHour_Tick);
            // 
            // btnMisc
            // 
            resources.ApplyResources(this.btnMisc, "btnMisc");
            this.btnMisc.Name = "btnMisc";
            this.btnMisc.UseVisualStyleBackColor = true;
            this.btnMisc.Click += new System.EventHandler(this.btnMisc_Click);
            // 
            // lvwLog
            // 
            resources.ApplyResources(this.lvwLog, "lvwLog");
            this.lvwLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2,
            this.columnHeader1});
            this.lvwLog.FullRowSelect = true;
            this.lvwLog.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lvwLog.MultiSelect = false;
            this.lvwLog.Name = "lvwLog";
            this.lvwLog.ShowItemToolTips = true;
            this.lvwLog.UseCompatibleStateImageBehavior = false;
            this.lvwLog.View = System.Windows.Forms.View.Details;
            this.lvwLog.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lvwLog_MouseClick);
            // 
            // columnHeader2
            // 
            resources.ApplyResources(this.columnHeader2, "columnHeader2");
            // 
            // columnHeader1
            // 
            resources.ApplyResources(this.columnHeader1, "columnHeader1");
            // 
            // btnReset
            // 
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.Name = "btnReset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // btnSettings
            // 
            resources.ApplyResources(this.btnSettings, "btnSettings");
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // menuMisc
            // 
            this.menuMisc.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mniLog,
            this.mniClearLog,
            this.mniExpCounter,
            this.mniQuickwatch,
            this.mniCharts,
            this.mniCharState,
            this.toolStripMenuItem4,
            this.mniServerLogin,
            this.mniParty,
            this.toolStripMenuItem1,
            this.mniUpdateAddresses,
            this.mniHomunculusWatch,
            this.mniAutofeed,
            this.mniPetWatch,
            this.toolStripMenuItem5,
            this.mniOpenClient,
            this.mniHideClient,
            this.mniOptimizeClientSize,
            this.mniOpenSetup,
            this.toolStripMenuItem2,
            this.mniSkillTimer,
            this.mniMacro,
            this.mniMobSearch,
            this.mniCharCalculator});
            this.menuMisc.Name = "menuMisc";
            resources.ApplyResources(this.menuMisc, "menuMisc");
            this.menuMisc.Opening += new System.ComponentModel.CancelEventHandler(this.menuMisc_Opening);
            // 
            // mniLog
            // 
            this.mniLog.Name = "mniLog";
            resources.ApplyResources(this.mniLog, "mniLog");
            this.mniLog.Click += new System.EventHandler(this.mniLog_Click);
            // 
            // mniClearLog
            // 
            this.mniClearLog.Name = "mniClearLog";
            resources.ApplyResources(this.mniClearLog, "mniClearLog");
            this.mniClearLog.Click += new System.EventHandler(this.mniClearLog_Click);
            // 
            // mniExpCounter
            // 
            this.mniExpCounter.Name = "mniExpCounter";
            resources.ApplyResources(this.mniExpCounter, "mniExpCounter");
            this.mniExpCounter.Click += new System.EventHandler(this.mniExpCounter_Click);
            // 
            // mniQuickwatch
            // 
            this.mniQuickwatch.Name = "mniQuickwatch";
            resources.ApplyResources(this.mniQuickwatch, "mniQuickwatch");
            this.mniQuickwatch.Click += new System.EventHandler(this.mniQuickwatch_Click);
            // 
            // mniCharts
            // 
            this.mniCharts.Name = "mniCharts";
            resources.ApplyResources(this.mniCharts, "mniCharts");
            this.mniCharts.Click += new System.EventHandler(this.mniCharts_Click);
            // 
            // mniCharState
            // 
            this.mniCharState.Name = "mniCharState";
            resources.ApplyResources(this.mniCharState, "mniCharState");
            this.mniCharState.Click += new System.EventHandler(this.mniCharState_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            resources.ApplyResources(this.toolStripMenuItem4, "toolStripMenuItem4");
            // 
            // mniServerLogin
            // 
            this.mniServerLogin.Name = "mniServerLogin";
            resources.ApplyResources(this.mniServerLogin, "mniServerLogin");
            this.mniServerLogin.Click += new System.EventHandler(this.mniServerLogin_Click);
            // 
            // mniParty
            // 
            this.mniParty.Name = "mniParty";
            resources.ApplyResources(this.mniParty, "mniParty");
            this.mniParty.Click += new System.EventHandler(this.mniParty_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // mniUpdateAddresses
            // 
            this.mniUpdateAddresses.Name = "mniUpdateAddresses";
            resources.ApplyResources(this.mniUpdateAddresses, "mniUpdateAddresses");
            this.mniUpdateAddresses.Click += new System.EventHandler(this.mniUpdateAddresses_Click);
            // 
            // mniHomunculusWatch
            // 
            this.mniHomunculusWatch.Name = "mniHomunculusWatch";
            resources.ApplyResources(this.mniHomunculusWatch, "mniHomunculusWatch");
            this.mniHomunculusWatch.Click += new System.EventHandler(this.mniHomunculusWatch_Click);
            // 
            // mniAutofeed
            // 
            this.mniAutofeed.Name = "mniAutofeed";
            resources.ApplyResources(this.mniAutofeed, "mniAutofeed");
            this.mniAutofeed.Click += new System.EventHandler(this.mniAutofeed_Click);
            // 
            // mniPetWatch
            // 
            this.mniPetWatch.Name = "mniPetWatch";
            resources.ApplyResources(this.mniPetWatch, "mniPetWatch");
            this.mniPetWatch.Click += new System.EventHandler(this.mniPetWatch_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            resources.ApplyResources(this.toolStripMenuItem5, "toolStripMenuItem5");
            // 
            // mniOpenClient
            // 
            this.mniOpenClient.Name = "mniOpenClient";
            resources.ApplyResources(this.mniOpenClient, "mniOpenClient");
            this.mniOpenClient.Click += new System.EventHandler(this.mniOpenClient_Click);
            // 
            // mniHideClient
            // 
            this.mniHideClient.Name = "mniHideClient";
            resources.ApplyResources(this.mniHideClient, "mniHideClient");
            this.mniHideClient.Click += new System.EventHandler(this.mniHideClient_Click);
            // 
            // mniOptimizeClientSize
            // 
            this.mniOptimizeClientSize.Name = "mniOptimizeClientSize";
            resources.ApplyResources(this.mniOptimizeClientSize, "mniOptimizeClientSize");
            this.mniOptimizeClientSize.Click += new System.EventHandler(this.mniOptimizeClientSize_Click);
            // 
            // mniOpenSetup
            // 
            this.mniOpenSetup.Name = "mniOpenSetup";
            resources.ApplyResources(this.mniOpenSetup, "mniOpenSetup");
            this.mniOpenSetup.Click += new System.EventHandler(this.mniOpenSetup_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            resources.ApplyResources(this.toolStripMenuItem2, "toolStripMenuItem2");
            // 
            // mniSkillTimer
            // 
            this.mniSkillTimer.Name = "mniSkillTimer";
            resources.ApplyResources(this.mniSkillTimer, "mniSkillTimer");
            this.mniSkillTimer.Click += new System.EventHandler(this.mniSkillTimer_Click);
            // 
            // mniMacro
            // 
            this.mniMacro.Name = "mniMacro";
            resources.ApplyResources(this.mniMacro, "mniMacro");
            this.mniMacro.Click += new System.EventHandler(this.mniMacro_Click);
            // 
            // mniMobSearch
            // 
            this.mniMobSearch.Name = "mniMobSearch";
            resources.ApplyResources(this.mniMobSearch, "mniMobSearch");
            this.mniMobSearch.Click += new System.EventHandler(this.mniMobSearch_Click);
            // 
            // mniCharCalculator
            // 
            this.mniCharCalculator.Name = "mniCharCalculator";
            resources.ApplyResources(this.mniCharCalculator, "mniCharCalculator");
            this.mniCharCalculator.Click += new System.EventHandler(this.mniCharCalculator_Click);
            // 
            // label9
            // 
            this.label9.AutoEllipsis = true;
            this.label9.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // lblRequiredBaseExp
            // 
            resources.ApplyResources(this.lblRequiredBaseExp, "lblRequiredBaseExp");
            this.lblRequiredBaseExp.Name = "lblRequiredBaseExp";
            // 
            // lblRequiredJobExp
            // 
            resources.ApplyResources(this.lblRequiredJobExp, "lblRequiredJobExp");
            this.lblRequiredJobExp.Name = "lblRequiredJobExp";
            // 
            // timerPartyServer
            // 
            this.timerPartyServer.Enabled = true;
            this.timerPartyServer.Tick += new System.EventHandler(this.timerPartyServer_Tick);
            // 
            // lblRequiredBaseTime
            // 
            resources.ApplyResources(this.lblRequiredBaseTime, "lblRequiredBaseTime");
            this.lblRequiredBaseTime.Name = "lblRequiredBaseTime";
            // 
            // lblRequiredJobTime
            // 
            resources.ApplyResources(this.lblRequiredJobTime, "lblRequiredJobTime");
            this.lblRequiredJobTime.Name = "lblRequiredJobTime";
            // 
            // label1
            // 
            this.label1.AutoEllipsis = true;
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // timerAutopotions
            // 
            this.timerAutopotions.Tick += new System.EventHandler(this.timerAutopotions_Tick);
            // 
            // pbxAutoWings
            // 
            this.pbxAutoWings.Image = global::Rowatch2.Properties.Resources.fly_wing;
            resources.ApplyResources(this.pbxAutoWings, "pbxAutoWings");
            this.pbxAutoWings.Name = "pbxAutoWings";
            this.pbxAutoWings.TabStop = false;
            // 
            // pbxAutoPotions
            // 
            this.pbxAutoPotions.Image = global::Rowatch2.Properties.Resources.white_potion;
            resources.ApplyResources(this.pbxAutoPotions, "pbxAutoPotions");
            this.pbxAutoPotions.Name = "pbxAutoPotions";
            this.pbxAutoPotions.TabStop = false;
            // 
            // lblCompleted
            // 
            this.lblCompleted.AutoEllipsis = true;
            resources.ApplyResources(this.lblCompleted, "lblCompleted");
            this.lblCompleted.Name = "lblCompleted";
            this.lblCompleted.UseCompatibleTextRendering = true;
            // 
            // bwVersionCheck
            // 
            this.bwVersionCheck.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwVersionCheck_DoWork);
            this.bwVersionCheck.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwVersionCheck_RunWorkerCompleted);
            // 
            // FormMain
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCompleted);
            this.Controls.Add(this.pbxAutoPotions);
            this.Controls.Add(this.pbxAutoWings);
            this.Controls.Add(this.lvwLog);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnMisc);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblRequiredJobTime);
            this.Controls.Add(this.lblRequiredJobExp);
            this.Controls.Add(this.lblRequiredBaseTime);
            this.Controls.Add(this.lblRequiredBaseExp);
            this.Controls.Add(this.lblKilledMobs);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblJobExpHour);
            this.Controls.Add(this.lblGainedJobExp);
            this.Controls.Add(this.lblBaseExpHour);
            this.Controls.Add(this.lblGainedBaseExp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblJobExpValues);
            this.Controls.Add(this.lblJobPercent);
            this.Controls.Add(this.lblBasePercent);
            this.Controls.Add(this.lblBaseExpValues);
            this.Controls.Add(this.prgJobExp);
            this.Controls.Add(this.prgBaseEXP);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Activated += new System.EventHandler(this.FormMain_Activated);
            this.Deactivate += new System.EventHandler(this.FormMain_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.LocationChanged += new System.EventHandler(this.FormMain_LocationChanged);
            this.menuMisc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutoWings)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbxAutoPotions)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ProgressBar prgBaseEXP;
        private System.Windows.Forms.ProgressBar prgJobExp;
        private System.Windows.Forms.Label lblBaseExpValues;
        private System.Windows.Forms.Label lblJobExpValues;
        private System.Windows.Forms.Timer timerWatch;
        private System.Windows.Forms.Label lblBasePercent;
        private System.Windows.Forms.Label lblJobPercent;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblGainedBaseExp;
        private System.Windows.Forms.Label lblGainedJobExp;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblKilledMobs;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblBaseExpHour;
        private System.Windows.Forms.Label lblJobExpHour;
        private System.Windows.Forms.Timer timerExpHour;
        private System.Windows.Forms.Button btnMisc;
        private System.Windows.Forms.ListView lvwLog;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.ContextMenuStrip menuMisc;
        private System.Windows.Forms.ToolStripMenuItem mniLog;
        private System.Windows.Forms.ToolStripMenuItem mniQuickwatch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem mniOpenClient;
        private System.Windows.Forms.ToolStripMenuItem mniOpenSetup;
        private System.Windows.Forms.ToolStripMenuItem mniClearLog;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblRequiredBaseExp;
        private System.Windows.Forms.Label lblRequiredJobExp;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem mniHideClient;
        private System.Windows.Forms.ToolStripMenuItem mniOptimizeClientSize;
        private System.Windows.Forms.ToolStripMenuItem mniCharCalculator;
        private System.Windows.Forms.ToolStripMenuItem mniMobSearch;
        private System.Windows.Forms.ToolStripMenuItem mniServerLogin;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem mniParty;
        private System.Windows.Forms.Timer timerPartyServer;
        private System.Windows.Forms.ToolStripMenuItem mniHomunculusWatch;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.Label lblRequiredBaseTime;
        private System.Windows.Forms.Label lblRequiredJobTime;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Timer timerAutopotions;
        private System.Windows.Forms.ToolStripMenuItem mniCharts;
        private System.Windows.Forms.PictureBox pbxAutoWings;
        private System.Windows.Forms.PictureBox pbxAutoPotions;
        private System.Windows.Forms.ToolStripMenuItem mniUpdateAddresses;
        private System.Windows.Forms.ToolStripMenuItem mniSkillTimer;
        private System.Windows.Forms.ToolStripMenuItem mniMacro;
        private System.Windows.Forms.Label lblCompleted;
        private System.Windows.Forms.ToolStripMenuItem mniExpCounter;
        private System.Windows.Forms.ToolStripMenuItem mniPetWatch;
        private System.Windows.Forms.ToolStripMenuItem mniAutofeed;
        private System.Windows.Forms.ToolStripMenuItem mniCharState;
        private System.ComponentModel.BackgroundWorker bwVersionCheck;
    }
}

