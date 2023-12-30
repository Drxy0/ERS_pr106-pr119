using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion
{
    public class PodrucijeImpl : IPodrucije
    {
        public bool ExistsById(string oblast, IDbConnection connection)
        {
            string query = "select * from podrucije where oblast=:oblast";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterManagement.AddParameter(command, "oblast", DbType.String);

                ParameterManagement.SetParameterValue(command, "oblast", oblast);

                return command.ExecuteScalar() != null;
            }

        }
        private int SaveRow(GeografskoPodrucije entity, IDbConnection connection)
        {

            string insertSql = "insert into podrucije(oblast, nazivP) values (:oblast, :nazivP)";

            string updateSql = "update prog_potrosnja set oblast=:oblast, nazivP = :nazivP";

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
        public void InsertRows(List<GeografskoPodrucije> listaCeleTabele) {

            int numSaved = 0;

            using (IDbConnection connection = ConnectionSetup.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (GeografskoPodrucije entity in listaCeleTabele)
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

        public void InsertRowFromPotrosnja(string oblast)
        {
            string insertSql = "insert into podrucije(oblast, nazivP) values (:oblast,:nazivP)";

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

                            command.ExecuteNonQuery();
                        }
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

        public IEnumerable<GeografskoPodrucije> FindAll()
        {
            string query = "select * from podrucije";
            List<GeografskoPodrucije> ret = new List<GeografskoPodrucije>();

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
                            GeografskoPodrucije gp = new GeografskoPodrucije(reader.GetString(0),reader.GetString(1));
                            ret.Add(gp);
                        }
                    }
                }
            }
            
            return ret;
        }



    }
}
