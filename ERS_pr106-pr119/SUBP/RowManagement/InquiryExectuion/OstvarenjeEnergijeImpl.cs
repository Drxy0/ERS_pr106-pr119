using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion
{
    public class OstvarenjeEnergijeImpl : ostvManagement
    {

        public IEnumerable<ERS_pr106_pr119.Element>FindAll()
        {
            throw new NotImplementedException();
        }
        public bool ExistsById(int sat, string fileName, IDbConnection connection)
        {
            string query = "select * from prog_potrosnja where sat_o=:sat_o AND fileName_o=:fileName_o";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterManagement.AddParameter(command, "sat_o", DbType.Int32);
                ParameterManagement.AddParameter(command, "fileName_o", DbType.String);

                ParameterManagement.SetParameterValue(command, "sat_o", sat);
                ParameterManagement.SetParameterValue(command, "tip_o", fileName);

                return command.ExecuteScalar() != null;
            }
            
        }


        private int SaveRow(Element entity, IDbConnection connection)
        {

            string insertSql = "insert into ostv_potrosnja(sat_o, load_o, oblast_o, tip_o, datumUvoza_o ,satnicaUvoza_o ,file_location_o,datumImenaFajla_o,fileName_o )" +
                " values (:sat_o, :load_o, :oblast_o, :tip_o, :datumUvoza_o, :satnicaUvoza_o, :file_location_o, :datumImenaFajla_o, :fileName_o)";

            string updateSql = "update ostv_potrosnja set" +
                " sat_o=:sat_o, load_o = :load_o, oblast_o = :oblast_o, tip_o = :tip_o,datumUvoza_o = : datumUvoza_o,satnicaUvoza_o = : satnicaUvoza_o," +
                " file_location_o = :file_location_o,datumImenaFajla_o=:datumImenaFajla_o,fileName_o:=fileName_o ";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(entity.Sat, entity.FileName, connection) ? updateSql : insertSql;
                ParameterManagement.AddParameter(command, "sat_o", DbType.Int32);
                ParameterManagement.AddParameter(command, "load_o", DbType.Int32);
                ParameterManagement.AddParameter(command, "oblast_o", DbType.String);
                ParameterManagement.AddParameter(command, "tip_o", DbType.String);
                ParameterManagement.AddParameter(command, "datumUvoza_o", DbType.DateTime);
                ParameterManagement.AddParameter(command, "satnicaUvoza_o", DbType.DateTime);
                ParameterManagement.AddParameter(command, "file_location_o", DbType.String);
                ParameterManagement.AddParameter(command, "datumImenaFajla_o", DbType.DateTime);
                ParameterManagement.AddParameter(command, "fileName_o", DbType.String);

                ParameterManagement.SetParameterValue(command, "sat_o", entity.Sat);
                ParameterManagement.SetParameterValue(command, "load_o", entity.Load);
                ParameterManagement.SetParameterValue(command, "oblast_o", entity.Oblast);
                ParameterManagement.SetParameterValue(command, "tip_o", entity.Tip);
                ParameterManagement.SetParameterValue(command, "datumUvoza_o", entity.DatumUvoza);
                ParameterManagement.SetParameterValue(command, "satnicaUvoza_o", entity.SatnicaUvoza);
                ParameterManagement.SetParameterValue(command, "file_location_o", entity.File_location);
                ParameterManagement.SetParameterValue(command, "datumImenaFajla_o", entity.DatumImenaFajla);
                ParameterManagement.SetParameterValue(command, "fileName_o", entity.FileName);

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

        public List<Element> PullOstvPotrosnjaByDateAndArea(DateTime datum,string OBLAST) {

            List<Element> ret = new List<Element>();

            string query = "SELECT sat_o,load_o FROM ostv_potrosnja WHERE datumImenaFajla_o : = datumImenaFajla_o AND oblast_o = : oblast_o ";

            using (IDbConnection connection = ConnectionSetup.GetConnection())
            {

                connection.Open();

                using (IDbCommand command = connection.CreateCommand()) {

                    command.CommandText = query;
                    ParameterManagement.AddParameter(command, "datumImenaFajla_o", DbType.DateTime);
                    ParameterManagement.AddParameter(command, "oblast_o", DbType.String);
                    command.Prepare();
                    ParameterManagement.SetParameterValue(command, "datumImenaFajla_o", datum);
                    ParameterManagement.SetParameterValue(command, "oblast_o", OBLAST);
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
