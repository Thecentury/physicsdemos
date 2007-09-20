using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Thecentury.Functions {
	// todo переделать параметрические функции для соответствия типу formula
	public delegate float function_pt(float p, float t);

	public class AnimatedParametricFunction : GParametricFunction, ITimeControl {
		protected TimeControl.EndlessTimer timer = new Thecentury.TimeControl.EndlessTimer();
		protected function_pt x;
		protected function_pt y;

		public AnimatedParametricFunction(function_pt x, function_pt y) : this(x, y, 0, 1) { }

		public AnimatedParametricFunction(function_pt x, function_pt y, float startT, float endT) {
			this.x = x;
			this.y = y;
			this.startT = startT;
			this.endT = endT;
		}

		public override Rectangle To {
			get { return to; }
			set {
				to = value;
				float t = timer.time;
				points = LoadPoints(to, delegate(float p) { return x(p, t); }, delegate(float p) { return y(p, t); });
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
