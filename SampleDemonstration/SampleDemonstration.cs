using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

using Thecentury;
using Thecentury.Attributes;

namespace SampleDemonstration {
	/// <summary>
	/// Пример демонстрации - может служить скелетом для создания демонстраций
	/// </summary>
	[Authors("Бринчук Михаил", "Скоблов Никита")]
	//[Authors("А вот еще кто-то в качестве авторов не прокатит!")] // Ошибка! Несколько авторов запрещены!
	[AuthorsContacts(
		EMails = new string[] { "thecentury@gmail.com", "mtgrhox@gmail.com" },
		ICQs = new string[] { "240387601" },
		MSNs = new string[] { "thecentury@gmail.com", "mtgrhox@gmail.com" },
		MobilePhones = new string[] { "+7 903 256 03 56" },
		Phones = new string[] { "+7 495 173 62 88" })]
	[Title("Модельная демонстрация")]
	[Description("Просто модельная демонстрация и все... Ничего больше.")]
	[Remark("Не используйте данную демонстрацию на лекциях!")]
	[Modifiers("Nobody", "Somebody", "Anybody")]
	[Modifiers("Hello, world!")]
	public class Demonstration : GeneralDemonstration {
		/// <summary>
		/// Создает экземпляр модельной демонстрации
		/// </summary>
		public Demonstration() : base(new SampleDemonstration.MainDemonstrationControl()) { }
	}
}
