namespace ERS_pr106_pr119
{
	internal class Program
	{
		static void Main()
		{
			string UI = "\nEvidencija potrošnje električne energije\n\n";
			UI += "1 - Importuj fajlove\n";
			UI += "2 - Ispisati prognoziranu i ostvarenu potrošnju\n";
			UI += "q - Quit program\n";
			string s;
			do {
				Console.WriteLine(UI);
				s = Console.ReadLine();
			}
			while (s != "q");

		}
	}
}
