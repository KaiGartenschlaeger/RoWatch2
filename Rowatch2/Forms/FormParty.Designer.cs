namespace Rowatch2.Forms
{
    partial class FormParty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormParty));
            this.lvwMember = new ExtendedControls.ListViewEx();
            this.clmnName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnLevel = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnMap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnHPSP = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnBaseExp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnJobExp = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnKills = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // lvwMember
            // 
            resources.ApplyResources(this.lvwMember, "lvwMember");
            this.lvwMember.AllowColumnReorder = true;
            this.lvwMember.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnName,
            this.clmnLevel,
            this.clmnMap,
            this.clmnHPSP,
            this.clmnBaseExp,
            this.clmnJobExp,
            this.clmnKills});
            this.lvwMember.ExplorerTheme = true;
            this.lvwMember.FullRowSelect = true;
            this.lvwMember.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvwMember.Name = "lvwMember";
            this.lvwMember.UseCompatibleStateImageBehavior = false;
            this.lvwMember.View = System.Windows.Forms.View.Details;
            this.lvwMember.VirtualMode = true;
            this.lvwMember.RetrieveVirtualItem += new System.Windows.Forms.RetrieveVirtualItemEventHandler(this.lvwMember_RetrieveVirtualItem);
            // 
            // clmnName
            // 
            resources.ApplyResources(this.clmnName, "clmnName");
            // 
            // clmnLevel
            // 
            resources.ApplyResources(this.clmnLevel, "clmnLevel");
            // 
            // clmnMap
            // 
            resources.ApplyResources(this.clmnMap, "clmnMap");
            // 
            // clmnHPSP
            // 
            resources.ApplyResources(this.clmnHPSP, "clmnHPSP");
            // 
            // clmnBaseExp
            // 
            resources.ApplyResources(this.clmnBaseExp, "clmnBaseExp");
            // 
            // clmnJobExp
            // 
            resources.ApplyResources(this.clmnJobExp, "clmnJobExp");
            // 
            // clmnKills
            // 
            resources.ApplyResources(this.clmnKills, "clmnKills");
            // 
            // FormParty
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lvwMember);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.Name = "FormParty";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.ListViewEx lvwMember;
        private System.Windows.Forms.ColumnHeader clmnName;
        private System.Windows.Forms.ColumnHeader clmnHPSP;
        private System.Windows.Forms.ColumnHeader clmnBaseExp;
        private System.Windows.Forms.ColumnHeader clmnJobExp;
        private System.Windows.Forms.ColumnHeader clmnMap;
        private System.Windows.Forms.ColumnHeader clmnKills;
        private System.Windows.Forms.ColumnHeader clmnLevel;
    }
}