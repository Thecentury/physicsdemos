using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.TimeControl {
	public class EndlessTimer : GeneralTimer {

		/// <summary>
		/// Запускается автоматически
		/// </summary>
		public EndlessTimer() : this(Environment.TickCount, true) { }

		/// <summary>
		/// 
		/// </summary>
		/// <param name="autoStart">If true then starts immediately; if false - doesn't start immediately and needs to be started manually</param>
		public EndlessTimer(bool autoStart) : this(Environment.TickCount, autoStart) { }

		public EndlessTimer(float startTime) : this(true) { }

		// todo: решить проблему с неплавным вычисление времени при раскомментированной строке в конструкторе
		public EndlessTimer(float startTime, bool autoStart) {
			if (autoStart) {
				this.Start();
			}
			//this.startTime = (int)(startTime / speed);
		}
	}
}
