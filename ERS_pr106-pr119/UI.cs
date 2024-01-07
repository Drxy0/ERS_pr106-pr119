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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERS_pr106_pr119
{
    internal class UI
	{
		private static readonly IPodrucje podrucje = new PodrucjeImpl();

		private static readonly PrognozaEnergijeImpl prognozaImpl = new PrognozaEnergijeImpl();
		private static readonly OstvarenaEnergijaImpl ostvarenaImpl = new OstvarenaEnergijaImpl();
		public UI() { }

		public void Show()
		{
			string UIstring = "\nEvidencija potrošnje električne energije\n\n" +
								"1 - Importuj fajlove\n" +
								"2 - Ispisati prognoziranu i ostvarenu potrošnju\n" +
								"q - Quit program\n";
			Console.WriteLine(UIstring);
		}

		public ExportDTO IspisOpcije() 
        {
			List<Element> prognozaTest = new List<Element>();
			List<Element> ostvarenaTest = new List<Element>();
			prognozaTest = prognozaImpl.FindAll().ToList();
			ostvarenaTest = ostvarenaImpl.FindAll().ToList();

			string datum = GetInputDate(prognozaTest);
			if (datum == "q") { return null; }
			string geoOblast = getInputOblast();
			if (geoOblast == "q") { return null; }

			List<Element> Lostv = new List<Element>();
			List<Element> Lprog = new List<Element>();

			Lprog = prognozaImpl.PullProgPotrosnjaByDateAndArea(datum, geoOblast);
			Lostv = ostvarenaImpl.PullOstvPotrosnjaByDateAndArea(datum, geoOblast);

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
			string? potvrda = Console.ReadLine();

			if (potvrda == "+") return true;
			else if (potvrda == "-") return false;
			return ExportUpit();
		}

		private string GetInputDate(List<Element> prognozaTest)
		{
			string? datum = string.Empty;
			var uniqueValues = prognozaTest.Select(el => el.DatumImenaFajla).Distinct();
			while (true)
			{
				Console.WriteLine("------------------------------");
				foreach (var value in uniqueValues)
				{
					Console.WriteLine(value);
					Console.WriteLine("------------------------------");
				}
				Console.WriteLine("\n");
				Console.Write("Izaberite jedan od datuma (q - exit): ");
				datum = Console.ReadLine();
				Console.WriteLine();

				if (!string.IsNullOrEmpty(datum))
				{
					if (datum == "q")
						return datum;

					foreach (var value in uniqueValues)
					{
						if (value == datum)
							return datum;
					}
					Console.WriteLine("Nema informacija za taj datum!");
				}
			}
		}

		private string getInputOblast()
		{
			string? geoOblast = string.Empty;
			while(true)
			{
				Console.WriteLine("------------------------------\n");
				foreach (Geografskopodrucje gp in podrucje.FindAll())
				{
					Console.WriteLine("Geografsko područje: " + gp.NazivP);
					Console.WriteLine("------------------------------");
					Console.WriteLine();
				}
				Console.Write("\nIzaberite područje (q - exit): ");
				geoOblast = Console.ReadLine();
				Console.WriteLine();

				if (!string.IsNullOrEmpty(geoOblast))
				{
					if (geoOblast == "q")
						return geoOblast;
					foreach (Geografskopodrucje gp in podrucje.FindAll())
					{
						if (geoOblast == gp.NazivP)
							return geoOblast;
					}
					Console.WriteLine("Unesite validno područje!");
				}
			}
		}

	}
}
