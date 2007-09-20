using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {
	public delegate float function_t (float t);

	public class GrowingParametricFunction : GParametricFunction, ITimeControl{
		protected TimeControl.EndlessTimer timer = new Thecentury.TimeControl.EndlessTimer();
		protected function_p x;
		protected function_p y;
		protected function_t start;
		protected function_t end;


		public GrowingParametricFunction(function_p x, function_p y, function_t start, function_t end) {
			this.x = x;
			this.y = y;
			this.start = start;
			this.end = end;
		}

		public override Rectangle To {
			get { return to; }
			set {
				to = value;
				float t = timer.time;
				this.startT = start(t);
				this.endT = end(t);
				points = LoadPoints(to, delegate(float p) { return x(p); }, delegate(float p) { return y(p); });
			}
		}

		public override bool IsDynamic {
			get { return true; }
		}

		public override void Paint(Graphics g) {
			GeneralPaint(g, points);
		}

		#region ITimeControl Members

		public State State {
			get { return timer.State; }
			set { timer.State = value; }
		}

		public float Speed {
			get { return timer.Speed; }
			set { timer.Speed = value; }
		}

		public AnimationDirection Direction {
			get { return timer.Direction; }
			set { timer.Direction = value; }
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
