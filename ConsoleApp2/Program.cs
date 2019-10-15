using System;
using System.Xml;
class Program
{
	static void Main(string[] args)
	{
		XmlDocument xDoc = new XmlDocument();
		xDoc.Load("users.xml");

		// получим корневой элемент
		XmlElement xRoot = xDoc.DocumentElement;
		// обход всех узлов в корневом элементе
		foreach (XmlNode xnode in xRoot)
		{
			// получаем атрибут name
			if (xnode.Attributes.Count > 0)
			{
				XmlNode attr = xnode.Attributes.GetNamedItem("name");
				if (attr != null)
					Console.WriteLine(attr.Value);
			}
			// обходим все дочерние узлы элемента user
			foreach (XmlNode childnode in xnode.ChildNodes)
			{
				// если узел - company
				if (childnode.Name == "company")
				{
					Console.WriteLine("Компания: {0}", childnode.InnerText);
				}
				// если узел age
				if (childnode.Name == "age")
				{
					Console.WriteLine("Возраст: {0}", childnode.InnerText);
				}
			}
			Console.WriteLine();
		}
		Console.Read();
	}
}
