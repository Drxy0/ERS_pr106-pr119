using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERS_pr106_pr119.Model;
using ERS_pr106_pr119.SUBP.Connection;
using ERS_pr106_pr119.SUBP.DAO.RowManagement;

namespace ERS_pr106_pr119.SUBP.DAO.RowManagement.InquiryExectuion
{
    public class PodrucjeImpl : IPodrucje
    {
        private bool ExistsById(string oblast, IDbConnection connection)
        {
            string query = "select * from podrucje where oblast=:oblast";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterManagement.AddParameter(command, "oblast", DbType.String);

                ParameterManagement.SetParameterValue(command, "oblast", oblast);

                return command.ExecuteScalar() != null;
            }

        }
        private int SaveRow(Geografskopodrucje entity, IDbConnection connection)
        {

            string insertSql = "insert into podrucje(oblast, nazivP) values (:oblast, :nazivP)";

            string updateSql = "update podrucje set oblast=:oblast, nazivP = :nazivP";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Oblast, connection) ? updateSql : insertSql;

                ParameterManagement.AddParameter(command, "oblast", DbType.String);
                ParameterManagement.AddParameter(command, "nazivP", DbType.String);


                ParameterManagement.SetParameterValue(command, "oblast", entity.Oblast);
                ParameterManagement.SetParameterValue(command, "nazivP", entity.NazivP);


                return command.ExecuteNonQuery();
            }

        }
        public void InsertRows(List<Geografskopodrucje> listaCeleTabele)
        {

            int numSaved = 0;

            using (IDbConnection connection = ConnectionSetup.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (Geografskopodrucje entity in listaCeleTabele)
                    {
                        numSaved += SaveRow(entity, connection);
                    }

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                }

            }
        }

        public int InsertRowFromPotrosnja(string oblast)
        {
            string insertSql = "insert into podrucje(oblast, nazivP) values (:oblast,:nazivP)";
            
            int numSaved = 0;

            using (IDbConnection connection = ConnectionSetup.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();

                try
                {
                    if (!ExistsById(oblast, connection))
                    {
                        using (IDbCommand command = connection.CreateCommand())
                        {
                            command.CommandText = insertSql;

                            ParameterManagement.AddParameter(command, "oblast", DbType.String);
                            ParameterManagement.AddParameter(command, "nazivP", DbType.String);

                            command.Prepare();

                            ParameterManagement.SetParameterValue(command, "oblast", oblast);
                            ParameterManagement.SetParameterValue(command, "nazivP", oblast);

							numSaved += command.ExecuteNonQuery();

                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                }
                return numSaved;
            }
		}

		public IEnumerable<Geografskopodrucje> FindAll()
        {
            string query = "select * from podrucje";
            List<Geografskopodrucje> ret = new List<Geografskopodrucje>();

            using (IDbConnection connection = ConnectionSetup.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Geografskopodrucje gp = new Geografskopodrucje(reader.GetString(0), reader.GetString(1));
                            ret.Add(gp);
                        }
                    }
                }
            }

            return ret;
        }



    }
}
