using System;
using System.Collections.Generic;
using System.Text;
using Thecentury;
using Thecentury.Attributes;

namespace AmplitydnoModylirovanSignal {
	[Title("јмплитудно-\nмодулированный\nсигнал")]
	[Description("ƒемонстрируетс€ зависимость спектра амплитудно-модулированного сигнала от его параметров:\nглубины и частоты модул€ции и несущей частоты")]
	public class Demonstration : GeneralDemonstration {
		public Demonstration() : base(new AmplitydnoModylirovanSignal.UserControl1()) { }
	}
}
