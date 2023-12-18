using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualBasic.FileIO;

namespace ERS_pr106_pr119
{
	public class FileReader
	{
		public void Ucitaj()
		{
			List<Element> list = new List<Element>();

			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			string filesDirectory = projectDirectory + "\\xml";

			string[] files = Directory.GetFiles(filesDirectory);

			foreach (string file in files)
			{
				string fileName = Path.GetFileName(file);

				string[] extension = fileName.Split('.');
				string[] nameComponents = extension[0].Split('_');

				string tip = nameComponents[0];
				string godina = nameComponents[1];
				string mjesec = nameComponents[2];
				string dan = nameComponents[3];

				string fileExtension = Path.GetExtension(file);

				if (fileExtension == ".xml")
				{
					ProcessXML(ref list, file);
				}
				else if (fileExtension == ".csv")
				{
					ProcessCSV(ref list, file);
				}
			}
		}

		private void ProcessXML(ref List<Element> list, string file)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(file);

			foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
			{
				XmlNodeList child = node.ChildNodes;

				string sat = child.Item(0).InnerText;
				string load = child.Item(1).InnerText;
				string oblast = child.Item(2).InnerText;

				Element element = new Element(sat, load, oblast, null);
				list.Add(element);
			}
		}

		private void ProcessCSV(ref List<Element> list, string file)
		{
			using (TextFieldParser parser = new TextFieldParser(file))
			{
				parser.TextFieldType = FieldType.Delimited;
				parser.SetDelimiters(",");

				string[] headers = parser.ReadFields();

				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();

					string sat = fields[0];
					string load = fields[1];
					string oblast = fields[2];

					Element element = new Element(sat, load, oblast, null);
					list.Add(element);
				}
			}
		}
	}
}
