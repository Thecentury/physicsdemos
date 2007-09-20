using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {

	/// <summary>
	/// Информация о непосредственных авторах демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class AuthorsAttribute : GeneralCreatorsAttribute {
		/// <summary>
		/// Создает экземпляр класса AuthorsAttribute.
		/// </summary>
		/// <param name="authors">Список авторов демонстрации</param>
		public AuthorsAttribute(params string[] authors) : base(authors) { }
		/// <summary>
		/// Создает экземпляр класса AuthorsAttribute.
		/// </summary>
		/// <param name="date">Дата создания демонстрации</param>
		/// <param name="authors">Список авторов демонстрации</param>
		public AuthorsAttribute(DateTime date, params string[] authors) : base(date, authors) { }
	}
}
