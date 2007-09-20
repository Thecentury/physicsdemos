namespace PhysicsDemonstrations
{
    partial class DemonstrationInfo
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
			this.nameLabel = new System.Windows.Forms.Label();
			this.DemoRunButton = new System.Windows.Forms.Button();
			this.descriptionLabel = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// nameLabel
			// 
			this.nameLabel.AutoSize = true;
			this.nameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.nameLabel.ForeColor = System.Drawing.Color.Black;
			this.nameLabel.Location = new System.Drawing.Point(13, 25);
			this.nameLabel.Name = "nameLabel";
			this.nameLabel.Size = new System.Drawing.Size(65, 13);
			this.nameLabel.TabIndex = 0;
			this.nameLabel.Text = "Название";
			// 
			// DemoRunButton
			// 
			this.DemoRunButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.DemoRunButton.Location = new System.Drawing.Point(714, 25);
			this.DemoRunButton.Name = "DemoRunButton";
			this.DemoRunButton.Size = new System.Drawing.Size(75, 23);
			this.DemoRunButton.TabIndex = 1;
			this.DemoRunButton.Text = "Запустить";
			this.DemoRunButton.UseVisualStyleBackColor = true;
			this.DemoRunButton.Click += new System.EventHandler(this.DemoRunButton_Click);
			// 
			// descriptionLabel
			// 
			this.descriptionLabel.AutoSize = true;
			this.descriptionLabel.Location = new System.Drawing.Point(169, 25);
			this.descriptionLabel.MaximumSize = new System.Drawing.Size(1000, 0);
			this.descriptionLabel.MinimumSize = new System.Drawing.Size(450, 0);
			this.descriptionLabel.Name = "descriptionLabel";
			this.descriptionLabel.Size = new System.Drawing.Size(450, 13);
			this.descriptionLabel.TabIndex = 2;
			this.descriptionLabel.Text = "Описание";
			// 
			// DemonstrationInfo
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.DarkGray;
			this.Controls.Add(this.descriptionLabel);
			this.Controls.Add(this.DemoRunButton);
			this.Controls.Add(this.nameLabel);
			this.Name = "DemonstrationInfo";
			this.Size = new System.Drawing.Size(804, 77);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label nameLabel;
        private System.Windows.Forms.Button DemoRunButton;
        private System.Windows.Forms.Label descriptionLabel;


    }
}
