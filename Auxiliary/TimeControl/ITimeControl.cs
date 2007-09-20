using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury {
	public interface ITimeControl {
		State State { get; set; }
		AnimationDirection Direction { get; set; }
		float Speed { get; set; }

		void Start();
		void Pause();
		void Stop();
		void Reverse();
	}

	public enum State {
		Playing,
		Paused,
		Stopped,
		Different
	}

	public enum AnimationDirection {
		Forward,
		Backward,
		Different
	}
}
