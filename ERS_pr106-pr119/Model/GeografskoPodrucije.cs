using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.Model
{
    public class Geografskopodrucje
    {
        private string oblast;
        private string nazivP;

        public Geografskopodrucje(string oblast, string naziv)
        {
            this.oblast = oblast;
            nazivP = naziv;
        }

        public string Oblast
        {
            get { return oblast; }
            set { oblast = value; }
        }

        public string NazivP
        {
            get { return nazivP; }
            set { nazivP = value; }
        }

    }
}
