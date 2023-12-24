using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
	public class Datum
	{
		private string dan;
		private string mjesec;
		private string godina;
		public Datum(string dan, string mjesec, string godina)
		{
			this.dan = dan;
			this.mjesec = mjesec;
			this.godina = godina;
		}
		public Datum()
		{
			this.dan = "";
			this.mjesec = "";
			this.godina = "";
		}
		public string Dan
		{
			get { return this.dan; }
			set { this.dan = value; }
		}
		public string Mjesec
		{
			get { return this.mjesec; }
			set { this.mjesec = value; }
		}
		public string Godina
		{
			get { return this.godina; }
			set { this.godina = value; }
		}
		public string GetDatum()
		{
			string datum = this.Dan + "." + this.Mjesec + "." + this.Godina + ".";
			return datum;
		}

		public Datum SetDatumFromString(string datum)
		{
			string[] dijelovi = datum.Split('.');
			return new Datum(dijelovi[0], dijelovi[1], dijelovi[2]);
		}
	}
}
