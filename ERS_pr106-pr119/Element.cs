using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            public Element(string datum, String oblast)
            {
                this.datumImenaFajla = datum;
                this.oblast = oblast;
            }

            public Element(int sat, string load, string oblast,string tip, string datumUvoza, string satnicaUvoza, string file_location, string datumImenaFajla, string fileName)
            {
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

