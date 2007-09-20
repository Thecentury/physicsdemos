using System;
using System.Collections.Generic;
using System.Text;


namespace Thecentury.Functions {
	public abstract class GTimeDependentFunction : GFormulaFunction, ITimeControl {
		protected TimeControl.GeneralTimer timer = new Thecentury.TimeControl.EndlessTimer();
		public TimeControl.GeneralTimer Timer {
			get { return timer; }
			set { timer = value; repaint = true; }
		}

		protected float time {
			get { return timer.time; }
		}

		public override bool IsDynamic {
			get { return true; }
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
