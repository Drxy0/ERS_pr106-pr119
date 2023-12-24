using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ERS_pr106_pr119
{
	internal class UI
	{
		public UI() { }

		public void Show()
		{
			string UIstring = "\nEvidencija potrošnje električne energije\n\n" +
								"1 - Importuj fajlove\n" +
								"2 - Ispisati prognoziranu i ostvarenu potrošnju\n" +
								"q - Quit program\n";
			Console.WriteLine(UIstring);
		}

		public void IspisOpcije(List<FileDTO> fileDTOs)
		{
			Console.Write("Izaberite datum: ");
			string datum = Console.ReadLine();
			Console.Write("Izaberite geografsku oblast: ");
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
			Console.WriteLine(GetFormattedHeader());
			for (int i = 0; i < Lostv.Count; i++)
			{
				double relativnoOdstupanje = ((double.Parse(Lostv[i].Load) - double.Parse(Lprog[i].Load)) / double.Parse(Lostv[i].Load) * 100);
				Console.WriteLine(string.Format("{0,-10} {1,-20} {2,-20} {3, -6} {4, -1}",
					Lostv[i].Sat, Lostv[i].Load, Lprog[i].Load, relativnoOdstupanje.ToString("F2"), "%"));
			}
		}
		private static string GetFormattedHeader()
		{
			return string.Format("\n{0,-10} {1,-20} {2,-20} {3,-20}",
								"SAT", "PROGNOZIRANA P.", "OSTVARENA P.", "RELATIVNO ODSTUPANJE");
		}

	}
}
