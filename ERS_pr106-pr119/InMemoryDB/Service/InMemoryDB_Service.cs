using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.InMemoryDB.Service
{
	public class InMemoryDB_Service
	{
		private InMemoryDataBaseDTO inMemDB;
		public InMemoryDB_Service(InMemoryDataBaseDTO inMemDB)
		{
			this.inMemDB = inMemDB;
		}

		public bool DataBaseCount()
		{
			if (inMemDB.PrognoziranaPotrosnja.Count != inMemDB.OstvarenaPotrosnja.Count ||
			   (inMemDB.PrognoziranaPotrosnja.Count == 0 && inMemDB.OstvarenaPotrosnja.Count == 0))
			{
				Console.WriteLine("Datoteke nemaju jednak broj polja ili su prazne!");
				return false;
			}
			else 
				return true;
		}

		public List<Element> PullProgPotrosnjaByDateAndArea(string datum, string geoOblast)
		{
			List<Element> prognozaTest = inMemDB.PrognoziranaPotrosnja;
			if (prognozaTest.Count == 0)
				return null;

			List<Element> pullPrognozirana = new List<Element>();
			foreach (Element el in prognozaTest)
			{
				if (el.DatumImenaFajla == datum && el.Oblast == geoOblast)
				{
					pullPrognozirana.Add(el);
				}
			}

			if (pullPrognozirana.Count == 0)
			{
				Console.WriteLine("Nijesu nađeni prognozirani podaci za navedene parametre u bazi podataka!");
				return null;
			}
			else
				return pullPrognozirana;
		}
		public List<Element> PullOstvPotrosnjaByDateAndArea(string datum, string geoOblast)
		{
			List<Element> ostvarenaTest = inMemDB.OstvarenaPotrosnja;
			if (ostvarenaTest.Count == 0)
				return null;

			List<Element> pullOstvarena = new List<Element>();
			foreach (Element el in ostvarenaTest)
			{
				if (el.DatumImenaFajla == datum && el.Oblast == geoOblast)
				{
					pullOstvarena.Add(el);
				}
			}
			if (pullOstvarena.Count == 0)
			{
				Console.WriteLine("Nijesu nađeni ostvareni podaci za navedene parametre u bazi podataka!");
				return null;
			}
			else
				return pullOstvarena;
		}
	}
}
