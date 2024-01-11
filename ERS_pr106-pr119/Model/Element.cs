using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace ERS_pr106_pr119.Model
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

        public Element(int sat, string load, string oblast, string tip, string datumUvoza, string satnicaUvoza, string file_location, string datumImenaFajla, string fileName)
        {

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
                throw new FormatException("Format datuma uvoza mora biti DD/MM/YYYY");
            }

            if (ValidnostSatniceUvoza(satnicaUvoza.Split(':')) == false)
            {
                throw new FormatException("Santica uvozau 24h formatu hh/mm/ss");
            }

            if (ValidnostDatumaImenaFajla(datumImenaFajla.Split('.')) == false)
            {
                throw new FormatException("Format datuma na imenu fajla mora biti DD.MM.YYYY.");
            }

            if (ValidnostImenaFajla(fileName) == false)
            {
                throw new ArgumentException("Naziv datoteke mora biti formata 'tip_YYYY_MM_DD.xxx'", nameof(fileName));
            }

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

        private bool ValidnostSatniceUvoza(string[] satnicaUvoza)
        {
            if (satnicaUvoza.Length != 3) return false;
            string sat = satnicaUvoza[0];
            string minut = satnicaUvoza[1];
            string sekunda = satnicaUvoza[2];

            if (sat == "00")
                sat = satnicaUvoza[0].Substring(1);
            else
                sat = satnicaUvoza[0].TrimStart('0');

            if (minut == "00")
                minut = satnicaUvoza[1].Substring(1);
            else
                minut = satnicaUvoza[1].TrimStart('0');

            if (sekunda == "00")
                sekunda = satnicaUvoza[2].Substring(1);
            else
                sekunda = satnicaUvoza[2].TrimStart('0');

            bool minutSuccess = int.TryParse(minut, out _);
            bool sekundaSuccess = int.TryParse(sekunda, out _);

            if (!int.TryParse(sat, out _) || !minutSuccess || !sekundaSuccess)                        //Da li su unijete vrijednosti cijeli brojevi
                return false;
            if (satnicaUvoza[0].Length != 2 || satnicaUvoza[1].Length != 2 || satnicaUvoza[2].Length != 2)                                             //Da li su odgovarajuće dužine
                return false;
            if (int.Parse(sat) > 23 || int.Parse(sat) < 0 || int.Parse(minut) > 59 || int.Parse(minut) < 0 || int.Parse(sekunda) > 59 || int.Parse(sekunda) < 0)     //Da li su validne vrijednosti
                return false;
            return true;
        }


        private bool ValidnostDatumaImenaFajla(string[] datumImenaFajla)
        {
            if (datumImenaFajla.Length != 4) return false;

            string dan = datumImenaFajla[0].TrimStart('0');
            string mjesec = datumImenaFajla[1].TrimStart('0');

            if (!int.TryParse(dan, out _) || !int.TryParse(mjesec, out _) || !int.TryParse(datumImenaFajla[2], out _))                        //Da li su unijete vrijednosti cijeli brojevi
                return false;
            else if (datumImenaFajla[0].Length != 2 || datumImenaFajla[1].Length != 2 || datumImenaFajla[2].Length != 4)                                             //Da li su odgovarajuće dužine
                return false;
            else if (int.Parse(dan) > 31 || int.Parse(dan) < 0 || int.Parse(mjesec) > 12 || int.Parse(mjesec) < 0 || int.Parse(datumImenaFajla[2]) < 0)     //Da li su validne vrijednosti
                return false;
            else
                return true;
        }

        private bool ValidnostImenaFajla(string fileName)
        {
            string[] fileNameParts_Extension = fileName.Split('.');
            string[] fileNameParts = fileNameParts_Extension[0].Split('_');
            if (fileNameParts.Length != 4) return false;
            else if (fileNameParts[0] != "prog" && fileNameParts[0] != "ostv") return false;
            else if (ValidnostImenaFajla_Datum(fileNameParts[1], fileNameParts[2], fileNameParts[3]) == false) return false;
            else return true;
        }

        private bool ValidnostImenaFajla_Datum(string godina, string mjesecString, string danString)
        {
            string dan = danString.TrimStart('0');
            string mjesec = mjesecString.TrimStart('0');

            if (!int.TryParse(dan, out _) || !int.TryParse(mjesec, out _) || !int.TryParse(godina, out _))                        //Da li su unijete vrijednosti cijeli brojevi
                return false;
            else if (danString.Length != 2 || mjesecString.Length != 2 || godina.Length != 4)                                             //Da li su odgovarajuće dužine
                return false;
            else if (int.Parse(dan) > 31 || int.Parse(dan) < 0 || int.Parse(mjesec) > 12 || int.Parse(mjesec) < 0 || int.Parse(godina) < 0)     //Da li su validne vrijednosti
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

