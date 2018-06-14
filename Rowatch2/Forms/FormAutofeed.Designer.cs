namespace Rowatch2.Forms
{
    partial class FormAutofeed
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutofeed));
            this.lblFeedButtonValue = new System.Windows.Forms.Label();
            this.lblConfirmButtonValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblText = new System.Windows.Forms.Label();
            this.btnRecord = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.timerFeed = new System.Windows.Forms.Timer(this.components);
            this.lblMinFeedValue = new System.Windows.Forms.Label();
            this.nudMinFeedValue = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFeedValue)).BeginInit();
            this.SuspendLayout();
            // 
            // lblFeedButtonValue
            // 
            resources.ApplyResources(this.lblFeedButtonValue, "lblFeedButtonValue");
            this.lblFeedButtonValue.Name = "lblFeedButtonValue";
            // 
            // lblConfirmButtonValue
            // 
            resources.ApplyResources(this.lblConfirmButtonValue, "lblConfirmButtonValue");
            this.lblConfirmButtonValue.Name = "lblConfirmButtonValue";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblText
            // 
            resources.ApplyResources(this.lblText, "lblText");
            this.lblText.Name = "lblText";
            // 
            // btnRecord
            // 
            resources.ApplyResources(this.btnRecord, "btnRecord");
            this.btnRecord.Name = "btnRecord";
            this.btnRecord.UseVisualStyleBackColor = true;
            this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // timerFeed
            // 
            this.timerFeed.Interval = 1000;
            this.timerFeed.Tick += new System.EventHandler(this.timerFeed_Tick);
            // 
            // lblMinFeedValue
            // 
            resources.ApplyResources(this.lblMinFeedValue, "lblMinFeedValue");
            this.lblMinFeedValue.Name = "lblMinFeedValue";
            // 
            // nudMinFeedValue
            // 
            resources.ApplyResources(this.nudMinFeedValue, "nudMinFeedValue");
            this.nudMinFeedValue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudMinFeedValue.Name = "nudMinFeedValue";
            this.nudMinFeedValue.Value = new decimal(new int[] {
            65,
            0,
            0,
            0});
            // 
            // FormAutofeed
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.nudMinFeedValue);
            this.Controls.Add(this.lblMinFeedValue);
            this.Controls.Add(this.lblFeedButtonValue);
            this.Controls.Add(this.lblConfirmButtonValue);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblText);
            this.Controls.Add(this.btnRecord);
            this.Controls.Add(this.btnStart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormAutofeed";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.nudMinFeedValue)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblFeedButtonValue;
        private System.Windows.Forms.Label lblConfirmButtonValue;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblText;
        private System.Windows.Forms.Button btnRecord;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Timer timerFeed;
        private System.Windows.Forms.Label lblMinFeedValue;
        private System.Windows.Forms.NumericUpDown nudMinFeedValue;
    }
}