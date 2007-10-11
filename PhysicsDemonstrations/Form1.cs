using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace PhysicsDemonstrations {
	public partial class Form1 : Form {
		NDemoList demonstrations = new NDemoList();
		Control currentDemoControl;

		public Form1() {
			InitializeComponent();
			this.demonstrations.RunDemo += new EventHandler(demonstrations_RunDemo);
			//timer1.Start();
			PlaceDemos();
			demonstrations.DisableControls();
			this.button1.TabStop = false;
		}

		private void PlaceDemos() {
			noLabel.Visible = demonstrations.IsEmpty;
			if (!demonstrations.IsEmpty) {
				foreach (Control c in demonstrations.Controls) {
					Controls.Add(c);
				}
			}
		}

		/// <summary>
		/// Нажатие кнопки "Запустить"
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void demonstrations_RunDemo(object sender, EventArgs e) {
			Thecentury.GeneralDemonstration demo = sender as Thecentury.GeneralDemonstration;
			demo.MainControl.Location = new Point(0, 20);
			demo.MainControlChanged += new EventHandler(demo_MainControlChanged);

			foreach (Control c in Controls) {
				c.Enabled = false;
				c.Visible = false;
			}

			toolStrip1.Visible = true;
			toolStrip1.Enabled = true;

			this.CloseDemoButton.Enabled = true;
			this.CloseDemoButton.Visible = true;
			this.Controls.Add(demo.MainControl);
			currentDemoControl = demo.MainControl;
		}

		void demo_MainControlChanged(object sender, EventArgs e) {
			this.Invalidate();
		}

		private void timer1_Tick(object sender, EventArgs e) {
			//timer1.Stop();
			pictureBox1.Visible = false;
			pictureBox1.Dispose();
			Controls.Remove(pictureBox1);

			label1.Visible = true;
			noLabel.Visible = demonstrations.IsEmpty;
			demonstrations.EnableControls();

			this.RefreshButton.Visible = true;
			this.RefreshButton.Enabled = true;
			this.toolStrip1.Enabled = true;
			this.toolStrip1.Visible = true;
			this.button1.Dispose();
			this.button2.Dispose();
			//this.button1.Enabled = false;
			//this.button1.Visible = false;
			//this.button2.Enabled = false;
			//this.button2.Visible = false;
		}

		private void Form1_KeyPress(object sender, KeyPressEventArgs e) {
			if ((e.KeyChar == (char)Keys.Enter) /*&& (timer1.Enabled)*/)
				timer1_Tick(null, null);
		}

		/// <summary>
		/// Обновить
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void RefreshButton_Click(object sender, EventArgs e) {
			foreach (Control c in demonstrations.Controls) {
				Controls.Remove(c);
			}
			demonstrations = new NDemoList();
			this.demonstrations.RunDemo += new EventHandler(demonstrations_RunDemo);
			PlaceDemos();
		}

		/// <summary>
		///  Закрыть демонстрацию
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseDemoButton_Click(object sender, EventArgs e) {
			Controls.Remove(currentDemoControl);
			
			this.SuspendLayout();
			foreach (Control c in Controls) {
				c.Enabled = true;
				c.Visible = true;
			}
			noLabel.Visible = demonstrations.IsEmpty;

			this.CloseDemoButton.Visible = false;
			this.CloseDemoButton.Enabled = false;
			this.button1.Enabled = false;
			this.button1.Visible = false;

			this.ResumeLayout();
		}

		/// <summary>
		/// Выход
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void файлToolStripMenuItem_Click(object sender, EventArgs e) {
			this.Close();
		}

		private void button1_Click(object sender, EventArgs e) {
			AboutBox1 ab = new AboutBox1();
			ab.Show();
			ab.TopMost = true;
			ab.FormClosed += new FormClosedEventHandler(ab_FormClosed);
		}

		void ab_FormClosed(object sender, FormClosedEventArgs e) {
			this.button2.Focus();
		}

		private string GetAuthorsInfo(string FileName) {
			try {
				string result;
				StreamReader file = new System.IO.StreamReader(FileName, Encoding.GetEncoding(1251));
				result = file.ReadToEnd();
				file.Close();
				return result;

			}
			catch {
				return "";
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			timer1.Enabled = false;
			timer1.Dispose();
			timer1_Tick(null, null);
		}

		private void оСоздателяхToolStripMenuItem_Click(object sender, EventArgs e) {
			this.button1_Click(null, null);
		}
	}
}