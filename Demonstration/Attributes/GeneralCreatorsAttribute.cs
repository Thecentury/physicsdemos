using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {
	/// <summary>
	/// Базовый класс для аттрибутов, хранящих информацию о создателях и дате создания
	/// </summary>
	public abstract class GeneralCreatorsAttribute : GeneralDemonstrationAttribute {
		/// <summary>
		/// Список создателей
		/// </summary>
		protected List<string> names = default(List<string>);
		public List<string> Names {
			get { return names; }
			set { names = value; }
		}

		/// <summary>
		/// Дата
		/// </summary>
		protected DateTime date = default(DateTime);
		public DateTime Date {
			get { return date; }
			set { date = value; }
		}

		/// <summary>
		/// Создает новый экземпляр класса GeneralCreatorsAttribute.
		/// </summary>
		/// <param name="creators">Создатели</param>
		public GeneralCreatorsAttribute(params string[] creators) : this(DateTime.Now, creators) { }

		/// <summary>
		/// Создает новый экземпляр класса GeneralCreatorsAttribute.
		/// </summary>
		/// <param name="date">Дата</param>
		/// <param name="creators">Создатели</param>
		public GeneralCreatorsAttribute(DateTime date, params string[] creators) {
			names = new List<string>();
			foreach (string creator in creators) {
				names.Add(creator);
			}
			this.date = date;
		}
	}
}
