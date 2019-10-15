using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSON_parser
{
	class Program
	{
		static Dictionary<string, string> ParseJson(string res)
		{
			var lines = res.Split("\r\n".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			var ht = new Dictionary<string, string>(20);
			var st = new Stack<string>(20);

			for (int i = 0; i < lines.Length; ++i)
			{
				var line = lines[i];
				var pair = line.Split(":".ToCharArray(), 2, StringSplitOptions.RemoveEmptyEntries);

				if (pair.Length == 2)
				{
					var key = ClearString(pair[0]);
					var val = ClearString(pair[1]);

					if (val == "{")
					{
						st.Push(key);
					}
					else
					{
						if (st.Count > 0)
						{
							key = string.Join("_", st) + "_" + key;
						}

						if (ht.ContainsKey(key))
						{
							ht[key] += "&" + val;
						}
						else
						{
							ht.Add(key, val);
						}
					}
				}
				else if (line.IndexOf('}') != -1 && st.Count > 0)
				{
					st.Pop();
				}
			}

			return ht;
		}

		static string ClearString(string str)
		{
			str = str.Trim();
			//Убираем кавычки
			var ind0 = str.IndexOf("\'");
			var ind1 = str.LastIndexOf("\'");

			if (ind0 != -1 && ind1 != -1)
			{
				str = str.Substring(ind0 + 1, ind1 - ind0 - 1);
			}
			else if (str[str.Length - 1] == ',')
			{
				str = str.Substring(0, str.Length - 1);
			}

			//str = HttpUtility.UrlDecode(str);

			return str;
		}

		static void Main(string[] args)
		{
			string doc = @"{'id': 'cb6e111111aaaaaa', 
						   'name': 'Code Tester', 
						   'first_name': 'Code', 
						   'last_name': 'Tester', 
						   'link': 'http://profile.live.com/cid-cb6e111111aaaaaa/', 
						   'gender': 'male', 
						   'emails': {
										'preferred': 'tester@gmail.com', 
							  'account': 'tester@gmail.com', 
							  'personal': null, 
							  'business': null
						   }, 
						   'locale': 'en_US', 
						   'updated_time': '2012-05-21T21:40:43+0000'
						}";

			var test = ParseJson(doc);
			foreach (KeyValuePair<string, string> kvp in test)
			{
				Console.WriteLine($"{kvp.Key}: {kvp.Value}");
			}
				Console.ReadKey();

		}
	}
}
