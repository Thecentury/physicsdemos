using System;
using System.Collections.Generic;
using System.Text;
using Thecentury;
using Thecentury.Attributes;

namespace FormingOfSignalFromSpecter {
	[Title("‘ормирование сигнала\nиз спектра. ќсцилл€ции\n√иббса")]
	[Description("ƒемонстраци€ иллиюстрирует формирование сигнала из гармоник и возникновение \nпогрешностей при ограничении спектральной полосы сигнала")]
	public class Demonstration : GeneralDemonstration {
		public Demonstration() : base(new FormingOfSignalFromSpecter.UserControl1()) { }
	}
}
