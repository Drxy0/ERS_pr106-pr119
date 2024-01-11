using ERS_pr106_pr119.SUBP.DAO.RowManagement.InquiryExectuion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using ERS_pr106_pr119.Model;
using ERS_pr106_pr119.SUBP.DAO.RowManagement;

namespace ERS_pr106_pr119.SUBP.Service
{
    public class OstvarenaEnergijaService
    {
        private static readonly IOstvManagement ostvMangament = new OstvarenaEnergijaImpl();

        public List<Element> FindAll() { 
        
            return ostvMangament.FindAll().ToList();
        
        }

        public void InsertRows(List<Element> listaCeleTabele) {

            ostvMangament.InsertRows(listaCeleTabele);

            if (listaCeleTabele.Count == 0) {

                
            
            }

        }

        public List<Element> PullOstvPotrosnjaByDateAndArea(string datum, string oblast) {

            List<Element> list = FindAll();
            var uniqueValues = list.Select(el => el.DatumImenaFajla).Distinct();

            PodrucjeService podrucje = new PodrucjeService();

            if (!(Regex.IsMatch(oblast, "^[A-Z]+$")))                               //ABC...
            {
                throw new FormatException();
            }

            if (!(podrucje.FindAll().Any(gp => gp.Oblast == oblast))) {
                Console.WriteLine("Nema informacija za tu oblast!");
            }
            
            if (!(Regex.IsMatch(datum, @"^\d{2}\.\d{2}\.\d{4}\.$"))) {              //dd.mm.yyyy.

                throw new FormatException();
            }

            if (!string.IsNullOrEmpty(datum))
            {
                foreach (var value in uniqueValues)
                {
                    if (value == datum)
                        return ostvMangament.PullOstvPotrosnjaByDateAndArea(datum, oblast).ToList();
                }
                Console.WriteLine("Nema informacija za taj datum!");
                
            }

            throw new FormatException();

        }

    }
}
