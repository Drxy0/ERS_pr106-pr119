using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
    public class Element
    {
            //tip string //string
            private int sat;
            private string load;
            private string oblast;
            private string tip;
            private DateTime datumUvoza;
            private DateTime satnicaUvoza;           //TREBA SA NJOM DA VIDIMO DAL TREBA SAMO VREME OD POKRETANJA PROGRAMA ILI I DATUM   
            private string file_location;
            private DateTime datumImenaFajla;
            private string fileName;

            public Element(string sat, string load, string oblast,object o) { // treba obrisati ovo i adaptirati file reader po donjem konstruktoru
    
                this.load = load;
                this.oblast = oblast;
                //this.tip = tip;

            }

            public Element(DateTime datum, String oblast) {

                this.datumImenaFajla = datum;
                this.oblast = oblast;

            }

            public Element(int sat, string load, string oblast,string tip, DateTime datumUvoza, DateTime satnicaUvoza,string file_location,DateTime datumImenaFajla,string fileName)
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

            public DateTime DatumUvoza
            {
                get { return datumUvoza; }
                set { datumUvoza = value; }
            }

            public DateTime SatnicaUvoza
            {
                get { return satnicaUvoza; }
                set { satnicaUvoza = value; }
            }

            public string File_location
            {
            get { return file_location; }
            set { file_location = value; }
            }

            public DateTime DatumImenaFajla
            {
            get { return datumImenaFajla; }
            set { datumImenaFajla = value; }
            }

            public string FileName
            {
            get { return fileName; }
            set { fileName = value; }
            }
    }

}

