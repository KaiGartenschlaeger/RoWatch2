namespace Rowatch2.Forms
{
    partial class FormMinimized
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMinimized));
            this.btnBack = new System.Windows.Forms.Button();
            this.lblRequiredJobExp = new System.Windows.Forms.Label();
            this.lblRequiredBaseExp = new System.Windows.Forms.Label();
            this.lblKilledMobs = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblJobExpHour = new System.Windows.Forms.Label();
            this.lblGainedJobExp = new System.Windows.Forms.Label();
            this.lblBaseExpHour = new System.Windows.Forms.Label();
            this.lblGainedBaseExp = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblJobExpValues = new System.Windows.Forms.Label();
            this.lblJobPercent = new System.Windows.Forms.Label();
            this.lblBasePercent = new System.Windows.Forms.Label();
            this.lblBaseExpValues = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            resources.ApplyResources(this.btnBack, "btnBack");
            this.btnBack.Name = "btnBack";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblRequiredJobExp
            // 
            resources.ApplyResources(this.lblRequiredJobExp, "lblRequiredJobExp");
            this.lblRequiredJobExp.Name = "lblRequiredJobExp";
            // 
            // lblRequiredBaseExp
            // 
            resources.ApplyResources(this.lblRequiredBaseExp, "lblRequiredBaseExp");
            this.lblRequiredBaseExp.Name = "lblRequiredBaseExp";
            // 
            // lblKilledMobs
            // 
            resources.ApplyResources(this.lblKilledMobs, "lblKilledMobs");
            this.lblKilledMobs.Name = "lblKilledMobs";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // lblJobExpHour
            // 
            resources.ApplyResources(this.lblJobExpHour, "lblJobExpHour");
            this.lblJobExpHour.Name = "lblJobExpHour";
            // 
            // lblGainedJobExp
            // 
            resources.ApplyResources(this.lblGainedJobExp, "lblGainedJobExp");
            this.lblGainedJobExp.Name = "lblGainedJobExp";
            // 
            // lblBaseExpHour
            // 
            resources.ApplyResources(this.lblBaseExpHour, "lblBaseExpHour");
            this.lblBaseExpHour.Name = "lblBaseExpHour";
            // 
            // lblGainedBaseExp
            // 
            resources.ApplyResources(this.lblGainedBaseExp, "lblGainedBaseExp");
            this.lblGainedBaseExp.Name = "lblGainedBaseExp";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblJobExpValues
            // 
            resources.ApplyResources(this.lblJobExpValues, "lblJobExpValues");
            this.lblJobExpValues.Name = "lblJobExpValues";
            // 
            // lblJobPercent
            // 
            resources.ApplyResources(this.lblJobPercent, "lblJobPercent");
            this.lblJobPercent.Name = "lblJobPercent";
            // 
            // lblBasePercent
            // 
            resources.ApplyResources(this.lblBasePercent, "lblBasePercent");
            this.lblBasePercent.Name = "lblBasePercent";
            // 
            // lblBaseExpValues
            // 
            resources.ApplyResources(this.lblBaseExpValues, "lblBaseExpValues");
            this.lblBaseExpValues.Name = "lblBaseExpValues";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // FormMinimized
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.lblRequiredJobExp);
            this.Controls.Add(this.lblGainedBaseExp);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblBaseExpHour);
            this.Controls.Add(this.lblRequiredBaseExp);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.lblBaseExpValues);
            this.Controls.Add(this.lblGainedJobExp);
            this.Controls.Add(this.lblKilledMobs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblBasePercent);
            this.Controls.Add(this.lblJobExpHour);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.lblJobExpValues);
            this.Controls.Add(this.lblJobPercent);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMinimized";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMinimized_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblRequiredJobExp;
        private System.Windows.Forms.Label lblRequiredBaseExp;
        private System.Windows.Forms.Label lblKilledMobs;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblJobExpHour;
        private System.Windows.Forms.Label lblGainedJobExp;
        private System.Windows.Forms.Label lblBaseExpHour;
        private System.Windows.Forms.Label lblGainedBaseExp;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblJobExpValues;
        private System.Windows.Forms.Label lblJobPercent;
        private System.Windows.Forms.Label lblBasePercent;
        private System.Windows.Forms.Label lblBaseExpValues;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;

    }
}