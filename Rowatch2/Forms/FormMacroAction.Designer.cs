namespace Rowatch2.Forms
{
    partial class FormMacroAction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMacroAction));
            this.nudMinutes = new System.Windows.Forms.NumericUpDown();
            this.nudSeconds = new System.Windows.Forms.NumericUpDown();
            this.nudMiliseconds = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.cbxKeys = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chbDelay = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chbKey = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiliseconds)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nudMinutes
            // 
            resources.ApplyResources(this.nudMinutes, "nudMinutes");
            this.nudMinutes.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMinutes.Name = "nudMinutes";
            // 
            // nudSeconds
            // 
            resources.ApplyResources(this.nudSeconds, "nudSeconds");
            this.nudSeconds.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudSeconds.Name = "nudSeconds";
            // 
            // nudMiliseconds
            // 
            resources.ApplyResources(this.nudMiliseconds, "nudMiliseconds");
            this.nudMiliseconds.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.nudMiliseconds.Name = "nudMiliseconds";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // cbxKeys
            // 
            resources.ApplyResources(this.cbxKeys, "cbxKeys");
            this.cbxKeys.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxKeys.FormattingEnabled = true;
            this.cbxKeys.Name = "cbxKeys";
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.nudMinutes);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.nudMiliseconds);
            this.groupBox1.Controls.Add(this.nudSeconds);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // chbDelay
            // 
            resources.ApplyResources(this.chbDelay, "chbDelay");
            this.chbDelay.BackColor = System.Drawing.Color.Transparent;
            this.chbDelay.Name = "chbDelay";
            this.chbDelay.UseVisualStyleBackColor = false;
            this.chbDelay.CheckedChanged += new System.EventHandler(this.chbDelay_CheckedChanged);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.cbxKeys);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // chbKey
            // 
            resources.ApplyResources(this.chbKey, "chbKey");
            this.chbKey.Name = "chbKey";
            this.chbKey.UseVisualStyleBackColor = true;
            this.chbKey.CheckedChanged += new System.EventHandler(this.chbKey_CheckedChanged);
            // 
            // FormMacroAction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chbKey);
            this.Controls.Add(this.chbDelay);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.Name = "FormMacroAction";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.nudMinutes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudSeconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMiliseconds)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NumericUpDown nudMinutes;
        private System.Windows.Forms.NumericUpDown nudSeconds;
        private System.Windows.Forms.NumericUpDown nudMiliseconds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbxKeys;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox chbDelay;
        private System.Windows.Forms.CheckBox chbKey;


    }
}