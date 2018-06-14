namespace Rowatch2.Forms
{
    partial class FormSkillTimer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormSkillTimer));
            this.prbHP = new ExtendedControls.ProgressbarEx();
            this.lblLeft = new System.Windows.Forms.Label();
            this.nudIntervall = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.cbxKey = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.timerRefresh = new System.Windows.Forms.Timer(this.components);
            this.timerKey = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervall)).BeginInit();
            this.SuspendLayout();
            // 
            // prbHP
            // 
            resources.ApplyResources(this.prbHP, "prbHP");
            this.prbHP.EndColor = System.Drawing.Color.SlateBlue;
            this.prbHP.Name = "prbHP";
            this.prbHP.StartColor = System.Drawing.Color.CornflowerBlue;
            this.prbHP.Value = 0;
            // 
            // lblLeft
            // 
            resources.ApplyResources(this.lblLeft, "lblLeft");
            this.lblLeft.Name = "lblLeft";
            // 
            // nudIntervall
            // 
            resources.ApplyResources(this.nudIntervall, "nudIntervall");
            this.nudIntervall.Maximum = new decimal(new int[] {
            60000,
            0,
            0,
            0});
            this.nudIntervall.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudIntervall.Name = "nudIntervall";
            this.nudIntervall.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // cbxKey
            // 
            resources.ApplyResources(this.cbxKey, "cbxKey");
            this.cbxKey.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKey.FormattingEnabled = true;
            this.cbxKey.Items.AddRange(new object[] {
            resources.GetString("cbxKey.Items"),
            resources.GetString("cbxKey.Items1"),
            resources.GetString("cbxKey.Items2"),
            resources.GetString("cbxKey.Items3"),
            resources.GetString("cbxKey.Items4"),
            resources.GetString("cbxKey.Items5"),
            resources.GetString("cbxKey.Items6"),
            resources.GetString("cbxKey.Items7"),
            resources.GetString("cbxKey.Items8")});
            this.cbxKey.Name = "cbxKey";
            this.cbxKey.SelectedIndexChanged += new System.EventHandler(this.cbxKey_SelectedIndexChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // timerRefresh
            // 
            this.timerRefresh.Interval = 500;
            this.timerRefresh.Tick += new System.EventHandler(this.timerRefresh_Tick);
            // 
            // timerKey
            // 
            this.timerKey.Enabled = true;
            this.timerKey.Interval = 500;
            this.timerKey.Tick += new System.EventHandler(this.timerKey_Tick);
            // 
            // FormSkillTimer
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbxKey);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.nudIntervall);
            this.Controls.Add(this.lblLeft);
            this.Controls.Add(this.prbHP);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.Name = "FormSkillTimer";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.nudIntervall)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ExtendedControls.ProgressbarEx prbHP;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.NumericUpDown nudIntervall;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbxKey;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timerRefresh;
        private System.Windows.Forms.Timer timerKey;

    }
}