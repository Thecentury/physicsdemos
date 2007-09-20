using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {

	[AttributeUsage(AttributeTargets.Class, AllowMultiple=true, Inherited=false)]
	public class ModifiersAttribute : GeneralCreatorsAttribute {
		public ModifiersAttribute(params string[] modifiers) : base(modifiers) { }
		public ModifiersAttribute(DateTime date, params string[] modifiers) : base(date, modifiers) { }
	}
}
