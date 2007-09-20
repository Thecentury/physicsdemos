using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Animations {
	public class NullAnimation : GeneralAnimation {
		public override bool HasFinished(float currentTime) { return true; }

		public override void Restart(float startTime) { }

		public override void Apply(float currentTime) { }

		public override float EndTime {
			get { return 0; }
		}

		public override bool HasBegun(float currentTime) {
			return true;
		}

		public override bool IsWorking(float currentTime) {
			return false;
		}
	}
}
