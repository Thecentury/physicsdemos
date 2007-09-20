namespace FormingOfSignalFromSpecter
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
            this.Function = new Thecentury.GrapherControl();
            this.Spectrum = new Thecentury.GrapherControl();
            this.SetDefault = new System.Windows.Forms.Button();
            this.AddHarmonicButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Function
            // 
            this.Function.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Function.Cursor = System.Windows.Forms.Cursors.Default;
            this.Function.Location = new System.Drawing.Point(29, 93);
            this.Function.Name = "Function";
            this.Function.Size = new System.Drawing.Size(762, 281);
            this.Function.TabIndex = 0;
            // 
            // Spectrum
            // 
            this.Spectrum.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.Spectrum.Cursor = System.Windows.Forms.Cursors.Default;
            this.Spectrum.Location = new System.Drawing.Point(29, 442);
            this.Spectrum.Name = "Spectrum";
            this.Spectrum.Size = new System.Drawing.Size(762, 273);
            this.Spectrum.TabIndex = 1;
            // 
            // SetDefault
            // 
            this.SetDefault.Location = new System.Drawing.Point(29, 380);
            this.SetDefault.Name = "SetDefault";
            this.SetDefault.Size = new System.Drawing.Size(75, 23);
            this.SetDefault.TabIndex = 2;
            this.SetDefault.Text = "Репер";
            this.SetDefault.UseVisualStyleBackColor = true;
            this.SetDefault.Click += new System.EventHandler(this.SetDefault_Click);
            // 
            // AddHarmonicButton
            // 
            this.AddHarmonicButton.Location = new System.Drawing.Point(110, 380);
            this.AddHarmonicButton.Name = "AddHarmonicButton";
            this.AddHarmonicButton.Size = new System.Drawing.Size(133, 23);
            this.AddHarmonicButton.TabIndex = 3;
            this.AddHarmonicButton.Text = "Добавить гармонику ";
            this.AddHarmonicButton.UseVisualStyleBackColor = true;
            this.AddHarmonicButton.Click += new System.EventHandler(this.AddHarmonicButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 406);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Спектр функции";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(26, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(566, 24);
            this.label2.TabIndex = 5;
            this.label2.Text = "Формирование сигнала из спектра. Осцилляции Гиббса";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Функция";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(489, 388);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(302, 31);
            this.label4.TabIndex = 7;
            this.label4.Text = "Сумма N=0 гармоник";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.AddHarmonicButton);
            this.Controls.Add(this.SetDefault);
            this.Controls.Add(this.Spectrum);
            this.Controls.Add(this.Function);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(814, 728);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Thecentury.GrapherControl Function;
        private Thecentury.GrapherControl Spectrum;
        private System.Windows.Forms.Button SetDefault;
        private System.Windows.Forms.Button AddHarmonicButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
