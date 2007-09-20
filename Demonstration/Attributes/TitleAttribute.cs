using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {
	/// <summary>
	/// Название демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public class TitleAttribute : GeneralTextInfoAttribute {
		public TitleAttribute(string title) : base(title) { }
		/// <summary>
		/// Название
		/// </summary>
		public string Title {
			get { return this.info; }
		}
	}
}
