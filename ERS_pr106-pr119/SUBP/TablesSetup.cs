using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Oracle.ManagedDataAccess.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERS_pr106_pr119.SUBP
{
	public class TablesSetup
	{
		public static int ExecuteNonQuery(string sql)
		{
			using (IDbConnection connection = ConnectionSetup.GetConnection())
			{
				try
				{
					connection.Open();
					using (IDbCommand command = connection.CreateCommand())
					{
						command.CommandText = sql;
						int rowsAffected = command.ExecuteNonQuery();

						return rowsAffected;
					}
				}
				catch (DbException ex)
				{
					Console.WriteLine(ex.Message);
					return -2;
				}
			}
		}

		public static void CreateTable(string sql)
		{

			int rowsAffected = ExecuteNonQuery(sql);
			if (rowsAffected != -2)
			{
				Console.WriteLine("Table successfully created!");
			}
			else
			{
				Console.WriteLine("Exception occurred!");
			}
		}

	}
}