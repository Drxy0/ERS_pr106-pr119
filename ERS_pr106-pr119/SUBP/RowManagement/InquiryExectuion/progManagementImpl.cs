using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion
{
    public class progManagementImpl : progManagement
    {
        public bool ExistsById(int sat, string tip, long vreme, IDbConnection connection)
        {
            string query = "select * from prog_potrosnja where sat_p=:sat_p,tip_p=:tip_p,vreme_p=:vreme_p";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterManagement.AddParameter(command, "sat_p", DbType.Int32);
                ParameterManagement.AddParameter(command, "tip_p", DbType.String);
                ParameterManagement.AddParameter(command, "vreme_p", DbType.Int64);
                ParameterManagement.SetParameterValue(command, "sat_p", sat);
                ParameterManagement.SetParameterValue(command, "tip_p", tip);
                ParameterManagement.SetParameterValue(command, "vreme_p", vreme);
                return command.ExecuteScalar() != null;
            }
        }


        private int SaveRow(Element entity, IDbConnection connection)
        {

            string insertSql = "insert into prog_potrosnja(sat_p, load_p, oblast_p, tip_p, vreme_p ,file_location_p) values (:sat_p, :load_p, :oblast_p, :tip_p, :file_location_p)";
            string updateSql = "update prog_potrosnja set sat_p=:sat_p, load_p = :load_p, oblast_p = :oblast_p, tip_p = :tip_p,vreme_p = : vreme_p, file_location_p = :file_location_p ";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Sat, entity.Tip, entity.Vreme, connection) ? updateSql : insertSql;
                ParameterManagement.AddParameter(command, "sat_p", DbType.Int32);
                ParameterManagement.AddParameter(command, "load_p", DbType.Int32);
                ParameterManagement.AddParameter(command, "oblast_p", DbType.String);
                ParameterManagement.AddParameter(command, "tip_p", DbType.String);
                ParameterManagement.AddParameter(command, "vreme_p", DbType.Int64);
                ParameterManagement.AddParameter(command, "file_location_p", DbType.String);

                ParameterManagement.SetParameterValue(command, "sat_p", entity.Sat);
                ParameterManagement.SetParameterValue(command, "load_p", entity.Load);
                ParameterManagement.SetParameterValue(command, "oblast_p", entity.Oblast);
                ParameterManagement.SetParameterValue(command, "tip_p", entity.Tip);
                ParameterManagement.SetParameterValue(command, "vreme_p", entity.Vreme);
                ParameterManagement.SetParameterValue(command, "file_location_p", entity.File);
                return command.ExecuteNonQuery();
            }

        }

        public void InsertRows(List<Element> listaCeleTabele)
        {

            int numSaved = 0;

            using (IDbConnection connection = ConnectionSetup.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (Element entity in listaCeleTabele)
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
    }
}
