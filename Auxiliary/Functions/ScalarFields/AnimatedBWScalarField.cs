using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Functions.ScalarFields {
	public class AnimatedBWScalarField : GScalarField, ITimeControl {
		protected TimeControl.GeneralTimer timer = new Thecentury.TimeControl.EndlessTimer();
		public TimeControl.GeneralTimer Timer {
			get { return timer; }
			set { timer = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="f">ijt->[0,1]</param>
		public AnimatedBWScalarField(formula f) { this.f = f; }

		public override System.Drawing.Rectangle To {
			get { return to; }
			set {
				to = value;
				float t = timer.time;
				UnsafeBWLoadBMP(delegate(Vector v) { return f( new Vector(v[0], v[1], t)); });
			}
		}

		public override bool IsDynamic {
			get { return true; }
		}

		#region ITimeControl Members

		public State State {
			get { return timer.State; }
			set { timer.State = value; }
		}

		public AnimationDirection Direction {
			get { return timer.Direction; }
			set { timer.Direction = value; }
		}

		public float Speed {
			get { return timer.Speed; }
			set { timer.Speed = value; }
		}

		public void Start() {
			timer.Start();
		}

		public void Pause() {
			timer.Pause();
		}

		public void Stop() {
			timer.Stop();
		}

		public void Reverse() {
			timer.Reverse();
		}

		#endregion
	}
}
