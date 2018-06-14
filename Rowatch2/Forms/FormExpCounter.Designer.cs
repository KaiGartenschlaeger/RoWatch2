namespace Rowatch2.Forms
{
    partial class FormExpCounter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormExpCounter));
            this.lvwCounter = new System.Windows.Forms.ListView();
            this.clmnExp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnCounter = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwCounter
            // 
            resources.ApplyResources(this.lvwCounter, "lvwCounter");
            this.lvwCounter.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnExp,
            this.clmnCounter});
            this.lvwCounter.FullRowSelect = true;
            this.lvwCounter.GridLines = true;
            this.lvwCounter.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwCounter.Name = "lvwCounter";
            this.lvwCounter.UseCompatibleStateImageBehavior = false;
            this.lvwCounter.View = System.Windows.Forms.View.Details;
            // 
            // clmnExp
            // 
            resources.ApplyResources(this.clmnExp, "clmnExp");
            // 
            // clmnCounter
            // 
            resources.ApplyResources(this.clmnCounter, "clmnCounter");
            // 
            // FormExpCounter
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvwCounter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormExpCounter";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwCounter;
        private System.Windows.Forms.ColumnHeader clmnCounter;
        private System.Windows.Forms.ColumnHeader clmnExp;
    }
}