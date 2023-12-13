using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ERS_pr106_pr119
{
	internal class Reader
	{
		public void Ucitaj()
		{
			XmlDocument xmlDoc = new XmlDocument();

			string workingDirectory = Environment.CurrentDirectory;
			string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.FullName;
			string filesDirectory = projectDirectory + "\\xml";

			string[] files = Directory.GetFiles(filesDirectory);

			foreach (string file in files)
			{
				if (Path.GetExtension(file) == ".xml")
				{
					xmlDoc.Load(file);
					foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
					{
						XmlNodeList child = node.ChildNodes;
						string sat = child.Item(0).InnerText;
						string load = child.Item(1).InnerText;
						string oblast = child.Item(2).InnerText;
					}
				}
			}
		}
	}
}
