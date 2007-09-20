using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {

	/// <summary>
	/// Базовый класс для аттрибутов демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public abstract class GeneralDemonstrationAttribute : Attribute {
		bool visible = false;

		/// <summary>
		/// Если имеет значение TRUE, то аттрибут виден в просмотровщике демонстраций,
		/// иначе - не виден
		/// </summary>
		public bool Visible {
			get { return visible; }
			set { visible = value; }
		}
	}
}
