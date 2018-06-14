namespace PluginSample
{
    partial class FormDebug
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
            this.lbxLog = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // lbxLog
            // 
            this.lbxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbxLog.FormattingEnabled = true;
            this.lbxLog.IntegralHeight = false;
            this.lbxLog.Location = new System.Drawing.Point(0, 0);
            this.lbxLog.Name = "lbxLog";
            this.lbxLog.Size = new System.Drawing.Size(421, 261);
            this.lbxLog.TabIndex = 0;
            // 
            // FormDebug
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 261);
            this.Controls.Add(this.lbxLog);
            this.Name = "FormDebug";
            this.Text = "Sample Plugin";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbxLog;
    }
}