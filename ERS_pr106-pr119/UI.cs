using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

			foreach(FileDTO fileDTO in fileDTOs)
			{
				if (datum == fileDTO.Datum.GetDatum())
				{

					foreach (Element el in fileDTO.Elements)
					{
						Console.WriteLine(el.Oblast);
						if (el.Oblast == geoOblast)
						{

						}
					}
				}

			}


		}

	}
}
