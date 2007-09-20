using System;
using System.Collections.Generic;
using System.Text;
using Thecentury;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace PhysicsDemonstrations {
	public sealed class NDemoList {
		private List<Thecentury.GeneralDemonstration> demonstrations;
		public List<Thecentury.GeneralDemonstration> Demonstrations {
			get {
				return demonstrations;
			}
		}

		private List<DemonstrationInfo> controls = new List<DemonstrationInfo>();
		public List<DemonstrationInfo> Controls {
			get {
				return controls;
			}
		}

		public event EventHandler RunDemo;
		private void OnRunDemo(Thecentury.GeneralDemonstration demo) {
			if (RunDemo != null) {
				RunDemo(demo, null);
			}
		}

		private string demosDirectoryName = Environment.CurrentDirectory + @"\Demonstrations";
		private string demoExt = ".dll";

		public bool IsEmpty {
			get { return controls.Count == 0; }
		}

		private int currentY = 100;
		private int currentX = 25;
		public int YStep = 90;

		public NDemoList() {
			LoadDemonstrations();
		}

		public void LoadDemonstrations() {
			demonstrations = new List<GeneralDemonstration>();
			DirectoryInfo di = new DirectoryInfo(demosDirectoryName);
			FileInfo[] fis = di.GetFiles("*" + demoExt);
			if (fis.Length == 0) {
				System.Windows.Forms.MessageBox.Show("В папке " + demosDirectoryName + " файлов с расширением " + demoExt + " не найдено!");
			}
			else {
				foreach (FileInfo fi in fis) {
					try {
						Assembly a = Assembly.LoadFrom(fi.FullName);
						Type[] types = a.GetTypes();
						foreach (Type t in types) {
							if (typeof(Thecentury.GeneralDemonstration).IsAssignableFrom(t)) {
								object demo = null;
								try {
									demo = Activator.CreateInstance(t);
								}
								catch (Exception e) {
									MessageBox.Show("Ошибка при загрузке демонстрации из файла " + fi.Name + ":\n" + e.Message);
									continue;
								}
								Thecentury.GeneralDemonstration currentDemo = demo as Thecentury.GeneralDemonstration;
								demonstrations.Add(currentDemo);

								object[] attrs = t.GetCustomAttributes(typeof(Thecentury.Attributes.TitleAttribute), false);
								string name = "Название";
								if (attrs.Length != 0) {
									name = (attrs[0] as Thecentury.Attributes.TitleAttribute).Title;
								}

								attrs = t.GetCustomAttributes(typeof(Thecentury.Attributes.DescriptionAttribute), false);
								string description = "Описание";
								if (attrs.Length != 0) {
									description = (attrs[0] as Thecentury.Attributes.DescriptionAttribute).Description;
								}

								DemonstrationInfo demoInfo = new DemonstrationInfo(name, description, currentX, currentY);
								demoInfo.RunDemo += new EventHandler(demoInfo_RunDemo);
								controls.Add(demoInfo);
								currentY += 90;
							}
						}
					}
					catch (Exception e) {
						System.Windows.Forms.MessageBox.Show("Ошибка при загрузке демонстрации из файла " + fi.Name + ":\n" + e.Message);
					}
				}
			}
		}

		void demoInfo_RunDemo(object sender, EventArgs e) {
			DemonstrationInfo demoInfo = sender as DemonstrationInfo;
			int index = controls.IndexOf(demoInfo);
			OnRunDemo(demonstrations[index]);
		}

		public void DisableControls() {
			foreach (DemonstrationInfo demoInfo in controls) {
				demoInfo.Enabled = false;
				demoInfo.Visible = false;
			}
		}

		public void EnableControls() {
			foreach (DemonstrationInfo demoInfo in controls) {
				demoInfo.Enabled = true;
				demoInfo.Visible = true;
			}
		}
	}
}
