using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.Export;
using ERS_pr106_pr119.FileReader;
using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
    internal class Program
	{
		static void Main()
		{
			
			UI ui = new UI();
			ReaderXML xmlReader = new ReaderXML();
			List<FileDTO> files = new List<FileDTO>();
			ExportDTO exportTable = new ExportDTO();
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();

				switch (s)
				{
					case "1":
						files = xmlReader.Ucitaj();
						break;
					case "2":
						exportTable = ui.IspisOpcije(files);
						if (ui.ExportUpit() == true)
							new ExportToCSV().Export(exportTable);
						break;
					case "3":
						//TODO
						break;
				}
			}
			while (s != "q");
			
        TableCreations();
        }

        public static void TableCreations()
        {
			string sql_test = "create table test(sat_p int)";
            string sql1 = "create table prog_potrosnja(sat_p int, load_p int, oblast_p varchar(4),tip_p varchar(5),datum_p date,vreme_p date,file_location_p varchar(255),datumImenaFajla_p date,fileName_p varchar(50))," +
                "CONSTRAINT prog_potrosnja_PK PRIMARY KEY (sat_p,fileName_p)";

            string sql2 = "create table ostv_potrosnja(sat_o int, load_o int, oblast_o varchar(4),tip_o varchar(5),datum_o date,vreme_o date,file_location_o varchar(255),datumImenaFajla_o date,fileName_o varchar(50))," +
                "CONSTRAINT ostv_potrosnja_PK PRIMARY KEY (sat_o,fileName_o)";

			SUBP.TablesSetup.CreateTable(sql_test);
			//SUBP.TablesSetup.CreateTable(sql2);

			string sql3 = "create table podrucije(oblast varchar(4),nazivP varchar(16),velicinaP int, CONSTRAINT podrucije_PK PRINARY KEY (oblast))";


        }

    }
}
