using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;
using ERS_pr106_pr119.SUBP.RowManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.Service
{
    public class PodrucjeService
    {
        private static readonly IPodrucje podrucje = new PodrucjeImpl();

        public List<Geografskopodrucje> FindAll()
        {

            return podrucje.FindAll().ToList();

        }

        public void InsertRows(List<Geografskopodrucje> listaCeleTabele)
        {

            podrucje.InsertRows(listaCeleTabele);

        }

        public void InsertRowFromPotrosnja(string oblast) 
        {

            podrucje.InsertRowFromPotrosnja(oblast);

        }


    }
}
