using System;
using System.Drawing;
using Thecentury.Animations;
using System.Collections.Generic;
using Thecentury.TimeControl;

namespace Thecentury.Functions {
	public abstract class PraFunction {
		[Obsolete]
		protected List<GeneralAnimation> animations = new List<GeneralAnimation>();
		[Obsolete]
		protected GeneralTimer animTimer = new EndlessTimer();

		// todo удалить!
		[Obsolete]
		public void ApplyAnimations() {
			float t = animTimer.time;
			foreach (GeneralAnimation animation in animations) {
				if (animation.IsWorking(t)) {
					animation.Apply(t);
				}
			}
		}

		[Obsolete]
		public virtual bool IsDynamic {
			get {
				return repaint;
			}
		}

		protected Rectangle to;
		public virtual Rectangle To {
			get { return to; }
			set {
				if (to != value) {
					to = value;
					OnRecomputate();
				}
			}
		}

		protected RectangleF from;
		public virtual RectangleF From {
			get { return from; }
			set { from = value; repaint = true; }
		}

		protected bool causesUpdate = true;
		public bool CausesUpdate {
			get { return causesUpdate; }
			set { causesUpdate = value; }
		}

		public void StopUpdating() {
			causesUpdate = false;
		}

		public void PerformUpdate() {
			causesUpdate = true;
			OnRepaint();
		}

		public void ContinueUpdating() {
			causesUpdate = true;
		}

		public event EventHandler RepaintNeeded;
		protected virtual void OnRepaint() {
			if (RepaintNeeded != null) {
				RepaintNeeded(this, null);
			}
		}

		public event EventHandler RecomputateNeeded;
		protected virtual void OnRecomputate() {
			if (RecomputateNeeded != null) {
				RecomputateNeeded(this, null);
			}
		}

		// todo так было - убрать совсем
		//public virtual bool IsDynamic {
		//    get {
		//        if (repaint) {
		//            repaint = false;
		//            return true;
		//        }
		//        return this.IsAnimated;
		//    }
		//}

		public abstract void Paint(Graphics g);

		protected string name = "";
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		private bool _repaint = true;
		protected bool repaint {
			get {
				if (_repaint) {
					_repaint = false;
					return true;
				}
				return false;
			}
			set {
				if (value) {
					_repaint = true;
				}
			}
		}

		public void ForceRepaint() {
			// todo может, лучше просто вызвать перерисование?
			repaint = true;
		}

		private bool _recomputate = true;
		protected bool recomputate {
			get {
				// todo
				throw new NotImplementedException();
			}
			set {
				if (value) {
					_recomputate = true;
				}
			}
		}

		public abstract void Recomputate(RectangleF where);

		public void ForceRecomputate() {
			OnRecomputate();
		}

		#region IComparable Members

		public int CompareTo(object obj) {
			PraFunction f = obj as PraFunction;
			return this.name.CompareTo(f.Name);
		}

		#endregion
	}
}
