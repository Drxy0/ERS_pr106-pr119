﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.Model
{
    public class Datum
    {
        private string dan;
        private string mjesec;
        private string godina;
        public Datum(string dan, string mjesec, string godina)
        {
            if (ValidnostDatuma(dan, mjesec, godina) == false)
            {
                throw new FormatException("Format datuma mora biti DD.MM.YYYY.");
            }

            this.dan = dan;
            this.mjesec = mjesec;
            this.godina = godina;
        }

        private bool ValidnostDatuma(string dan, string mjesec, string godina)
        {
            if (dan.Length != 2 || mjesec.Length != 2 || godina.Length != 4)
                return false;
            else if (!int.TryParse(dan, out _) || !int.TryParse(mjesec, out _) || !int.TryParse(godina, out _))                        //Da li su unijete vrijednosti cijeli brojevi
                return false;
            else if (int.Parse(dan) > 31 || int.Parse(dan) < 0 || int.Parse(mjesec) > 12 || int.Parse(mjesec) < 0 || int.Parse(godina) < 0)     //Da li su vrijednosti validne
                return false;
            else
                return true;
        }
        public Datum()
        {
            dan = string.Empty;
            mjesec = string.Empty;
            godina = string.Empty;
        }
        public string Dan
        {
            get { return dan; }
            set { dan = value; }
        }
        public string Mjesec
        {
            get { return mjesec; }
            set { mjesec = value; }
        }
        public string Godina
        {
            get { return godina; }
            set { godina = value; }
        }
        public string GetDatum()
        {
            string datum = Dan + "." + Mjesec + "." + Godina + ".";
            return datum;
        }

        public Datum SetDatumFromString(string datum)
        {
            string[] dijelovi = datum.Split('.');
            if (dijelovi.Length != 4)
            {
                throw new FormatException("Format datuma mora biti DD.MM.YYYY.");
            }

            return new Datum(dijelovi[0], dijelovi[1], dijelovi[2]);
        }
    }
}
