using ERS_pr106_pr119.FileReader;
using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
	internal class Program
	{
		static void Main()
		{
			
			UI ui = new UI();
			ReaderXML xmlReader = new ReaderXML();
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();

				switch (s)
				{
					case "1":
						xmlReader.Ucitaj();
						break;
				}
			}
			while (s != "q");
			
        SUBP.TablesSetup.TableCreations();
        }
	}
}
