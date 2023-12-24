﻿using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
	internal class Program
	{
		static void Main()
		{
			
			UI ui = new UI();
			FileReader reader = new FileReader();
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();

				switch (s)
				{
					case "1":
						reader.Ucitaj();
						break;
				}
			}
			while (s != "q");
			
        TableCreations();
        }

        public static void TableCreations()
        {

            string sql1 = "create table prog_potrosnja(sat_p int, load_p int, oblast_p varchar(4),tip_p varchar(5),time_p long,file_location_p varchar(255)," +
                "CONSTRAINT prog_potrosnja_PK PRIMARY KEY (sat_p,tip_p,time_p)";

            string sql2 = "create table ostv_potrosnja(sat_o int, load_o int, oblast_o varchar(4),tip_o varchar(5),time_o long,file_location_o varchar(255)," +
                "CONSTRAINT prog_potrosnja_PK PRIMARY KEY (sat_o,tip_o,time_o)";

			SUBP.TablesSetup.CreateTable(sql1);
            SUBP.TablesSetup.CreateTable(sql2);

        }

    }
}
