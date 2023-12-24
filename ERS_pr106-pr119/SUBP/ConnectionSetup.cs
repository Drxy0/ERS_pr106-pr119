using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Oracle.ManagedDataAccess.Client;

namespace ERS_pr106_pr119.SUBP
{
    public class ConnectionSetup: IDisposable
    {
        private static IDbConnection? connection = null;

        public static IDbConnection GetConnection()
        {
            if (connection == null || connection.State == System.Data.ConnectionState.Closed)
            {
                OracleConnectionStringBuilder ocsb = new OracleConnectionStringBuilder();
               
                ocsb.DataSource = ConnectionParam.LOCAL_DATA_SOURCE;
                ocsb.UserID = ConnectionParam.USER_ID;
                ocsb.Password = ConnectionParam.PASSWORD;
                ocsb.Pooling = true;
                ocsb.MinPoolSize = 1;
                ocsb.MaxPoolSize = 10;
                ocsb.IncrPoolSize = 3;
                ocsb.ConnectionLifeTime = 5;
                ocsb.ConnectionTimeout = 30;

                connection = new OracleConnection(ocsb.ConnectionString);
                
                
            }
            return connection;
        }

        public void Dispose()
        {
            if (connection != null)
            {
                connection.Close();
                connection.Dispose();
            }

        }

    }


}
