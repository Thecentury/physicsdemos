using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {

	/// <summary>
	/// Контакты авторов демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=false, Inherited=false)]
	public class AuthorsContactsAttribute : GeneralContactsAttribute {
		/// <summary>
		/// Создает экземпляр класса AuthorsContactsAttribute
		/// </summary>
		public AuthorsContactsAttribute() { }
	}
}
