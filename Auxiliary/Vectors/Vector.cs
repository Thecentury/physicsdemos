using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Thecentury.Diagnostics;
using System.Drawing;

namespace Thecentury {
	/// <summary>
	/// Вектор значений типа double.
	/// </summary>
	public struct Vector : IEnumerable {
		private List<double> elems;

		/// <summary>
		/// Список элементов вектора.
		/// </summary>
		public List<double> Elements {
			get { return elems ?? (elems = new List<double>()); }
			set { elems = value; }
		}

		/// <summary>
		/// Количество элементов в векторе.
		/// </summary>
		public int Length {
			get { return elems.Count; }
		}

		/// <summary>
		/// Индексатор.
		/// </summary>
		/// <param name="i">Индекс</param>
		/// <returns></returns>
		public double this[int i] {
			get { return elems[i]; }
			set { elems[i] = value; }
		}

		/// <summary>
		/// Конструктор вектора из массива значений.
		/// </summary>
		/// <param name="values">Массив или перечисление значений</param>
		public Vector(params double[] values) {
			elems = new List<double>();
			foreach (double d in values) {
				elems.Add(d);
			}
		}

		/// <summary>
		/// Конструктор вектора из перечислимого типа
		/// </summary>
		/// <param name="source">Источник элементов вектора</param>
		public Vector(IEnumerable source) {
			elems = new List<double>();
			foreach (double d in source) {
				elems.Add(d);
			}
		}

		/// <summary>
		/// Конструктор вектора из всех элементов другого вектора
		/// </summary>
		/// <param name="source">Вектор-источник элементов вектора</param>
		public Vector(Vector source) {
			this.elems = new List<double>(source.elems.ToArray());
		}

		/// <summary>
		/// Конструктор вектора из структуры PointF
		/// </summary>
		/// <param name="source">Структура PointF - источник элементов вектора</param>
		public Vector(PointF source) {
			elems = new List<double>();
			elems.Add(source.X);
			elems.Add(source.Y);
		}

		/// <summary>
		/// Конструктор вектора из структуры Color
		/// </summary>
		/// <param name="source">Структура Color - источник элементов вектора</param>
		/// <remarks>Создает вектор со следующими элементами в следующем порядке: RGBA</remarks>
		public Vector(Color source) {
			elems = new List<double>();
			elems.Add(source.R);
			elems.Add(source.G);
			elems.Add(source.B);
			elems.Add(source.A);
		}

		/// <summary>
		/// Добавляет элементы вектора v в конец вектора
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public void Append(Vector v) {
			foreach (double d in v) {
				elems.Add(d);
			}
		}

		/// <summary>
		/// Добавляет число в конец вектора
		/// </summary>
		/// <param name="d">Добавляемое число</param>
		public void Append(double d) {
			elems.Add(d);
		}

		/// <summary>
		/// Поле для удобства работы с Thecentury.Functions.Function и иже с ними.
		/// </summary>
		public double X {
			get { return elems[0]; }
			set { elems[0] = value; }
		}

		/// <summary>
		/// Поле для удобного создания Thecentury.Functions.AnimatedFunction и иже с ними.
		/// </summary>
		public double T {
			get { return elems[1]; }
			set { elems[1] = value; }
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="v"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public static Vector operator *(Vector v, double a) {
			Vector res = new Vector(v);
			for (int i = 0; i < v.Length; i++) {
				res.elems[i] *= a;
			}
			return res;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="a"></param>
		/// <param name="v"></param>
		/// <returns></returns>
		public static Vector operator *(double a, Vector v) {
			Vector res = new Vector(v);
			for (int i = 0; i < v.Length; i++) {
				res.elems[i] *= a;
			}
			return res;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="v"></param>
		/// <param name="a"></param>
		/// <returns></returns>
		public static Vector operator /(Vector v, double a) {
			Vector res = new Vector(v);
			for (int i = 0; i < v.Length; i++) {
				res.elems[i] /= a;
			}
			return res;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public static Vector operator +(Vector v1, Vector v2) {
			MyDebug.CheckCondition(v1.Length == v2.Length, "Длины векторов-слагаемых должны быть одинаковы!");

			Vector res = new Vector(v1);
			for (int i = 0; i < v1.Length; i++) {
				res.elems[i] += v2[i];
			}
			return res;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="v1"></param>
		/// <param name="v2"></param>
		/// <returns></returns>
		public static Vector operator -(Vector v1, Vector v2) {
			MyDebug.CheckCondition(v1.Length == v2.Length, "Длины векторов-слагаемых должны быть одинаковы!");

			Vector res = new Vector(v1);
			for (int i = 0; i < v1.Length; i++) {
				res.elems[i] -= v2[i];
			}
			return res;
		}

		/// <summary>
		/// Приведение первого элемента вектора к типу double, вектор должен быть не пустым для успешного выполнения этой операции.
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static explicit operator double(Vector v) {
			return v[0];
		}

		/// <summary>
		/// Приведение первого элемента вектора к типу float, вектор должен быть не пустым для успешного выполнения этой операции.
		/// </summary>
		/// <param name="v"></param>
		/// <returns></returns>
		public static explicit operator float(Vector v) {
			return (float)v[0];
		}

		/// <summary>
		/// Создание вектора из элемента типа double
		/// </summary>
		/// <param name="a">Элемент типа double, который станет единственным элементом вектора</param>
		/// <returns></returns>
		public static explicit operator Vector(double a) {
			return new Vector(a);
		}

		/// <summary>
		/// Преобразование вектора к массиву double
		/// </summary>
		/// <param name="v">Преобразуемый вектор</param>
		/// <returns>Массив элементов вектора</returns>
		public static implicit operator double[](Vector v) {
			return v.elems.ToArray();
		}

		/// <summary>
		/// Преобразование массива элементов типа double к вектору
		/// </summary>
		/// <param name="a">Преобразуемый массив</param>
		/// <returns>Вектор из элементов массива</returns>
		public static implicit operator Vector(double[] a) {
			return new Vector(a);
		}

		/// <summary>
		/// Возвращает true в случае, если вектор пустой, и false в противном случае.
		/// </summary>
		/// <returns></returns>
		public bool IsEmpty {
			get {
				return (elems == null) || elems.Count == 0;
			}
		}

		#region IEnumerable Members

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public IEnumerator GetEnumerator() {
			foreach (double d in elems) {
				yield return d;
			}
		}

		#endregion
	}
}
