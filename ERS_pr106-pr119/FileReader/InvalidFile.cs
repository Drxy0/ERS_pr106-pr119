using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.FileReader
{
	public class InvalidFile
	{
		private string vrijemeUcitavanja = string.Empty;
		private string imeFajla = string.Empty;
		private string lokacijaFajla = string.Empty;
		private int brojRedova;

		public InvalidFile(string vrijemeUcitavanja, string imeFajla, string lokacijaFajla, int brojRedova)
		{
			this.vrijemeUcitavanja = vrijemeUcitavanja;
			this.imeFajla = imeFajla;
			this.lokacijaFajla = lokacijaFajla;
			this.brojRedova = brojRedova;
		}

		public string VrijemeUcitavanja
		{
			get { return vrijemeUcitavanja; }
			set { vrijemeUcitavanja = value; }
		}

		public string ImeFajla
		{
			get { return imeFajla; }
			set { imeFajla = value; }
		}

		public string LokacijaFajla
		{
			get { return lokacijaFajla; }
			set { lokacijaFajla = value; }
		}

		public int BrojRedova
		{
			get { return brojRedova; }
			set { brojRedova = value; }
		}


	}

}
