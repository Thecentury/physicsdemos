using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {

	/// <summary>
	/// Контакты модификаторов демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public class ModifiersContactsAttribute : GeneralContactsAttribute {
		/// <summary>
		/// Создает экземпляр класса ModifierssContactsAttribute
		/// </summary>
		public ModifiersContactsAttribute() { }
	}
}
