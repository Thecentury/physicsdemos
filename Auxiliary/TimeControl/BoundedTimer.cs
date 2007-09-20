using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.TimeControl {
	public class BoundedTimer : GeneralTimer {
		protected float duration = 3;
		/// <summary>
		/// Duration of full cycle of oscillation
		/// </summary>
		public float Duration { get { return duration; } set { duration = value; } }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="duration">Duration of full cycle of oscillation</param>
		public BoundedTimer(float duration) {
			this.duration = duration;
		}

		public override float time {
			get {
				float t = base.time;
				t = MyMath.Remainder(t, duration);

				if (t < duration / 2) { return t; }
				else { return duration - t; }
			}
		}
	}
}
