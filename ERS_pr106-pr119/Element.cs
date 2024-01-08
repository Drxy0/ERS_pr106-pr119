using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ERS_pr106_pr119
{
    public class Element
    {
            private int sat = 0;
            private string load = string.Empty;
            private string oblast = string.Empty;
            private string tip = string.Empty;
            private string datumUvoza = string.Empty;
            private string satnicaUvoza = string.Empty;
		    private string file_location = string.Empty;
		    private string datumImenaFajla = string.Empty;
		    private string fileName = string.Empty;

            public Element(int sat, string load, string oblast,string tip, string datumUvoza, string satnicaUvoza, string file_location, string datumImenaFajla, string fileName)
            {   
                /*
			    if (tip != "ostv" && tip != "prog")
                {
                    throw new ArgumentException("Tip mora biti ili ostv ili prog", nameof(tip));
                }

                if (!int.TryParse(load, out _) || int.Parse(load) < 0)
                {
				    throw new ArgumentException("Load mora biti pozitivan cio broj", nameof(load));
			    }

                if (ValidnostDatuma(datumUvoza.Split('/')) == false)
                {
                    throw new FormatException("Format datuma mora biti DD/MM/YYYY");
			    }
                */

				this.sat = sat;
                this.load = load;
                this.oblast = oblast;
				this.tip = tip;
			    this.datumUvoza = datumUvoza;
                this.satnicaUvoza = satnicaUvoza;
                this.file_location = file_location;
			    this.datumImenaFajla = datumImenaFajla;
			    this.fileName = fileName;
			}

            private bool ValidnostDatuma(string[] datum)
            {
			    if (datum.Length != 3)
				    return false;

			    string dan = datum[0].TrimStart('0');
                string mjesec = datum[1].TrimStart('0');

                if (!int.TryParse(dan, out _) || !int.TryParse(mjesec, out _) || !int.TryParse(datum[2], out _))                        //Da li su unijete vrijednosti cijeli brojevi
                    return false;
                else if (datum[0].Length != 2 || datum[1].Length != 2 || datum[2].Length != 4)                                             //Da li su odgovarajuće dužine
                    return false;
                else if (int.Parse(dan) > 31 || int.Parse(dan) < 0 || int.Parse(mjesec) > 12 || int.Parse(mjesec) < 0 || int.Parse(datum[2]) < 0)     //Da li su validne vrijednosti
                    return false;
                else
                    return true;
			}

		    public int Sat
            {
                get { return sat; }
                set { sat = value; }
            }

            public string Load
            {
                get { return load; }
                set { load = value; }
            }

            public string Oblast
            {
                get { return oblast; }
                set { oblast = value; }
            }

            public string Tip
            {
                get { return tip; }
                set { tip = value; }
            }

            public string DatumUvoza
            {
                get { return datumUvoza; }
                set { datumUvoza = value; }
            }

            public string SatnicaUvoza
            {
                get { return satnicaUvoza; }
                set { satnicaUvoza = value; }
            }

            public string File_location
            {
                get { return file_location; }
                set { file_location = value; }
            }
            public string FileName
            {
                get { return fileName; }
                set { fileName = value; }
            }

		    public string DatumImenaFajla
		    {
			    get { return datumImenaFajla; }
			    set { datumImenaFajla = value; }
		    }
	}

}

