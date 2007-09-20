using System;
using System.Collections.Generic;
using System.Text;

using Thecentury;
using Thecentury.Attributes;

namespace SpectrumOfSignalWithPeriod {
	[Title("Спектр периодического\nсигнала")]
	[Description("Демонстрируется взаимосвязь спектра периодического сигнала с его формой и трансформация\nдискретного спектра в сплошной при переходе к уединенному импульсу")]
	public class Demonstration : GeneralDemonstration {
		public Demonstration() : base(new SpectrumOfSignalWithPeriod.UserControl1()) { }
	}
}
