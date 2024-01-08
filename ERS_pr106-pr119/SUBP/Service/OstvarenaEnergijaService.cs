using ERS_pr106_pr119.SUBP.RowManagement;
using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        }

        public List<Element> PullOstvPotrosnjaByDateAndArea(string datum, string oblast) { 
        
            return ostvMangament.PullOstvPotrosnjaByDateAndArea(datum,oblast).ToList();

        }

    }
}
