using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.Export.Impl;
using ERS_pr106_pr119.FileReader.FileReaderImplementations;
using ERS_pr106_pr119.SUBP;
using ERS_pr106_pr119.SUBP.DAO.RowManagement.InquiryExectuion;
using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
    internal class Program
	{
		static void Main()
		{
			TableSetupCommands.TableCreations();

			IspisPodataka ispisPodataka = new IspisPodataka();
			GUI gui = new GUI();
			string folderName = "xml";
			ReaderXML xmlReader = new ReaderXML();
			ExportDTO exportTable = new ExportDTO();
			InMemoryDataBaseDTO? inMemDB = new InMemoryDataBaseDTO();
			string? s;
			do {
				gui.Show();
				s = Console.ReadLine();
				switch (s)
				{
					case "1":
						xmlReader.Ucitaj(folderName);
						//inMemDB = xmlReader.UcitajInMemory(folderName);
						break;
					case "2":
						exportTable = ispisPodataka.IspisOpcije();
						//if (inMemDB != null)
							//exportTable = ui.IspisOpcijeInMemory(inMemDB, "07.05.2020.", "VOJ");
						if (exportTable == null)
							break;
						if (ispisPodataka.ExportUpit() == true)
							new ExportToCSV().Export(exportTable);
						break;
				}
			}
			while (s != "q");
		}
	}
}
