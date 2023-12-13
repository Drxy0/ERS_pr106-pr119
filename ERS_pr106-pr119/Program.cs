using System.IO;
using System.Xml;

namespace ERS_pr106_pr119
{
	internal class Program
	{
		static void Main()
		{
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
			}
			while (s != "q");

		}

		static void Ucitaj()
		{
			XmlDocument xml = new XmlDocument();

			string workingDirectory = Environment.CurrentDirectory;

			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;

			string projectDirectory2 = projectDirectory + "\\xml\\ostv_2020_05_07.xml";

			Console.WriteLine(projectDirectory2);
			xml.Load(projectDirectory2);

		}
	}
}
