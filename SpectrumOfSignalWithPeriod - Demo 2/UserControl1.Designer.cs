namespace SpectrumOfSignalWithPeriod
{
    partial class UserControl1
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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.FunctionReper = new Thecentury.GrapherControl();
            this.SpectrumReper = new Thecentury.GrapherControl();
            this.Function = new Thecentury.GrapherControl();
            this.Spectrum = new Thecentury.GrapherControl();
            this.ReperTButton = new System.Windows.Forms.Button();
            this.ReperTauButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.PeriodBar = new Thecentury.ImprovedTrackBar.ImprovedTrackBar();
            this.DlitelnostBar = new Thecentury.ImprovedTrackBar.ImprovedTrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // FunctionReper
            // 
            this.FunctionReper.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.FunctionReper.Cursor = System.Windows.Forms.Cursors.Default;
            this.FunctionReper.Location = new System.Drawing.Point(3, 74);
            this.FunctionReper.Name = "FunctionReper";
            this.FunctionReper.Size = new System.Drawing.Size(520, 254);
            this.FunctionReper.TabIndex = 0;
            // 
            // SpectrumReper
            // 
            this.SpectrumReper.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.SpectrumReper.Cursor = System.Windows.Forms.Cursors.Default;
            this.SpectrumReper.Location = new System.Drawing.Point(529, 74);
            this.SpectrumReper.Name = "SpectrumReper";
            this.SpectrumReper.Size = new System.Drawing.Size(476, 254);
            this.SpectrumReper.TabIndex = 1;
            // 
            // Function
            // 
            this.Function.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Function.Cursor = System.Windows.Forms.Cursors.Default;
            this.Function.Location = new System.Drawing.Point(3, 334);
            this.Function.Name = "Function";
            this.Function.Size = new System.Drawing.Size(520, 254);
            this.Function.TabIndex = 2;
            // 
            // Spectrum
            // 
            this.Spectrum.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Spectrum.Cursor = System.Windows.Forms.Cursors.Default;
            this.Spectrum.Location = new System.Drawing.Point(529, 334);
            this.Spectrum.Name = "Spectrum";
            this.Spectrum.Size = new System.Drawing.Size(477, 254);
            this.Spectrum.TabIndex = 3;
            // 
            // ReperTButton
            // 
            this.ReperTButton.Enabled = false;
            this.ReperTButton.Location = new System.Drawing.Point(617, 623);
            this.ReperTButton.Name = "ReperTButton";
            this.ReperTButton.Size = new System.Drawing.Size(75, 23);
            this.ReperTButton.TabIndex = 8;
            this.ReperTButton.Text = "Репер";
            this.ReperTButton.UseVisualStyleBackColor = true;
            this.ReperTButton.Click += new System.EventHandler(this.ReperTButton_Click);
            // 
            // ReperTauButton
            // 
            this.ReperTauButton.Enabled = false;
            this.ReperTauButton.Location = new System.Drawing.Point(617, 690);
            this.ReperTauButton.Name = "ReperTauButton";
            this.ReperTauButton.Size = new System.Drawing.Size(75, 23);
            this.ReperTauButton.TabIndex = 11;
            this.ReperTauButton.Text = "Репер";
            this.ReperTauButton.UseVisualStyleBackColor = true;
            this.ReperTauButton.Click += new System.EventHandler(this.ReperTauButton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(16, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(527, 37);
            this.label3.TabIndex = 12;
            this.label3.Text = "Спектр периодического сигнала";
            // 
            // PeriodBar
            // 
            this.PeriodBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PeriodBar.Location = new System.Drawing.Point(263, 611);
            this.PeriodBar.Name = "PeriodBar";
            this.PeriodBar.Size = new System.Drawing.Size(348, 50);
            this.PeriodBar.TabIndex = 13;
            this.PeriodBar.ValueChanged += new Thecentury.ImprovedTrackBar.ValueChangedHandler(this.PeriodBar_ValueChanged);
            // 
            // DlitelnostBar
            // 
            this.DlitelnostBar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.DlitelnostBar.Location = new System.Drawing.Point(263, 674);
            this.DlitelnostBar.Name = "DlitelnostBar";
            this.DlitelnostBar.Size = new System.Drawing.Size(348, 50);
            this.DlitelnostBar.TabIndex = 14;
            this.DlitelnostBar.ValueChanged += new Thecentury.ImprovedTrackBar.ValueChangedHandler(this.DlitelnostBar_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(103, 623);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 31);
            this.label1.TabIndex = 15;
            this.label1.Text = "Период Т";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(105, 674);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 24);
            this.label2.TabIndex = 16;
            this.label2.Text = "Длительность";
            // 
            // UserControl1
            // 
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.DlitelnostBar);
            this.Controls.Add(this.PeriodBar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ReperTauButton);
            this.Controls.Add(this.ReperTButton);
            this.Controls.Add(this.Spectrum);
            this.Controls.Add(this.Function);
            this.Controls.Add(this.SpectrumReper);
            this.Controls.Add(this.FunctionReper);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1099, 781);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Thecentury.GrapherControl FunctionReper;
        private Thecentury.GrapherControl SpectrumReper;
        private Thecentury.GrapherControl Function;
        private Thecentury.GrapherControl Spectrum;
        private System.Windows.Forms.Button ReperTButton;
        private System.Windows.Forms.Button ReperTauButton;
        private System.Windows.Forms.Label label3;
        private Thecentury.ImprovedTrackBar.ImprovedTrackBar PeriodBar;
        private Thecentury.ImprovedTrackBar.ImprovedTrackBar DlitelnostBar;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}
