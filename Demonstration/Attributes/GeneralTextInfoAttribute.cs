using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {
	/// <summary>
	/// Базовый класс для аттрибутов, хранящих информацию о создателях и дате создания
	/// </summary>
	public abstract class GeneralTextInfoAttribute : Attribute {
		protected string info;
		///// <summary>
		///// Текстовая информация
		///// </summary>
		//public string Info {
		//    get { return info; }
		//    set { info = value; }
		//}

		/// <summary>
		/// Создает новый экземпляр класса GeneralTextInfoAttribute.
		/// </summary>
		/// <param name="info">Текстовая информация, добавляемая в экземпляр класса</param>
		public GeneralTextInfoAttribute(string info) {
			this.info = info;
		}
	}
}
