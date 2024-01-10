using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.Export;
using ERS_pr106_pr119.FileReader;
using ERS_pr106_pr119.SUBP;
using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;
using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
    internal class Program
	{
		static void Main()
		{
			TableSetupCommands.TableCreations();

			UI ui = new UI();
			string folderName = "xml";
			ReaderXML xmlReader = new ReaderXML();
			ExportDTO exportTable = new ExportDTO();
			InMemoryDataBaseDTO? inMemDB = new InMemoryDataBaseDTO();
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();
				switch (s)
				{
					case "1":
						xmlReader.Ucitaj(folderName);
						//inMemDB = xmlReader.UcitajInMemory(folderName);
						break;
					case "2":
						exportTable = ui.IspisOpcije();
						//if (inMemDB != null)
							//exportTable = ui.IspisOpcijeInMemory(inMemDB, "07.05.202s0.", "VOJ");
						if (exportTable == null)
							break;
						if (ui.ExportUpit() == true)
							new ExportToCSV().Export(exportTable);
						break;
				}
			}
			while (s != "q");

			//Unit Test TODO
			// Test za SUBP
		}
	}
}
