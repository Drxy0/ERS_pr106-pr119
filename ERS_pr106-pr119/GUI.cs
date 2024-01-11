using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
	public class GUI
	{
		public void Show()
		{
			string UIstring = "\nEvidencija potrošnje električne energije\n\n" +
								"1 - Importuj fajlove\n" +
								"2 - Ispisati prognoziranu i ostvarenu potrošnju\n" +
								"q - Quit program\n";
			Console.WriteLine(UIstring);
		}
	}
}
