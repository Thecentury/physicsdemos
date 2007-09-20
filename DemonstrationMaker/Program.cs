using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace DemonstrationMaker {
	class Program {
		static void Main(string[] args) {
			// Пардон, что-то я у тебя в проекте удалил лишку)))

			//TODO: Создать объект и сериализировать его
			//    DemonstrationFormat df = new DemonstrationFormat("NullDemonstration.dll");
			//    List<DemonstrationFormat> lst = new List<DemonstrationFormat>();
			//    lst.Add(df);
			//    lst.Add(df);
			//    DemonstrationFormat df1 = new DemonstrationFormat(lst, "Null Pod Demo");
			//    //lst.Add(df1);
			//    lst.Add(df);
			//    List<DemonstrationFormat> lst1 = new List<DemonstrationFormat>();
			//    DemonstrationFormat df2 = new DemonstrationFormat(lst, "Null Demo Demonstration");

			//    TextWriter writer = new StreamWriter("NullDemo.Xml");

			//    XmlSerializer xs = new XmlSerializer(typeof(DemonstrationFormat));
			//    try
			//    {
			//        xs.Serialize(writer, df2);
			//    }
			//    catch (Exception e)
			//    {
			//        Console.WriteLine(e.Message);
			//    }
			//    Console.WriteLine("OK!");
			//    writer.Close();

			//    //FileStream fs = File.Open("1.xml", FileMode.Open);
			//    //DemonstrationFormat dfDes = (xs.Deserialize(fs) as DemonstrationFormat);
			//    //try
			//    //{
			//    //    Console.WriteLine("Members of dfDes:\n" + dfDes.NodeName.ToString());
			//    //}
			//    //catch (Exception e)
			//    //{
			//    //    Console.WriteLine(e.Message);
			//    //}
			//    //Console.ReadLine();
			//    //fs.Close();

			//    FileStream fs = File.Create("NullDemo.dem");
			//    BinaryFormatter bf = new BinaryFormatter();
			//    bf.Serialize(fs, df2);
			//    fs.Close();
			//    fs = File.Open("NullDemo.dem", FileMode.Open);
			//    DemonstrationFormat dfDes = (bf.Deserialize(fs) as DemonstrationFormat);

			//}
		}
	}
}