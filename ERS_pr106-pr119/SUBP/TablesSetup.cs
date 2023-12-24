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
        public static void TableCreations()
        {

            string sql1 = "create table prog_potrosnja(sat_p int, load_p int, oblast_p varchar(4),ime_p varchar(5)),time_p TIMESTAMP,file_location_p varchar(255),PRIMARY KEY (sat_p,ime_p))";
            string sql2 = "create table ostv_potrosnja(sat_o int, load_o int, oblast_o varchar(4),ime_o varchar(5)),time_o TIMESTAMP,file_location_o varchar(255),PRIMARY KEY (sat_o,ime_o))";

            CreateTable(sql1);
            CreateTable(sql2);

        }

        private static int ExecuteNonQuery(string sql)
        {
            try
            {

                using (IDbConnection connection = ConnectionSetup.GetConnection())
                {
                    connection.Open();

                    using (IDbCommand command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        int rowsAffected = command.ExecuteNonQuery();

                        return rowsAffected;
                    }
                }
            }
            catch (DbException ex)
            {
                Console.WriteLine(ex.Message);
                return -2;
            }
        }

        private static void CreateTable(string sql)
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

        private static void InsertRows(List<Element> list)
        {
            foreach (Element element in list)
            {

                string sat = element.Sat;
                string load = element.Load;
                string oblast = element.Oblast;
                string ime = element.Tip;
                string time = "";
                string file_location = "";

                if (ime.Equals("prog"))
                    {
                        string sql = string.Format(
                        "insert into p_potrosnja(sat, load, oblast , ime) values ({0}, {1}, {2}, {3}))",
                        string.IsNullOrEmpty(sat) ? "null" : sat,
                        string.IsNullOrEmpty(load) ? "null" : load,
                        string.IsNullOrEmpty(oblast) ? "null" : oblast,
                        string.IsNullOrEmpty(ime) ? "null" : ime);

                        int rowsAffected = ExecuteNonQuery(sql);
                        if (rowsAffected != -2)
                        {
                            Console.WriteLine("Row sucessfully inserted");
                        }
                        else
                        {
                            Console.WriteLine("Exception occurred!");
                        }

                }
                else if (ime.Equals("ostv"))
                    {
                        string sql = string.Format(
                        "insert into p_potrosnja(sat, load, oblast , ime, time , file_location) values ({0}, {1}, {2}, {3},to_date({4}, 'dd.MM.yyyy.'),{5}))",
                        string.IsNullOrEmpty(sat) ? "null" : sat,
                        string.IsNullOrEmpty(load) ? "null" : load,
                        string.IsNullOrEmpty(oblast) ? "null" : oblast,
                        string.IsNullOrEmpty(ime) ? "null" : ime,
                        string.IsNullOrEmpty(time) ? "null" : string.Format("'{0}'", time),
                        string.IsNullOrEmpty(file_location) ? "null" : file_location);

                        int rowsAffected = ExecuteNonQuery(sql);
                        if (rowsAffected != -2)
                        {
                            Console.WriteLine("Row sucessfully inserted");
                        }
                        else
                        {
                            Console.WriteLine("Exception occurred!");
                        }
                }

            }
        }

    }
}
