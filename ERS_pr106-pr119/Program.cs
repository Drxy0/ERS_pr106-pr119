﻿using ERS_pr106_pr119.DTO;
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
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();
				switch (s)
				{
					case "1":
						xmlReader.Ucitaj(folderName);
						break;
					case "2":
						exportTable = ui.IspisOpcije();
						if (exportTable == null)
							break;
						if (ui.ExportUpit() == true)
							new ExportToCSV().Export(exportTable);
						break;
				}
			}
			while (s != "q");
        }
    }
}
