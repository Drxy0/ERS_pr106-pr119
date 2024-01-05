using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.FileReader
{
	public class FilesDirectory
	{
		public string[] GetFiles()
		{
			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			string filesDirectory = projectDirectory + "\\xml";

			return Directory.GetFiles(filesDirectory);
		}
		public string ExportDirectory()
		{
			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			string filesDirectory = projectDirectory + "\\Exported Tables\\";

			return filesDirectory;
		}
	}
}
