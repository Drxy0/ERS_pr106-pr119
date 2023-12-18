using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
	internal class Program
	{
		static void Main()
		{
			
			UI ui = new UI();
			FileReader reader = new FileReader();
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
			
        SUBP.TablesSetup.TableCreations();
        }
	}
}
