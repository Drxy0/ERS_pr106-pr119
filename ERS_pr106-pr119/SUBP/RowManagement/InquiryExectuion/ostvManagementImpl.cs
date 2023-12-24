using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion
{
    public class ostvManagementImpl : ostvManagement
    {

        public bool ExistsById(int sat,string tip, long vreme, IDbConnection connection)
        {
            string query = "select * from showing where ordnum_sh=:ordnum_sh";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterManagement.AddParameter(command, "sat_o", DbType.Int32);
                ParameterManagement.AddParameter(command, "tip_p", DbType.String);
                ParameterManagement.AddParameter(command, "vreme_p", DbType.Int64);
                ParameterManagement.SetParameterValue(command, "sat_o", sat);
                ParameterManagement.SetParameterValue(command, "tip_p", tip);
                ParameterManagement.SetParameterValue(command, "vreme_p", vreme);
                return command.ExecuteScalar() != null;
            }
        }


        public int SaveRow(Element entity, IDbConnection connection) {

            string insertSql = "insert into prog_potrosnja(sat_o, load_o, oblast_o, tip_p, vreme_p ,file_location_o) values (:sat_o, :load_o, :oblast_o, :tip_p, :file_location_o)";
            string updateSql = "update prog_potrosnja set sat_o=:sat_o, load_o = :load_o, oblast_o = :oblast_o, tip_p = :tip_p,vreme_p = : vreme_p, file_location_o = :file_location_o ";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Sat,entity.Tip,entity.Vreme, connection) ? updateSql : insertSql;
                ParameterManagement.AddParameter(command, "sat_o", DbType.Int32);
                ParameterManagement.AddParameter(command, "load_o", DbType.Int32);
                ParameterManagement.AddParameter(command, "oblast_o", DbType.String);
                ParameterManagement.AddParameter(command, "tip_p", DbType.String);
                ParameterManagement.AddParameter(command, "vreme_p", DbType.Int64);
                ParameterManagement.AddParameter(command, "file_location_o", DbType.String);

                ParameterManagement.SetParameterValue(command, "sat_o", entity.Sat);
                ParameterManagement.SetParameterValue(command, "load_o", entity.Load);
                ParameterManagement.SetParameterValue(command, "oblast_o", entity.Oblast);
                ParameterManagement.SetParameterValue(command, "tip_p", entity.Tip);
                ParameterManagement.SetParameterValue(command, "vreme_p", entity.Vreme);
                ParameterManagement.SetParameterValue(command, "file_location_o", entity.File);
                return command.ExecuteNonQuery();
            }

        }

        public void InsertRows(List<Element> listaCeleTabele) {

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
                catch(Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine($"Error: {ex.Message}");
                }


            }
        }
    }
}
