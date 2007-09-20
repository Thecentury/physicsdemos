using System;
using System.Collections.Generic;
using System.Text;
using Thecentury.Diagnostics;
using System.Drawing;
using System.Reflection;
using System.Diagnostics;
using Thecentury.Functions;

namespace Thecentury.Animations {
	public class Animation : GeneralAnimation {

		public Animation(string parameterName, interpolate changer, float duration, bool renew, object[] steps, params float[] weights)
			: this(parameterName, changer, 0, duration, renew, steps, weights) { }

		public Animation(string parameterName, interpolate changer, float duration, bool renew, object[] steps)
			: this(parameterName, changer, 0, duration, renew, steps, GenerateWeights(steps.Length)) { }

		public Animation(string parameterName, interpolate changer, float startTime, float duration, bool renew, params object[] steps)
			: this(parameterName, changer, startTime, duration, renew, steps, GenerateWeights(steps.Length)) { }

		public Animation(string parameterName, interpolate changer, float startTime, float duration, bool renew, object[] steps, params float[] weights) {
			Initialize(parameterName, changer, startTime, duration, renew, steps, weights);
		}

		public Animation(object o, string parameterName, interpolate changer, float startTime, float duration, bool renew, params object[] steps)
			: this(o, parameterName, changer, startTime, duration, renew, steps, GenerateWeights(steps.Length)) { }

		public Animation(object o, string parameterName, interpolate changer, float startTime, float duration, bool renew, object[] steps, params float[] weights) {
			Initialize(parameterName, changer, startTime, duration, renew, steps, weights);
			Load(o, parameterName);
		}

		public override object AppliedToObject {
			get {
				return base.AppliedToObject;
			}
			set {
				base.AppliedToObject = value;
				Load(o, parameterName);
			}
		}

		protected void Initialize(string parameterName, interpolate changer, float startTime, float duration, bool renew, object[] steps, params float[] weights) {
			Thecentury.Diagnostics.MyDebug.CheckCondition(steps.Length > 1);
			Thecentury.Diagnostics.MyDebug.CheckCondition(parameterName != "");

			this.parameterName = parameterName;
			this.steps = steps;
			this.weights = new float[steps.Length];
			this.interpolator = changer;
			this.startTime = startTime;
			this.duration = duration;
			this.renew = renew;

			float sum = 0;
			for (int i = 0; i < weights.Length; i++) {
				sum += weights[i];
			}

			float coeff = sumWeight / sum;
			for (int i = 0; i < weights.Length; i++) {
				this.weights[i] = weights[i] * coeff;
			}
			Finished += new AnimationDelegate(Animation_Finished);
		}

		void Animation_Finished() {
			UncheckedApply(EndTime - duration * 0.0001f);
		}

		public override float EndTime { get { return startTime + duration; } }

		public bool IsEndless { get { return renew; } }

		public override bool HasBegun(float currentTime) {
			return currentTime >= startTime;
		}

		public override bool IsWorking(float currentTime) {
			return HasBegun(currentTime) && !HasFinished(currentTime);
		}

		public override bool HasFinished(float currentTime) { return (!renew) && (currentTime >= EndTime); }

		public override void Restart(float startTime) {
			this.startTime = startTime;
			onStartedSent = false;
		}

		protected static float[] GenerateWeights(int length) {
			float[] res = new float[length];
			float weight = sumWeight / length;
			for (int i = 0; i < length; i++) {
				res[i] = weight;
			}
			return res;
		}

		protected bool ProperTime(float currentTime) {
			if (currentTime < startTime) {
				return false;
			}
			if (!renew && (currentTime > startTime + duration)) {
				OnFinished();
				return false;
			}

			OnStarted();
			onStartedSent = false;
			OnOccured();
			return true;
		}

		protected void Load(object o, string parameterName) {
			this.o = o;
			Type type = o.GetType();
			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo property in properties) {
				if (property.Name == parameterName) {
					usesField = false;
					this.propertyInfo = property;
					return;
				}
			}

			FieldInfo[] fields = type.GetFields();
			foreach (FieldInfo field in fields) {
				if (field.Name == parameterName) {
					usesField = true;
					this.fieldInfo = field;
					return;
				}
			}
			throw new Exception("Field or property \"" + parameterName + "\" was not found!");
		}

		public override void Apply(float currentTime) {
			if (!ProperTime(currentTime)) { return; }
			UncheckedApply(currentTime);
		}

		protected void UncheckedApply(float currentTime) {
			float ratio = MyMath.Remainder(currentTime - startTime, duration) / duration; // [0,1]
			
			float newRatio;
			int number = FindTwoObjects(ratio, out newRatio);

			object value = Interpolate(steps[number], steps[number + 1], interpolator(newRatio));

			SetValue(value);
		}

		protected void SetValue(object value) {
			if (usesField) {
				fieldInfo.SetValue(o, value);
			}
			else {
				propertyInfo.SetValue(o, value, null);
			}
		}

		protected int FindTwoObjects(float ratio, out float newRatio) {
			ratio *= sumWeight;
			float a = 0;
			float step;
			for (int i = 0; i < steps.Length - 1; i++) {
				step = weights[i] + weights[i + 1];
				a += step;
				if (ratio < a) {
					newRatio = 1 + (ratio - a) / step;
					return i;
				}
			}
			newRatio = 1;
			return steps.Length - 2;
		}

		protected object Interpolate(object from, object to, float ratio) {
			if (from is Color) {
				return Interpolation.Interpolate((Color)from, (Color)to, ratio);
			}
			else if (from is float) {
				return ((float)from) * (1 - ratio) + ((float)to) * ratio;
			}
			else if (from is int) {
				return (int)(((int)from) * (1 - ratio) + ((int)to) * ratio);
			}
			else if (from is formula) {
				return new formula(delegate(Vector v) {
					Vector v2 = (to as formula)(v);
					Vector v1 = (from as formula)(v);
					return v1 * (1 - ratio) + v2 * ratio;
				});
			}
			else if (from is PointF) {
				return Interpolation.Interpolate((PointF)from, (PointF)to, ratio);
			}
			else if (from is Point) {
				return Interpolation.Interpolate((Point)from, (Point)to, ratio);
			}
			else if (from is Size) {
				return Interpolation.Interpolate((Size)from, (Size)to, ratio);
			}
			else if (from is SizeF) {
				return Interpolation.Interpolate((SizeF)from, (SizeF)to, ratio);
			}
			else if (from is Rectangle) {
				return Interpolation.Interpolate((Rectangle)from, (Rectangle)to, ratio);
			}
			else if (from is RectangleF) {
				return Interpolation.Interpolate((RectangleF)from, (RectangleF)to, ratio);
			}
			else {
				return null;
			}
		}

		protected object[] steps = null;
		protected float[] weights = null;

		protected bool usesField;
		protected FieldInfo fieldInfo;
		protected PropertyInfo propertyInfo;

		protected float startTime = 0;
		protected float duration;
		protected interpolate interpolator;
		protected bool renew = false;
		protected const float sumWeight = 100;
		protected string parameterName;
	}
}
