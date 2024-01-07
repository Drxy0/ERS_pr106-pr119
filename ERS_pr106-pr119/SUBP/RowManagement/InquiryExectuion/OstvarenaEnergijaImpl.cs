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
	public class OstvarenaEnergijaImpl : IOstvManagement
	{

		public IEnumerable<ERS_pr106_pr119.Element> FindAll()
		{
			string query = "select * from ostv_potrosnja";
			List<Element> elementList = new List<Element>();

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
							Element element = new Element(reader.GetInt32(0), reader.GetString(1),
														  reader.GetString(2), reader.GetString(3),
														  reader.GetString(4), reader.GetString(5),
														  reader.GetString(6), reader.GetString(7), reader.GetString(8));
							elementList.Add(element);
						}
					}
				}
			}

			return elementList;
		}
		public bool ExistsById(int sat_o, string oblast_o, string fileName_o, IDbConnection connection)
		{
			string query = "SELECT * from ostv_potrosnja where sat_o=:sat_o AND oblast_o =:oblast_o AND fileName_o =:fileName_o";

			using (IDbCommand command = connection.CreateCommand())
			{
				command.CommandText = query;

				ParameterManagement.AddParameter(command, "sat_o", DbType.Int32);
				ParameterManagement.AddParameter(command, "oblast_o", DbType.String);
				ParameterManagement.AddParameter(command, "fileName_o", DbType.String);

				command.Prepare();

				ParameterManagement.SetParameterValue(command, "sat_o", sat_o);
				ParameterManagement.SetParameterValue(command, "oblast_o", oblast_o);
				ParameterManagement.SetParameterValue(command, "fileName_o", fileName_o);

				return command.ExecuteScalar() == null;
			}

		}


		public int SaveRow(Element entity, IDbConnection connection)
		{

			string insertSql = "insert into ostv_potrosnja(sat_o, load_o, oblast_o, tip_o, datumUvoza_o ,satnicaUvoza_o ,file_location_o,datumImenaFajla_o,fileName_o )" +
				" values (:sat_o, :load_o, :oblast_o, :tip_o, :datumUvoza_o, :satnicaUvoza_o, :file_location_o, :datumImenaFajla_o, :fileName_o)";

			string updateSql = "update ostv_potrosnja set" +
				" sat_o =: sat_o, load_o =: load_o, oblast_o =: oblast_o," +
				" tip_o =: tip_o, datumUvoza_o = : datumUvoza_o," +
				" satnicaUvoza_o =: satnicaUvoza_o, file_location_o =: file_location_o," +
				" datumImenaFajla_o =: datumImenaFajla_o, fileName_o =: fileName_o" +
				" WHERE sat_o =: sat_o AND fileName_o =: fileName_o AND oblast_o =: oblast_o";



			using (IDbCommand command = connection.CreateCommand())
			{
				command.CommandText = ExistsById(entity.Sat, entity.Oblast, entity.FileName, connection) ? insertSql : updateSql;

				ParameterManagement.AddParameter(command, "sat_o", DbType.Int32);
				ParameterManagement.AddParameter(command, "load_o", DbType.Int32);
				ParameterManagement.AddParameter(command, "oblast_o", DbType.String);
				ParameterManagement.AddParameter(command, "tip_o", DbType.String);
				ParameterManagement.AddParameter(command, "datumUvoza_o", DbType.String);
				ParameterManagement.AddParameter(command, "satnicaUvoza_o", DbType.String);
				ParameterManagement.AddParameter(command, "file_location_o", DbType.String);
				ParameterManagement.AddParameter(command, "datumImenaFajla_o", DbType.String);
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

		public List<Element> PullOstvPotrosnjaByDateAndArea(string datum, string OBLAST)
		{

			List<Element> ret = new List<Element>();

			string query = "SELECT * FROM ostv_potrosnja WHERE datumImenaFajla_o = :datumImenaFajla_o AND oblast_o = :oblast_o ";

			using (IDbConnection connection = ConnectionSetup.GetConnection())
			{

				connection.Open();

				using (IDbCommand command = connection.CreateCommand())
				{

					command.CommandText = query;
					ParameterManagement.AddParameter(command, "datumImenaFajla_o", DbType.String);
					ParameterManagement.AddParameter(command, "oblast_o", DbType.String);
					command.Prepare();
					ParameterManagement.SetParameterValue(command, "datumImenaFajla_o", datum);
					ParameterManagement.SetParameterValue(command, "oblast_o", OBLAST);
					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							Element element = new Element(reader.GetInt32(0), reader.GetString(1),
														  reader.GetString(2), reader.GetString(3),
														  reader.GetString(4), reader.GetString(5),
														  reader.GetString(6), reader.GetString(7), reader.GetString(8));
							ret.Add(element);
						}
					}

				}

			}

			return ret;

		}


	}
}
