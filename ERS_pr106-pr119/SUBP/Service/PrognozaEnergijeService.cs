using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;
using ERS_pr106_pr119.SUBP.RowManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.Service
{
    public class PrognozaEnergijeService
    {
        private static readonly IProgManagement progManagement = new PrognozaEnergijeImpl();

        public List<Element> FindAll()
        {

            return progManagement.FindAll().ToList();

        }

        public void InsertRows(List<Element> listaCeleTabele)
        {

            progManagement.InsertRows(listaCeleTabele);

        }

        public List<Element> PullProgPotrosnjaByDateAndArea(string datum, string oblast)
        {

            return progManagement.PullProgPotrosnjaByDateAndArea(datum, oblast).ToList();

        }
    }
}
