using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace PhysicsDemonstrations {
	public partial class DemonstrationInfo : UserControl {
		int xPos;
		int yPos;

		public event EventHandler RunDemo;
		protected void OnRunDemo() {
			if (RunDemo != null) {
				RunDemo(this, null);
			}
		}

		public DemonstrationInfo() {
			InitializeComponent();
		}

		public DemonstrationInfo(string name, string description, int xPos, int yPos) {
			InitializeComponent();
			nameLabel.Text = name;
			descriptionLabel.Text = description;
			this.yPos = yPos;
			this.xPos = xPos;
			this.Location = new Point(xPos, yPos);
		}

		private void DemoRunButton_Click(object sender, EventArgs e) {
			OnRunDemo();
		}
	}
}
