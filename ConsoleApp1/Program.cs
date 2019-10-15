using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;


namespace ConsoleApp1
{
	[DataContract]
	class User
	{
		public User(string name, int age)
		{
			Name = name;
			Age = age;
		}

		[DataMember]
		public string Name { get; set; }

		[DataMember]
		public int Age { get; set; }
	}

	class Program
	{
		static void Main(string[] args)
		{
			User user1 = new User("Ivan",35);
			User user2 = new User("Stepan", 33);
			User user3 = new User("Sergey", 34);
			User[] users = new User[] { user1, user2, user3 };
			//Сериализуем объект в память
			DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(User[]));
			MemoryStream msObj2 = new MemoryStream();
			serializer.WriteObject(msObj2, users);

			//Читаем символы из потока начиная с позиции Position
			msObj2.Position = 0;
			StreamReader reader = new StreamReader(msObj2);
			string json2 = reader.ReadToEnd();
			Console.WriteLine(json2);


			Console.ReadKey();

			reader.Close();
			msObj2.Close();
		}
	}
}
