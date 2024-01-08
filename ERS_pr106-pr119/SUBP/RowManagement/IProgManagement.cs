using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.RowManagement
{
    public interface IProgManagement : ITableManagement<Element>
    {
        List<Element> PullProgPotrosnjaByDateAndArea(string datum, string oblast);
    }
}
