using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERS_pr106_pr119.Export
{
	public class ExportToCSV : IExportTable
	{
		public void Export(ExportDTO exportDTO)
		{
			string fileName = exportDTO.Oblast + " " + exportDTO.Datum.Godina + "_" + exportDTO.Datum.Mjesec + "_" + exportDTO.Datum.Dan + ".csv";
			string filePath = new FilesDirectory().ExportDirectory() + fileName;
			
			try
			{
				using (StreamWriter sw = new StreamWriter(filePath))
				{
					sw.WriteLine("SAT,PROGNOZIRANA P.,OSTVARENA P.,RELATIVNO ODSTUPANJE");

					for (int i = 0; i < exportDTO.OstvarenaP.Count; i++)
					{
						string line = exportDTO.OstvarenaP[i].Sat + ","
									+ exportDTO.PrognoziranaP[i].Load + ","
									+ exportDTO.OstvarenaP[i].Load + ","
									+ exportDTO.Odstupanja[i];
						sw.WriteLine(line);
					}
				}

				Console.WriteLine("Tabela uspješno eksportovana.");
				Console.WriteLine("Lokacija:  ./Exported Tables");
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Export error: {ex.Message}");
			}
		}
	}
}
