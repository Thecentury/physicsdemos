using System;
using System.Collections.Generic;
using System.Text;
using Thecentury;
using Thecentury.Attributes;

namespace AmplitydnoModylirovanSignal {
	[Title("����������-\n��������������\n������")]
	[Description("��������������� ����������� ������� ����������-��������������� ������� �� ��� ����������:\n������� � ������� ��������� � ������� �������")]
	public class Demonstration : GeneralDemonstration {
		public Demonstration() : base(new AmplitydnoModylirovanSignal.UserControl1()) { }
	}
}
