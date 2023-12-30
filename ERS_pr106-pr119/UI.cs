using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.Export;
using ERS_pr106_pr119.SUBP.RowManagement;
using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;

namespace ERS_pr106_pr119
{
    internal class UI
	{
		private static readonly IPodrucije podrucije = new PodrucijeImpl();
		public UI() { }

		public void Show()
		{
			string UIstring = "\nEvidencija potrošnje električne energije\n\n" +
								"1 - Importuj fajlove\n" +
								"2 - Ispisati prognoziranu i ostvarenu potrošnju\n" +
								"q - Quit program\n";
			Console.WriteLine(UIstring);
		}

		public ExportDTO IspisOpcije(List<FileDTO> fileDTOs)
		{

		
            Console.WriteLine("------------------------------");
            foreach (FileDTO fileDTOPrint in fileDTOs)
            {
             
                Console.WriteLine(fileDTOPrint.Datum.GetDatum());
                Console.WriteLine("------------------------------");
            }
            Console.WriteLine("\n");

            Console.Write("Izaberite jedan od datuma: ");
			string datum = Console.ReadLine();

            Console.WriteLine("------------------------------\n");

            foreach (GeografskoPodrucije gp in podrucije.FindAll())
			{
                Console.WriteLine("Geografsko podrucije: ");
                Console.WriteLine(gp.NazivP);
                Console.WriteLine("Oblast: ");
                Console.WriteLine(gp.Oblast);
                Console.WriteLine("------------------------------");
            }

            Console.Write("\nIzaberite podrucije po oblasti: ");
			string geoOblast = Console.ReadLine();

			List<Element> Lostv = new List<Element>();
			List<Element> Lprog = new List<Element>();

			foreach (FileDTO fileDTO in fileDTOs)
			{
				if (datum == fileDTO.Datum.GetDatum())
				{
					foreach (Element el in fileDTO.Elements)
					{
						if (el.Oblast == geoOblast)
						{
							if (el.Tip == "ostv")
							{
								Lostv.Add(el);
							}
							else if (el.Tip == "prog")
							{
								Lprog.Add(el);
							}
						}
					}
				}
			}

			List<string> rOdstupanja = new List<string>();

			Console.WriteLine(GetFormattedHeader());
			for (int i = 0; i < Lostv.Count; i++)
			{
				double relativnoOdstupanje = ((double.Parse(Lostv[i].Load) - double.Parse(Lprog[i].Load)) / double.Parse(Lostv[i].Load) * 100);
				string rOdstupanjeString = relativnoOdstupanje.ToString("F2") + " %";

				rOdstupanja.Add(rOdstupanjeString);
				Console.WriteLine(string.Format("{0,-10} {1,-20} {2,-20} {3, -6} {4, -1}",
					Lostv[i].Sat, Lostv[i].Load, Lprog[i].Load, relativnoOdstupanje.ToString("F2"), "%"));
			}

			ExportDTO exportTable = new ExportDTO();
			exportTable.Oblast = geoOblast;
			exportTable.Datum = new Datum().SetDatumFromString(datum);
			exportTable.OstvarenaP = Lostv;
			exportTable.PrognoziranaP = Lprog;
			exportTable.Odstupanja = rOdstupanja;

			return exportTable;
		}
		private static string GetFormattedHeader()
		{
			return string.Format("\n{0,-10} {1,-20} {2,-20} {3,-20}",
								"SAT", "PROGNOZIRANA P.", "OSTVARENA P.", "RELATIVNO ODSTUPANJE");
		}

		public bool ExportUpit()
		{
			Console.WriteLine("\nDa li želite exportovati ovu tabelu?\n (+) DA\t (-) NE\n");
			string potvrda = Console.ReadLine();

			if (potvrda == "+") return true;
			else if (potvrda == "-") return false;
			return ExportUpit();
		}

	}
}
