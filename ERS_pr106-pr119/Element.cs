using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
    public class Element
    {
            private int sat;
            private string load;
            private string oblast;
            private string tip;
            private long vreme;           //TREBA SA NJOM DA VIDIMO DAL TREBA SAMO VREME OD POKRETANJA PROGRAMA ILI I DATUM   
            private string file_location;

            public Element(string sat, string load, string oblast,object o) { // treba obrisati ovo i adaptirati file reader po donjem konstruktoru
    
                this.load = load;
                this.oblast = oblast;
                //this.tip = tip;

            }
            public Element(int sat, string load, string oblast,string tip, long vreme,string file_location)
            {
                this.sat = sat;
                this.load = load;
                this.oblast = oblast;
                this.tip = tip;
                this.vreme = vreme;
                this.file_location = file_location;
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

            public long Vreme
            {
                get { return vreme; }
                set { vreme = value; }
            }

            public string File
            {
            get { return file_location; }
            set { file_location = value; }
            }
    }

}

