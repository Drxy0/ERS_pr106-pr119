using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.Connection
{
    public interface IConnection : IDisposable
    {
        void OverrideConnection(IDbConnection mockConnection);

        IDbConnection GetConnection();

        void Dispose();

    }
}
