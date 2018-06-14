namespace Rowatch2.Forms
{
    partial class FormMacroExecution
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMacroExecution));
            this.lblCurrentAction = new System.Windows.Forms.Label();
            this.lblCurrentActionValue = new System.Windows.Forms.Label();
            this.btnStart = new System.Windows.Forms.Button();
            this.chbRepeat = new System.Windows.Forms.CheckBox();
            this.bwMacro = new System.ComponentModel.BackgroundWorker();
            this.btnStop = new System.Windows.Forms.Button();
            this.lblCounter = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCurrentAction
            // 
            resources.ApplyResources(this.lblCurrentAction, "lblCurrentAction");
            this.lblCurrentAction.Name = "lblCurrentAction";
            // 
            // lblCurrentActionValue
            // 
            resources.ApplyResources(this.lblCurrentActionValue, "lblCurrentActionValue");
            this.lblCurrentActionValue.Name = "lblCurrentActionValue";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // chbRepeat
            // 
            resources.ApplyResources(this.chbRepeat, "chbRepeat");
            this.chbRepeat.Name = "chbRepeat";
            this.chbRepeat.UseVisualStyleBackColor = true;
            // 
            // bwMacro
            // 
            this.bwMacro.WorkerReportsProgress = true;
            this.bwMacro.WorkerSupportsCancellation = true;
            this.bwMacro.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwMacro_DoWork);
            this.bwMacro.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwMacro_ProgressChanged);
            this.bwMacro.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwMacro_RunWorkerCompleted);
            // 
            // btnStop
            // 
            resources.ApplyResources(this.btnStop, "btnStop");
            this.btnStop.Name = "btnStop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // lblCounter
            // 
            resources.ApplyResources(this.lblCounter, "lblCounter");
            this.lblCounter.Name = "lblCounter";
            // 
            // FormMacroExecution
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.chbRepeat);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.lblCurrentActionValue);
            this.Controls.Add(this.lblCurrentAction);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MagneticType = ((ExtendedControls.MyForm_MagneticType)(((ExtendedControls.MyForm_MagneticType.Working | ExtendedControls.MyForm_MagneticType.Desktop) 
            | ExtendedControls.MyForm_MagneticType.Forms)));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormMacroExecution";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMacroExecution_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCurrentAction;
        private System.Windows.Forms.Label lblCurrentActionValue;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.CheckBox chbRepeat;
        private System.ComponentModel.BackgroundWorker bwMacro;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Label lblCounter;

    }
}