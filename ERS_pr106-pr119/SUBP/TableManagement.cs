using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP
{
    public interface TableManagement<T>
    {
        IEnumerable<T> FindAll();
        void InsertRows(List<T> listaCeleTabele);

    }
}
