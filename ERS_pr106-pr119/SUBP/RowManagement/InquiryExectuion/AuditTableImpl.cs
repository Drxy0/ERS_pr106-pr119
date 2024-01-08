using ERS_pr106_pr119.FileReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion
{
	public class AuditTableImpl
	{
		public void InsertSingleRow(InvalidFile invalidFile)
		{
			using (IDbConnection connection = ConnectionSetup.GetConnection())
			{
				connection.Open();
				IDbTransaction transaction = connection.BeginTransaction();
				try
				{
					SaveRow(invalidFile, connection);
					transaction.Commit();
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					Console.WriteLine($"Error: {ex.Message}");
				}
			}
		}

		private int SaveRow(InvalidFile invalidFile, IDbConnection connection)
		{
			string insertSql = "insert into audit_table" +
										"(vrijeme_ucitavanja, ime_fajla, lokacija_fajla, broj_redova)" +
								"values (:vrijeme_ucitavanja, :ime_fajla, :lokacija_fajla, :broj_redova)";
			
			using (IDbCommand command = connection.CreateCommand())
			{
				command.CommandText = insertSql;

				ParameterManagement.AddParameter(command, "vrijeme_ucitavanja", DbType.String);
				ParameterManagement.AddParameter(command, "ime_fajla", DbType.String);
				ParameterManagement.AddParameter(command, "lokacija_fajla", DbType.String);
				ParameterManagement.AddParameter(command, "broj_redova", DbType.Int32);

				command.Prepare();

				ParameterManagement.SetParameterValue(command, "vrijeme_ucitavanja", invalidFile.VrijemeUcitavanja);
				ParameterManagement.SetParameterValue(command, "ime_fajla", invalidFile.ImeFajla);
				ParameterManagement.SetParameterValue(command, "lokacija_fajla", invalidFile.LokacijaFajla);
				ParameterManagement.SetParameterValue(command, "broj_redova", invalidFile.BrojRedova);

				return command.ExecuteNonQuery();
			}
		}

	}
}
