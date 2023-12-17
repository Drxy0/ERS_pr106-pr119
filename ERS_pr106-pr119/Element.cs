using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
    public class Element
    {
            private string sat;
            private string load;
            private string oblast;
            private string tip;

            public Element(string sat, string load, string oblast,string tip)
            {
                this.sat = sat;
                this.load = load;
                this.oblast = oblast;
                this.tip = tip;
            }

            public string Sat
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
    }

}

