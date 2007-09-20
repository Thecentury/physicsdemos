using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace Thecentury.Utils {
	/// <summary>
	/// Эта структура позволяет подсчитать скорость выполнения кода одним из
	/// наиболее точным способов. Фактически вычисления производятся в тактах
	/// процессора, а потом переводятся в секунды.
	/// </summary>
	public struct PerfCounter : IDisposable {
		Int64 start;
		Int64 freq;
		Int64 finish;
		string message;

		/// <summary>
		/// Начинает подсчет времени выполнения.
		/// </summary>
		public void Start() {
			start = 0;
			freq = 0;
			QueryPerformanceFrequency(ref freq);
			QueryPerformanceCounter(ref start);
		}

		public PerfCounter(string message)
			: this(true) {
			this.message = message;
		}

		public PerfCounter(bool autostart) {
			start = 0;
			freq = 0;
			finish = 0;
			message = "";
			if (autostart) {
				Start();
			}
		}

		/// <summary>
		/// Время в секундах
		/// </summary>
		public float TimeF {
			get {
				finish = 0;
				QueryPerformanceCounter(ref finish);
				return (((float)(finish - start) / (float)freq));
			}
		}

		public double Time {
			get {
				finish = 0;
				QueryPerformanceCounter(ref finish);
				return (((double)(finish - start) / (double)freq));
			}
		}

		public int Time_mSecs {
			get {
				finish = 0;
				QueryPerformanceCounter(ref finish);

				return (int)((1000L * (finish - start)) / freq);
			}
		}

		public int Time_nSecs {
			get {
				finish = 0;
				QueryPerformanceCounter(ref finish);

				return (int)((1000000L * (finish - start)) / freq);
			}
		}


		#region IDisposable Members

		public void Dispose() {
			Debug.WriteLine(message + Time_nSecs.ToString());
		}

		#endregion

		[DllImport("Kernel32.dll")]
		static extern bool QueryPerformanceCounter(ref Int64 performanceCount);

		[DllImport("Kernel32.dll")]
		static extern bool QueryPerformanceFrequency(ref Int64 frequency);
	}
}