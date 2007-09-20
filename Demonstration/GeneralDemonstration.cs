using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Thecentury {
	/// <summary>
	/// Базовый класс для всех демонстраций
	/// </summary>
	public abstract class GeneralDemonstration {
		/// <summary>
		/// Событие, возникающее когда демонстрация изменяет свой главный (видимый) контрол.
		/// </summary>
		public event EventHandler MainControlChanged;
		/// <summary>
		/// Поднимает событие изменения видимого контрола
		/// </summary>
		protected void OnMainControlChanged() {
			if (MainControlChanged != null) {
				MainControlChanged(this, null);
			}
		}

		/// <summary>
		/// Видимый в данный момент контрол.
		/// <remarks>Не рекомендуется изменять это поле в производных классах, лучше изменять свойство MainControl - при это сработает событие такого изменения</remarks>
		/// </summary>
		protected UserControl mainControl;
		/// <summary>
		/// Графическое представление демонстрации
		/// </summary>
		public virtual UserControl MainControl {
			get {
				return this.mainControl;
			}
			protected set {
				this.mainControl = value;
				OnMainControlChanged();
			}
		}

		/// <summary>
		/// Создает экземпляр класса GeneralDemonstration
		/// </summary>
		/// <param name="mainControl">Графическое представление демонстрации</param>
		public GeneralDemonstration(UserControl mainControl) {
			this.mainControl = mainControl;
		}
	}
}
