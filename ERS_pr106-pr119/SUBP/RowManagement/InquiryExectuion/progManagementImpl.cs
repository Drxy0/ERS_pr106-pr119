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
        public bool ExistsById(int sat, string fileName, IDbConnection connection)
        {
            string query = "select * from prog_potrosnja where sat_p=:sat_p AND fileName_p=:fileName_p";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterManagement.AddParameter(command, "sat_p", DbType.Int32);
                ParameterManagement.AddParameter(command, "fileName_p", DbType.String);

                ParameterManagement.SetParameterValue(command, "sat_p", sat);
                ParameterManagement.SetParameterValue(command, "tip_p", fileName);

                return command.ExecuteScalar() != null;
            }

        }


        private int SaveRow(Element entity, IDbConnection connection)
        {

            string insertSql = "insert into prog_potrosnja(sat_p, load_p, oblast_p, tip_p, datumUvoza_p ,satnicaUvoza_p ,file_location_p,datumImenaFajla_p,fileName_p )" +
                " values (:sat_p, :load_p, :oblast_p, :tip_p, :datumUvoza_p, :satnicaUvoza_p, :file_location_p, :datumImenaFajla_p, :fileName_p)";

            string updateSql = "update prog_potrosnja set" +
                " sat_p=:sat_p, load_p = :load_p, oblast_p = :oblast_p, tip_p = :tip_p,datumUvoza_p = : datumUvoza_p,satnicaUvoza_p = : satnicaUvoza_p," +
                " file_location_p = :file_location_p,datumImenaFajla_p=:datumImenaFajla_p,fileName_p:=fileName_p ";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Sat, entity.FileName, connection) ? updateSql : insertSql;
                ParameterManagement.AddParameter(command, "sat_p", DbType.Int32);
                ParameterManagement.AddParameter(command, "load_p", DbType.Int32);
                ParameterManagement.AddParameter(command, "oblast_p", DbType.String);
                ParameterManagement.AddParameter(command, "tip_p", DbType.String);
                ParameterManagement.AddParameter(command, "datumUvoza_p", DbType.DateTime);
                ParameterManagement.AddParameter(command, "satnicaUvoza_p", DbType.DateTime);
                ParameterManagement.AddParameter(command, "file_location_p", DbType.String);
                ParameterManagement.AddParameter(command, "datumImenaFajla_p", DbType.DateTime);
                ParameterManagement.AddParameter(command, "fileName_p", DbType.String);

                ParameterManagement.SetParameterValue(command, "sat_p", entity.Sat);
                ParameterManagement.SetParameterValue(command, "load_p", entity.Load);
                ParameterManagement.SetParameterValue(command, "oblast_p", entity.Oblast);
                ParameterManagement.SetParameterValue(command, "tip_p", entity.Tip);
                ParameterManagement.SetParameterValue(command, "datumUvoza_p", entity.DatumUvoza);
                ParameterManagement.SetParameterValue(command, "satnicaUvoza_p", entity.SatnicaUvoza);
                ParameterManagement.SetParameterValue(command, "file_location_p", entity.File_location);
                ParameterManagement.SetParameterValue(command, "datumImenaFajla_p", entity.DatumImenaFajla);
                ParameterManagement.SetParameterValue(command, "fileName_p", entity.FileName);

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

        public List<Element> PullProgPotrosnjaByDateAndArea(DateTime datum, string OBLAST)
        {

            List<Element> ret = new List<Element>();

            string query = "SELECT sat_p,load_p FROM prog_potrosnja WHERE datumImenaFajla_p : = datumImenaFajla_p AND oblast_p = : oblast_p ";

            using (IDbConnection connection = ConnectionSetup.GetConnection())
            {

                connection.Open();

                using (IDbCommand command = connection.CreateCommand())
                {

                    command.CommandText = query;
                    ParameterManagement.AddParameter(command, "datumImenaFajla_p", DbType.DateTime);
                    ParameterManagement.AddParameter(command, "oblast_p", DbType.String);
                    command.Prepare();
                    ParameterManagement.SetParameterValue(command, "datumImenaFajla_p", datum);
                    ParameterManagement.SetParameterValue(command, "oblast_p", OBLAST);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Element element = new Element(reader.GetDateTime(0), reader.GetString(1));
                            ret.Add(element);
                        }
                    }

                }

            }

            return ret;

        }
    }
}
