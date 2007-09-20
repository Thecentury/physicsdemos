using System;
using System.Collections.Generic;
using System.Text;

namespace Thecentury.Attributes {

	/// <summary>
	/// Базовый класс, содержащий информацию о контактах создателей демонстрации
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public abstract class GeneralContactsAttribute : GeneralDemonstrationAttribute {
		protected string[] icqList = null;
		/// <summary>
		/// Список номеров ICQ
		/// </summary>
		public string[] ICQs {
			get { return icqList; }
			set { icqList = value; }
		}

		protected string[] emailList = null;
		/// <summary>
		/// Список адресов электронной почты
		/// </summary>
		public string[] EMails {
			get { return emailList; }
			set { emailList = value; }
		}

		protected string[] urlList = null;
		/// <summary>
		/// Список адресов сайтов в сети Интернет
		/// </summary>
		public string[] URLs {
			get { return urlList; }
			set { urlList = value; }
		}

		protected string[] mobilePhoneList = null;
		/// <summary>
		/// Список мобильных телефонов
		/// </summary>
		public string[] MobilePhones {
			get { return mobilePhoneList; }
			set { mobilePhoneList = value; }
		}

		protected string[] phoneList = null;
		/// <summary>
		/// Список телефонов
		/// </summary>
		public string[] Phones {
			get { return phoneList; }
			set { phoneList = value; }
		}

		private string[] msnList = null;
		/// <summary>
		/// Список идентификаторов Windows Messenger
		/// </summary>
		public string[] MSNs {
			get { return msnList; }
			set { msnList = value; }
		}

		/// <summary>
		/// Создает экземпляр класса GeneralContactsAttribute
		/// </summary>
		public GeneralContactsAttribute() { }
	}
}
