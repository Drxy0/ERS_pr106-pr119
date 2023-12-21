using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.FileReader
{
	internal class FilesDirectory
	{
		public string[] GetFiles()
		{
			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			string filesDirectory = projectDirectory + "\\xml";

			return Directory.GetFiles(filesDirectory);
		}
	}
}
