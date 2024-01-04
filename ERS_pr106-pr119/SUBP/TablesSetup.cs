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
		private static int ExecuteNonQuery(string sql)
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

		public static void TableCreations()
		{
			string sql0 = "CREATE TABLE podrucije (" +
				"oblast varchar(4)," +
				"nazivP varchar(16)," +
				"CONSTRAINT podrucije_PK PRIMARY KEY (oblast)" +
				")";

			string sql1 = "CREATE TABLE prog_potrosnja (" +
				"sat_p integer," +
				"load_p varchar(10)," +
				"oblast_p varchar(4)," +
				"tip_p varchar(5)," +
				"datumUvoza_p varchar(10)," +
				"satnicaUvoza_p varchar(10)," +
				"file_location_p varchar(255)," +
				"datumImenaFajla_p varchar(20)," +
				"fileName_p varchar(50)," +
				"CONSTRAINT prog_potrosnja_PK PRIMARY KEY (sat_p,fileName_p,oblast_p)" +
				")";

			string sql2 = "CREATE TABLE ostv_potrosnja (" +
				"sat_o integer," +
				"load_o varchar(10)," +
				"oblast_o varchar(4)," +
				"tip_o varchar(5)," +
				"datumUvoza_o varchar(10)," +
				"satnicaUvoza_o varchar(10)," +
				"file_location_o varchar(255)," +
				"datumImenaFajla_o varchar(20)," +
				"fileName_o varchar(50)," +
				"CONSTRAINT ostv_potrosnja_PK PRIMARY KEY (sat_o,fileName_o,oblast_o)" +
				")";

			TablesSetup.CreateTable(sql0);
			TablesSetup.CreateTable(sql1);
			TablesSetup.CreateTable(sql2);
		}

	}
}