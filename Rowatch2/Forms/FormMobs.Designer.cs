namespace Rowatch2.Forms
{
    partial class FormMobs
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMobs));
            this.lvwMobs = new System.Windows.Forms.ListView();
            this.clmnID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnLV = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHit = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnFlee = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnBExp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnJExp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnElement = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnRace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnScale = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.nudServerExpFactor = new System.Windows.Forms.NumericUpDown();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chbOptimizedLevel = new System.Windows.Forms.CheckBox();
            this.lnkMobinfo = new System.Windows.Forms.LinkLabel();
            this.tbxSearch = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.nudOptLvlDiff = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.chbBosMobs = new System.Windows.Forms.CheckBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tbxMaxLvl = new System.Windows.Forms.TextBox();
            this.tbxMinLvl = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.cbxRace = new System.Windows.Forms.ComboBox();
            this.cbxElement = new System.Windows.Forms.ComboBox();
            this.lblSearchstatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nudServerExpFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOptLvlDiff)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwMobs
            // 
            resources.ApplyResources(this.lvwMobs, "lvwMobs");
            this.lvwMobs.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnID,
            this.clmnName,
            this.clmnLV,
            this.clmnHit,
            this.clmnFlee,
            this.clmnBExp,
            this.clmnJExp,
            this.clmnElement,
            this.clmnRace,
            this.clmnScale});
            this.lvwMobs.FullRowSelect = true;
            this.lvwMobs.GridLines = true;
            this.lvwMobs.HideSelection = false;
            this.lvwMobs.Name = "lvwMobs";
            this.lvwMobs.UseCompatibleStateImageBehavior = false;
            this.lvwMobs.View = System.Windows.Forms.View.Details;
            this.lvwMobs.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lvwMobs_ColumnClick);
            this.lvwMobs.SelectedIndexChanged += new System.EventHandler(this.lvwMobs_SelectedIndexChanged);
            // 
            // clmnID
            // 
            resources.ApplyResources(this.clmnID, "clmnID");
            // 
            // clmnName
            // 
            resources.ApplyResources(this.clmnName, "clmnName");
            // 
            // clmnLV
            // 
            resources.ApplyResources(this.clmnLV, "clmnLV");
            // 
            // clmnHit
            // 
            resources.ApplyResources(this.clmnHit, "clmnHit");
            // 
            // clmnFlee
            // 
            resources.ApplyResources(this.clmnFlee, "clmnFlee");
            // 
            // clmnBExp
            // 
            resources.ApplyResources(this.clmnBExp, "clmnBExp");
            // 
            // clmnJExp
            // 
            resources.ApplyResources(this.clmnJExp, "clmnJExp");
            // 
            // clmnElement
            // 
            resources.ApplyResources(this.clmnElement, "clmnElement");
            // 
            // clmnRace
            // 
            resources.ApplyResources(this.clmnRace, "clmnRace");
            // 
            // clmnScale
            // 
            resources.ApplyResources(this.clmnScale, "clmnScale");
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // nudServerExpFactor
            // 
            resources.ApplyResources(this.nudServerExpFactor, "nudServerExpFactor");
            this.nudServerExpFactor.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudServerExpFactor.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.nudServerExpFactor.Name = "nudServerExpFactor";
            this.nudServerExpFactor.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // btnRefresh
            // 
            resources.ApplyResources(this.btnRefresh, "btnRefresh");
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chbOptimizedLevel
            // 
            resources.ApplyResources(this.chbOptimizedLevel, "chbOptimizedLevel");
            this.chbOptimizedLevel.Checked = true;
            this.chbOptimizedLevel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbOptimizedLevel.Name = "chbOptimizedLevel";
            this.chbOptimizedLevel.UseVisualStyleBackColor = true;
            // 
            // lnkMobinfo
            // 
            resources.ApplyResources(this.lnkMobinfo, "lnkMobinfo");
            this.lnkMobinfo.Name = "lnkMobinfo";
            this.lnkMobinfo.TabStop = true;
            this.lnkMobinfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkMobinfo_LinkClicked);
            // 
            // tbxSearch
            // 
            resources.ApplyResources(this.tbxSearch, "tbxSearch");
            this.tbxSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxSearch.Name = "tbxSearch";
            this.tbxSearch.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tbxSearch_KeyUp);
            // 
            // btnSearch
            // 
            resources.ApplyResources(this.btnSearch, "btnSearch");
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // nudOptLvlDiff
            // 
            resources.ApplyResources(this.nudOptLvlDiff, "nudOptLvlDiff");
            this.nudOptLvlDiff.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudOptLvlDiff.Name = "nudOptLvlDiff";
            this.nudOptLvlDiff.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.chbBosMobs);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.nudOptLvlDiff);
            this.panel1.Controls.Add(this.nudServerExpFactor);
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.chbOptimizedLevel);
            this.panel1.Name = "panel1";
            // 
            // chbBosMobs
            // 
            resources.ApplyResources(this.chbBosMobs, "chbBosMobs");
            this.chbBosMobs.Name = "chbBosMobs";
            this.chbBosMobs.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            resources.ApplyResources(this.panel2, "panel2");
            this.panel2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tbxMaxLvl);
            this.panel2.Controls.Add(this.tbxMinLvl);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.cbxRace);
            this.panel2.Controls.Add(this.cbxElement);
            this.panel2.Controls.Add(this.tbxSearch);
            this.panel2.Controls.Add(this.btnSearch);
            this.panel2.Name = "panel2";
            // 
            // tbxMaxLvl
            // 
            resources.ApplyResources(this.tbxMaxLvl, "tbxMaxLvl");
            this.tbxMaxLvl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxMaxLvl.Name = "tbxMaxLvl";
            // 
            // tbxMinLvl
            // 
            resources.ApplyResources(this.tbxMinLvl, "tbxMinLvl");
            this.tbxMinLvl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tbxMinLvl.Name = "tbxMinLvl";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // cbxRace
            // 
            resources.ApplyResources(this.cbxRace, "cbxRace");
            this.cbxRace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxRace.FormattingEnabled = true;
            this.cbxRace.Name = "cbxRace";
            // 
            // cbxElement
            // 
            resources.ApplyResources(this.cbxElement, "cbxElement");
            this.cbxElement.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxElement.FormattingEnabled = true;
            this.cbxElement.Name = "cbxElement";
            // 
            // lblSearchstatus
            // 
            resources.ApplyResources(this.lblSearchstatus, "lblSearchstatus");
            this.lblSearchstatus.Name = "lblSearchstatus";
            // 
            // FormMobs
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblSearchstatus);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lnkMobinfo);
            this.Controls.Add(this.lvwMobs);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMobs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.nudServerExpFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudOptLvlDiff)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView lvwMobs;
        private System.Windows.Forms.ColumnHeader clmnName;
        private System.Windows.Forms.ColumnHeader clmnLV;
        private System.Windows.Forms.ColumnHeader clmnBExp;
        private System.Windows.Forms.ColumnHeader clmnJExp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nudServerExpFactor;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.CheckBox chbOptimizedLevel;
        private System.Windows.Forms.LinkLabel lnkMobinfo;
        private System.Windows.Forms.TextBox tbxSearch;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ColumnHeader clmnElement;
        private System.Windows.Forms.ColumnHeader clmnRace;
        private System.Windows.Forms.ColumnHeader clmnScale;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown nudOptLvlDiff;
        private System.Windows.Forms.ColumnHeader clmnID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbxRace;
        private System.Windows.Forms.ComboBox cbxElement;
        private System.Windows.Forms.Label lblSearchstatus;
        private System.Windows.Forms.TextBox tbxMaxLvl;
        private System.Windows.Forms.TextBox tbxMinLvl;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader clmnHit;
        private System.Windows.Forms.ColumnHeader clmnFlee;
        private System.Windows.Forms.CheckBox chbBosMobs;
    }
}