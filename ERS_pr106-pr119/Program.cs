using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
	internal class Program
	{
		static void Main()
		{
			UI ui = new UI();
			Reader reader = new Reader();
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();

				switch (s)
				{
					case "1":
						reader.Ucitaj();
						break;
				}
			}
			while (s != "q");

		}

	}
}
