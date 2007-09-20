using System;
using System.Collections.Generic;
using System.Text;
using Thecentury.Functions;

namespace Thecentury.TimeControl {
	public abstract class GeneralTimer : ITimeControl{
		protected int startTime;
		protected int coeff = 0;
		protected int prevCoeff = 0;
		protected State state = State.Stopped;
		protected bool hasStarted = false;
		protected int pauseStartTime = 0;
		protected bool isPaused = false;
		protected float speed = 0.001f;

		public State State {
			get {
				return state;
			}
			set {
				switch (value) {
					case State.Playing:
						Start();
						break;
					case State.Paused:
						if (state == State.Playing) {
							Pause();
						}
						break;
					case State.Stopped:
						Stop();
						break;
				}
			}
		}

		public virtual float time {
			get {
				return speed * (coeff * (Environment.TickCount - startTime) + pauseStartTime);
			}
		}

		public void Start() {
			if (state == State.Stopped) {
				startTime = Environment.TickCount;
			}

			if (!hasStarted) {
				state = State.Playing;
				if (isPaused) {
					Pause(); // resuming
				}
				else {
					hasStarted = true;
					coeff = 1;
					startTime = Environment.TickCount;
				}
			}
		}

		/// <summary>
		/// If you want to pause animation, call this method; if you want to resume animation, call this method again or call Play()
		/// </summary>
		public void Pause() {
			if (hasStarted) {
				if (!isPaused) { // was playing
					state = State.Paused;
					isPaused = true;
					prevCoeff = coeff;
					pauseStartTime = coeff * (Environment.TickCount - startTime);
					coeff = 0;
				}
				else { // was paused
					isPaused = false;
					state = State.Playing;
					coeff = prevCoeff;
					if (prevCoeff == 1) {
						startTime = Environment.TickCount - pauseStartTime;
					}
					else {
						startTime = Environment.TickCount + pauseStartTime;
					}
					pauseStartTime = 0;
				}
			}
		}

		/// <summary>
		/// Stops time flow and sets time to 0.
		/// </summary>
		public void Stop() {
			coeff = 0;
			isPaused = false;
			hasStarted = false;
			state = State.Stopped;
		}

		/// <summary>
		/// Reverses direction of time flow; should be called only if time is not stopped or paused.
		/// </summary>
		public void Reverse() {
			if (!isPaused) {
				if (coeff == -1) {
					int t = Environment.TickCount;
					startTime = t - (startTime - t);
					coeff = 1;
				}
				else {
					int t = Environment.TickCount;
					startTime = t + (t - startTime);
					coeff = -1;
				}
			}
		}

		/// <summary>
		/// If value is positive, time is increasing; in opposite case time is decreasing
		/// </summary>
		public AnimationDirection Direction {
			get {
				if (coeff > 0) {
					return AnimationDirection.Forward;
				}
				else if (coeff == 0) {
					return AnimationDirection.Different;
				}
				else return AnimationDirection.Backward;
			}
			set {
				if (value != AnimationDirection.Different) {
					if (coeff > 0 && value == AnimationDirection.Backward) {
						Reverse();
					}
					else if (coeff < 0 && value == AnimationDirection.Forward) {
						Reverse();
					}
				}
			}
		}

		public float Speed {
			get { return speed; }
			set { speed = value; }
		}

	}
}
