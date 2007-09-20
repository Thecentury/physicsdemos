using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Thecentury {
	public sealed class MyString {
		public static int NumberOfOccurences(string where, char what) {
			int res = 0;
			foreach (char c in where) {
				if (c == what) {
					res++;
				}
			}
			return res;
		}

		public static string ConvertToString(float a, NumberFormatInfo info) {
			double abs = Math.Abs(a);
			string res;
			if (abs == 0.0) {
				res = 0.ToString();
			}
			else if (abs < 0.01) {
				res = a.ToString("#.##e0", info);
			}
			else if (abs < 1.0) {
				res = a.ToString("n3", info);
			}
			else if (abs < 10.0) {
				res = a.ToString("n2", info);
			}
			else if (abs < 100.0) {
				res = a.ToString("n1", info);
			}
			else if (abs < 1000) {
				res = a.ToString("n0", info);
			}
			else {
				res = a.ToString("#.##e0", info);
			}

			string[] substrings = res.Split('e');
			if (res != "0") {
				if (substrings.Length > 1) {
					substrings[0] = substrings[0].TrimEnd('0');
				}
				string[] subsubstrings = substrings[0].Split('.');
				if (subsubstrings.Length > 1) {
					subsubstrings[1] = subsubstrings[1].TrimEnd('0');
					if (subsubstrings[1] != "") {
						substrings[0] = subsubstrings[0] + "." + subsubstrings[1];
					}
					else substrings[0] = subsubstrings[0];
				}
				else {
					substrings[0] = subsubstrings[0];
				}
			}
			if (substrings.Length > 1) {
				res = substrings[0] + "^" + substrings[1];
			}
			else {
				res = substrings[0];
			}

			return res;
		}

	}
}
