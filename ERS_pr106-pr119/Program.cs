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
			List<FileDTO> files = new List<FileDTO>();
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();

				switch (s)
				{
					case "1":
						files = xmlReader.Ucitaj();
						break;
					case "2":
						ui.IspisOpcije(files);
						break;
				}
			}
			while (s != "q");
			
        SUBP.TablesSetup.TableCreations();
        }
	}
}
