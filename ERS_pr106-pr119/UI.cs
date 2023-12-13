using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
	internal class UI
	{
		public UI() { }

		public void Show()
		{
			string UIstring = "\nEvidencija potrošnje električne energije\n\n";
			UIstring += "1 - Importuj fajlove\n";
			UIstring += "2 - Ispisati prognoziranu i ostvarenu potrošnju\n";
			UIstring += "q - Quit program\n";
			Console.WriteLine(UIstring);
		}

	}
}
