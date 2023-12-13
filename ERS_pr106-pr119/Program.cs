<<<<<<< HEAD
﻿using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
=======
﻿namespace ERS_pr106_pr119
>>>>>>> 66cb6c0f1cab673412df04c69d62d204114f5acb
{
	internal class Program
	{
		static void Main()
		{
<<<<<<< HEAD
			UI ui = new UI();
			string? s;
			do {
				ui.Show();
				s = Console.ReadLine();

				switch (s)
				{
					case "1":
						Ucitaj();
						break;
				}
=======
			string UI = "\nEvidencija potrošnje električne energije\n\n";
			UI += "1 - Importuj fajlove\n";
			UI += "2 - Ispisati prognoziranu i ostvarenu potrošnju\n";
			UI += "q - Quit program\n";
			string s;
			do {
				Console.WriteLine(UI);
				s = Console.ReadLine();
>>>>>>> 66cb6c0f1cab673412df04c69d62d204114f5acb
			}
			while (s != "q");

		}
<<<<<<< HEAD

		static void Ucitaj()
		{
			XmlDocument xml = new XmlDocument();

			string workingDirectory = Environment.CurrentDirectory;

			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

			string projectDirectory2 = projectDirectory + "\\xml\\ostv_2020_05_07.xml";

			Console.WriteLine(projectDirectory2);
			xml.Load(projectDirectory2);

		}
=======
>>>>>>> 66cb6c0f1cab673412df04c69d62d204114f5acb
	}
}
