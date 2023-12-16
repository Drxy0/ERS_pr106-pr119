﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualBasic.FileIO;

namespace ERS_pr106_pr119
{
	internal class Reader
	{
		public struct Element
		{
			public string sat;
			public string load;
			public string oblast;

			public Element(string sat, string load, string oblast)
			{
				this.sat = sat;
				this.load = load;
				this.oblast = oblast;
			}
		}
		public void Ucitaj()
		{
			XmlDocument xmlDoc = new XmlDocument();
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

				if (Path.GetExtension(file) == ".xml")
				{
					xmlDoc.Load(file);

					foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
					{
						XmlNodeList child = node.ChildNodes;
						string sat = child.Item(0).InnerText;
						string load = child.Item(1).InnerText;
						string oblast = child.Item(2).InnerText;

						Element element = new Element(sat, load, oblast);
						list.Add(element);
					}
				}
				else if (Path.GetExtension(file) == ".csv")
				{
					using(TextFieldParser parser = new TextFieldParser(file))
					{
						parser.TextFieldType = FieldType.Delimited;
						parser.SetDelimiters(",");
						string[] headers = parser.ReadFields();
						while(!parser.EndOfData)
						{
							string[] fields = parser.ReadFields();

							string sat = fields[0];
							string load = fields[1];
							string oblast = fields[2];
							Element element = new Element(sat, load, oblast);
							list.Add(element);
						}
						Console.WriteLine(list.Count);
					}
				}
			}

		}
	}
}
