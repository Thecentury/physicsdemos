using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Thecentury.ColorFunctions;

namespace Thecentury.ColorFunctions {
	public static class RandomColor {
		/// <summary>
		/// Generates random Color, using built-in class Random
		/// </summary>
		/// <returns>Randomly generated Color</returns>
		public static Color Generate() {
			Random rnd = new Random();
			return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
		}

		/// <summary>
		/// Generates random Color, using built-in class Random
		/// </summary>
		/// <param name="seed">Seed for Random</param>
		/// <returns>Randomly generated color</returns>
		public static Color Generate(int seed) {
			Random rnd = new Random(seed);
			return Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
		}

		public static Color[] GenerateColors(int number) {
			Random rnd = new Random();
			Color[] res = new Color[number];
			for (int i = 0; i < number; i++) {
				res[i] = Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256));
			}
			return res;
		}
	}

	public static class MyColor {
		public static Color Generate(double alpha, double r, double g, double b) {
			return Color.FromArgb(Interpolation.CutToBorder(255 * alpha, 0, 255), Interpolation.CutToBorder(255 * r, 0, 255), Interpolation.CutToBorder(g * 255, 0, 255), Interpolation.CutToBorder(b * 255, 0, 255));
		}

		public static Color UncompressTo0_255(double r, double g, double b) {
			return Color.FromArgb(Interpolation.CutToBorder(255 * r, 0, 255), Interpolation.CutToBorder(g * 255, 0, 255), Interpolation.CutToBorder(b * 255, 0, 255));
		}

		public static Vector Decrease(Color c) {
			return new Vector(
				c.R / 256.0,
				c.G / 256.0,
				c.B / 256.0
				);
		}

		public static Vector CompressTo0_1(double R, double G, double B) {
			return new Vector(
				R / 256.0,
				G / 256.0,
				B / 256.0
				);
		}
	}
}
