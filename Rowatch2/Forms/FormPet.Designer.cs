namespace Rowatch2.Forms
{
    partial class FormPet
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormPet));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.lblName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.prbHungry = new ExtendedControls.ProgressbarEx();
            this.lblHungryText = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.prbFriendly = new ExtendedControls.ProgressbarEx();
            this.lblFriendlyText = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.lblName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.prbHungry, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblHungryText, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.prbFriendly, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.lblFriendlyText, 2, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Name = "label2";
            // 
            // prbHungry
            // 
            resources.ApplyResources(this.prbHungry, "prbHungry");
            this.prbHungry.EndColor = System.Drawing.Color.SlateBlue;
            this.prbHungry.Name = "prbHungry";
            this.prbHungry.StartColor = System.Drawing.Color.DeepSkyBlue;
            this.prbHungry.Value = 0;
            // 
            // lblHungryText
            // 
            resources.ApplyResources(this.lblHungryText, "lblHungryText");
            this.lblHungryText.Name = "lblHungryText";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label4.Name = "label4";
            // 
            // prbFriendly
            // 
            resources.ApplyResources(this.prbFriendly, "prbFriendly");
            this.prbFriendly.EndColor = System.Drawing.Color.SlateBlue;
            this.prbFriendly.Maximum = 1000;
            this.prbFriendly.Name = "prbFriendly";
            this.prbFriendly.StartColor = System.Drawing.Color.DeepSkyBlue;
            this.prbFriendly.Value = 0;
            // 
            // lblFriendlyText
            // 
            resources.ApplyResources(this.lblFriendlyText, "lblFriendlyText");
            this.lblFriendlyText.Name = "lblFriendlyText";
            // 
            // FormPet
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormPet";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblFriendlyText;
        private System.Windows.Forms.Label lblHungryText;
        private ExtendedControls.ProgressbarEx prbHungry;
        private ExtendedControls.ProgressbarEx prbFriendly;
    }
}