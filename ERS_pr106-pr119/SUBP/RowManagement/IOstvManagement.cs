﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.RowManagement
{
    public interface IOstvManagement : ITableManagement<Element>
    {
        List<Element> PullOstvPotrosnjaByDateAndArea(string datum,string oblast);

    }
}
