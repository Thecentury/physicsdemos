using System;
using System.Collections.Generic;
using System.Text;

using Thecentury;
using Thecentury.Attributes;

namespace SpectrumOfSignalWithPeriod {
	[Title("������ ��������������\n�������")]
	[Description("��������������� ����������� ������� �������������� ������� � ��� ������ � �������������\n����������� ������� � �������� ��� �������� � ����������� ��������")]
	public class Demonstration : GeneralDemonstration {
		public Demonstration() : base(new SpectrumOfSignalWithPeriod.UserControl1()) { }
	}
}
