using System;
using System.Collections.Generic;
using System.Text;
using Thecentury;
using Thecentury.Attributes;

namespace FormingOfSignalFromSpecter {
	[Title("������������ �������\n�� �������. ����������\n������")]
	[Description("������������ ������������� ������������ ������� �� �������� � ������������� \n������������ ��� ����������� ������������ ������ �������")]
	public class Demonstration : GeneralDemonstration {
		public Demonstration() : base(new FormingOfSignalFromSpecter.UserControl1()) { }
	}
}
