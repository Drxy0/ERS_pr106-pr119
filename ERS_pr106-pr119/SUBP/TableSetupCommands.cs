using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.SUBP
{
    internal class TableSetupCommands
    {
        public static void TableCreations()
        {
            string sql0 = "CREATE TABLE podrucje (" +
                "oblast varchar(4)," +
                "nazivP varchar(16)," +
                "CONSTRAINT podrucje_PK PRIMARY KEY (oblast)" +
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

            string sql_audit = "CREATE TABLE audit_table (" +
                "vrijeme_ucitavanja varchar(50), " +
                "ime_fajla varchar(50), " +
                "lokacija_fajla varchar(255), " +
                "broj_redova integer " +
                ")";


            string sql0Drop = "drop table podrucje";
            string sql1Drop = "drop table prog_potrosnja";
            string sql2Drop = "drop table ostv_potrosnja";
            string sqlAuditDrop = "drop table audit_table";

            TablesSetup.ExecuteNonQuery(sql0Drop);
            TablesSetup.ExecuteNonQuery(sql1Drop);
            TablesSetup.ExecuteNonQuery(sql2Drop);
            TablesSetup.ExecuteNonQuery(sqlAuditDrop);

            TablesSetup.CreateTable(sql0);
            TablesSetup.CreateTable(sql1);
            TablesSetup.CreateTable(sql2);
            TablesSetup.CreateTable(sql_audit);
        }
    }
}
