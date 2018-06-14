namespace Rowatch2.Forms
{
    partial class FormCharState
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCharState));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label5 = new System.Windows.Forms.Label();
            this.prbHP = new ExtendedControls.ProgressbarEx();
            this.lblHpValue = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.prbSP = new ExtendedControls.ProgressbarEx();
            this.lblSpValue = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            resources.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.prbHP, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.lblHpValue, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.prbSP, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.lblSpValue, 2, 1);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label5.Name = "label5";
            // 
            // prbHP
            // 
            resources.ApplyResources(this.prbHP, "prbHP");
            this.prbHP.EndColor = System.Drawing.Color.SlateBlue;
            this.prbHP.Maximum = 999999;
            this.prbHP.Name = "prbHP";
            this.prbHP.StartColor = System.Drawing.Color.DeepSkyBlue;
            this.prbHP.Value = 0;
            // 
            // lblHpValue
            // 
            resources.ApplyResources(this.lblHpValue, "lblHpValue");
            this.lblHpValue.Name = "lblHpValue";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label3.Name = "label3";
            // 
            // prbSP
            // 
            resources.ApplyResources(this.prbSP, "prbSP");
            this.prbSP.EndColor = System.Drawing.Color.SlateBlue;
            this.prbSP.Maximum = 999999;
            this.prbSP.Name = "prbSP";
            this.prbSP.StartColor = System.Drawing.Color.DeepSkyBlue;
            this.prbSP.Value = 0;
            // 
            // lblSpValue
            // 
            resources.ApplyResources(this.lblSpValue, "lblSpValue");
            this.lblSpValue.Name = "lblSpValue";
            // 
            // FormCharState
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCharState";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label5;
        private ExtendedControls.ProgressbarEx prbHP;
        private System.Windows.Forms.Label lblHpValue;
        private System.Windows.Forms.Label label3;
        private ExtendedControls.ProgressbarEx prbSP;
        private System.Windows.Forms.Label lblSpValue;
    }
}