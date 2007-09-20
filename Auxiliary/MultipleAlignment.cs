using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Thecentury {
	public class MultipleAlignment {
		public static void Align(string name, object value, params object[] objects) {
			foreach (object obj in objects) {
				Align(name, value, obj);
			}
		}

		public static void Align(string name, object value, object obj) {
			Type type = obj.GetType();

			PropertyInfo[] properties = type.GetProperties();
			foreach (PropertyInfo property in properties) {
				if (property.Name == name) {
					property.SetValue(obj, value, null);
					return;
				}
			}

			FieldInfo[] fields = type.GetFields();
			foreach (FieldInfo field in fields) {
				if (field.Name == name) {
					field.SetValue(obj, value);
					return;
				}
			}
			throw new Exception("Field or property \"" + name + "\" was not found in object " + obj.ToString());
		}

		public static void CallMethod(string name, object[] parameters, object obj) {
			Type type = obj.GetType();
			MethodInfo methInfo = type.GetMethod(name);
			methInfo.Invoke(obj, parameters);
		}

		public static void CallMethods(string name, object[] parameters, params object[] objects) {
			foreach (object obj in objects) {
				CallMethod(name, parameters, obj);
			}
		}
	}
}
