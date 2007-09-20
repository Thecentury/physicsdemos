using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.TimeControl {
	public class DiscreteTimer : GeneralTimer{
		protected float interval = 1000;
		public float Interval { get { return interval / speed; } set { interval = value / speed; } }

		public DiscreteTimer(float interval) : base() {
			this.interval = interval / speed;
		}

		public override float time {
			get {
				float t = base.time;
				return (float)Math.Floor(t / interval);
			}
		}
	}
}
