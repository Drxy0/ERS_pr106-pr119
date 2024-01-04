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
			TablesSetup.TableCreations();

			PrognozaEnergijeImpl prognozaImpl = new PrognozaEnergijeImpl();
			OstvarenaEnergijaImpl ostvarenaImpl = new OstvarenaEnergijaImpl();

			UI ui = new UI();
			ReaderXML xmlReader = new ReaderXML();
			List<FileDTO> files = new List<FileDTO>();
			ExportDTO exportTable = new ExportDTO();
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
						exportTable = ui.IspisOpcije();
						if (ui.ExportUpit() == true)
							new ExportToCSV().Export(exportTable);
						break;
					case "3":
						//TODO
						break;
				}
			}
			while (s != "q");
			
        }
    }
}
