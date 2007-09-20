using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {

	/// <summary>
	/// Замечание по поводу демонстрации,
	/// ее содержания,
	/// методики проведения лекции с использованием данной демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=false)]
	public class RemarkAttribute : GeneralTextInfoAttribute {
		/// <summary>
		/// Создает экземпляр класса RemarkAttribute
		/// </summary>
		/// <param name="remark">Замечания к демонстрации</param>
		public RemarkAttribute(string remark) : base(remark) { }
	}
}
