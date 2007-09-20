using System;
using System.Collections.Generic;
using System.Text;
using Thecentury.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Diagnostics;

namespace Thecentury.Animations {

	public enum AutoAnimationType {
		Idle,
		Timer
	}

	public delegate void AnimationDelegate();

	public abstract class GeneralAnimation : ITimeControl, IDisposable {
		protected AutoAnimationType aaType = AutoAnimationType.Idle;
		public AutoAnimationType AutoAnimationType {
			get { return aaType; }
			set {
				aaType = value;
				AutoAnimated = autoAnimated;
			}
		}

		protected object o = null;
		public virtual object AppliedToObject { get { return o; } set { o = value; } }

		protected Thecentury.TimeControl.GeneralTimer timer = new Thecentury.TimeControl.EndlessTimer(false);
		public Thecentury.TimeControl.GeneralTimer InnerTimer {
			get { return timer; }
			set {
				timer = value;
				if (autoAnimated) {
					timer.Start();
				}
			}
		}

		/// <summary>
		/// Событие возбуждается, когда анимация стартует
		/// </summary>
		public event AnimationDelegate Started = null;
		/// <summary>
		/// Событие возбуждается, когда анимация завершается
		/// </summary>
		public event AnimationDelegate Finished = null;
		/// <summary>
		/// Событие возбуждается, когда анимация в очередной раз применяется
		/// </summary>
		public event AnimationDelegate Occured = null;

		protected void OnStarted() {
			if (!onStartedSent && Started != null) Started();
		}
		protected void OnFinished() {

			if (Finished != null) Finished();
		}
		protected void OnOccured() {
			if (Occured != null) Occured();
		}

		protected Timer autoanimationTimer = new Timer();
		/// <summary>
		/// Таймер, по которому происходит автоанимация анимации
		/// </summary>
		public Timer AutoAnimationTimer {
			get { return autoanimationTimer; }
			set { autoanimationTimer = value; }
		}

		protected bool autoAnimated = false;
		public bool AutoAnimated {
			get { return autoAnimated; }
			set {
				autoAnimated = value;
				if (value) {
					AutoAnimatedApply();
				}
			}
		}

		protected void AutoAnimatedApply() {
			timer.Start();
			if (aaType == AutoAnimationType.Idle) {
				autoanimationTimer.Stop();
				autoanimationTimer.Tick -= new EventHandler(Apply);
				Application.Idle += new EventHandler(Apply);
			}
			else { // timer
				Application.Idle -= new EventHandler(Apply);
				autoanimationTimer.Tick += new EventHandler(Apply);
				autoanimationTimer.Interval = 33;
				autoanimationTimer.Start();
			}
		}

		public void AutoAnimate() {
			AutoAnimated = true;
		}

		protected bool onStartedSent = false;

		protected void Apply(object sender, EventArgs e) {
			float t = timer.time;
			if (HasFinished(t)) {
				OnFinished();
				Dispose();
			}
			else {
				if (!onStartedSent) {
					OnStarted();
					onStartedSent = true;
				}
				Apply(timer.time);
				OnOccured();
			}
		}

		public GeneralAnimation() { }
		public GeneralAnimation(object o) { this.o = o; }

		public abstract bool HasBegun(float currentTime);
		public abstract bool IsWorking(float currentTime);
		public abstract bool HasFinished(float currentTime);
		public abstract void Restart(float startTime);
		public abstract void Apply(float currentTime);
		public abstract float EndTime { get; }

		#region ITimeControl Members

		public State State {
			get { return InnerTimer.State; }
			set { InnerTimer.State = value; }
		}

		public AnimationDirection Direction {
			get { return InnerTimer.Direction; }
			set { InnerTimer.Direction = value; }
		}

		public float Speed {
			get { return InnerTimer.Speed; }
			set { InnerTimer.Speed = value; }
		}

		public void Start() {
			InnerTimer.Start();
		}

		public void Pause() {
			InnerTimer.Pause();
		}

		public void Stop() {
			InnerTimer.Stop();
		}

		public void Reverse() {
			InnerTimer.Pause();
		}

		#endregion

		#region IDisposable Members

		public void Dispose() {
			Application.Idle -= new EventHandler(Apply);
			autoanimationTimer.Tick -= new EventHandler(Apply);
			autoanimationTimer.Stop();
			Started = null;
			Finished = null;
			Occured = null;
			autoanimationTimer.Dispose();
		}

		#endregion
	}
}
