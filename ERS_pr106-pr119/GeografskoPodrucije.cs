﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
    public class GeografskoPodrucije
    {
        private string oblast;
        private string nazivP;

        public GeografskoPodrucije(string oblast, string naziv)
        {
            this.oblast = oblast;
            this.nazivP = naziv;

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
