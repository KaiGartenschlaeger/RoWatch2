namespace Rowatch2.Forms
{
    partial class FormCharts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormCharts));
            this.barChart1 = new ExtendedControls.Charts.BarChart();
            this.cbxChartType = new System.Windows.Forms.ComboBox();
            this.cbxTime = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // barChart1
            // 
            resources.ApplyResources(this.barChart1, "barChart1");
            this.barChart1.AxesColor = System.Drawing.Color.Black;
            this.barChart1.AxesFont = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.barChart1.AxesFontColor = System.Drawing.Color.Black;
            this.barChart1.AxesPadding = 10;
            this.barChart1.AxesTextCount = 7;
            this.barChart1.BackColor = System.Drawing.Color.White;
            this.barChart1.BackLinesColor = System.Drawing.Color.DarkGray;
            this.barChart1.LineEndColor = System.Drawing.Color.Firebrick;
            this.barChart1.LineStartColor = System.Drawing.Color.LimeGreen;
            this.barChart1.Name = "barChart1";
            this.barChart1.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;
            // 
            // cbxChartType
            // 
            resources.ApplyResources(this.cbxChartType, "cbxChartType");
            this.cbxChartType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxChartType.FormattingEnabled = true;
            this.cbxChartType.Items.AddRange(new object[] {
            resources.GetString("cbxChartType.Items"),
            resources.GetString("cbxChartType.Items1"),
            resources.GetString("cbxChartType.Items2"),
            resources.GetString("cbxChartType.Items3")});
            this.cbxChartType.Name = "cbxChartType";
            this.cbxChartType.SelectedIndexChanged += new System.EventHandler(this.cbxChartType_SelectedIndexChanged);
            // 
            // cbxTime
            // 
            resources.ApplyResources(this.cbxTime, "cbxTime");
            this.cbxTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxTime.FormattingEnabled = true;
            this.cbxTime.Items.AddRange(new object[] {
            resources.GetString("cbxTime.Items"),
            resources.GetString("cbxTime.Items1"),
            resources.GetString("cbxTime.Items2"),
            resources.GetString("cbxTime.Items3"),
            resources.GetString("cbxTime.Items4")});
            this.cbxTime.Name = "cbxTime";
            this.cbxTime.SelectedIndexChanged += new System.EventHandler(this.cbxTime_SelectedIndexChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.cbxChartType);
            this.panel1.Controls.Add(this.cbxTime);
            this.panel1.Name = "panel1";
            // 
            // FormCharts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.barChart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormCharts";
            this.ShowInTaskbar = false;
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ExtendedControls.Charts.BarChart barChart1;
        private System.Windows.Forms.ComboBox cbxChartType;
        private System.Windows.Forms.ComboBox cbxTime;
        private System.Windows.Forms.Panel panel1;
    }
}