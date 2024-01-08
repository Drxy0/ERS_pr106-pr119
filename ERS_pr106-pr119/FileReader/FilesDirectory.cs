using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.FileReader
{
	public class FilesDirectory
	{
		public string[]? GetFiles(string folderName)
		{
			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			string filesDirectory = projectDirectory + "\\" + folderName;

			try
			{
				return Directory.GetFiles(filesDirectory);
			}
			catch (Exception ex)
			{ 
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		public string ExportDirectory()
		{
			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			string filesDirectory = projectDirectory + "\\Exported Tables\\";

			Console.WriteLine(filesDirectory);

			return filesDirectory;
		}
	}
}
