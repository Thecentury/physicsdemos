using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {
	/// <summary>
	/// Описание демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class DescriptionAttribute : GeneralTextInfoAttribute {
		/// <summary>
		/// Создает экземпляр класса DescriptionAttribute.
		/// </summary>
		/// <param name="description">Описание демонстрации</param>
		public DescriptionAttribute(string description) : base(description) { }

		public string Description {
			get { return this.info; }
		}
	}
}
