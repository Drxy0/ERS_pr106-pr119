using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.SUBP.RowManagement;
using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;

namespace ERS_pr106_pr119.FileReader
{
    internal class ReaderXML : FileReader
	{
        private static readonly IPodrucije podrucije = new PodrucijeImpl();
        public List<FileDTO> Ucitaj()
		{
			List<FileDTO> fileDTOs = new();
			string[] files = new FilesDirectory().GetFiles();

			foreach (string file in files)
			{
				string fileName = Path.GetFileName(file);

				string[] extension = fileName.Split('.');
				string[] nameComponents = extension[0].Split('_');

				string tip = nameComponents[0];
				string godina = nameComponents[1];
				string mjesec = nameComponents[2];
				string dan = nameComponents[3];

				FileDTO fileDTO = new FileDTO();

				fileDTO.Datum = new Datum(dan, mjesec, godina);

				string fileExtension = Path.GetExtension(file);

				if (fileExtension == ".xml")
				{
					List<Element> list = new List<Element>();
					ProcessXML(ref list, file, tip);
					fileDTO.Elements = list;
					fileDTOs.Add(fileDTO);
				}
			}
			return fileDTOs;
		}

		private void ProcessXML(ref List<Element> list, string file, string tip)
		{
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(file);

			foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
			{
				XmlNodeList child = node.ChildNodes;

				string sat = child.Item(0).InnerText;
				string load = child.Item(1).InnerText;
				string oblast = child.Item(2).InnerText;

                podrucije.InsertRowFromPotrosnja(oblast);  //Punjenje tipa entiteta novim oblastima iz XML fajla

                Element element = new Element(sat, load, oblast, tip);
				list.Add(element);
			}
		}
	}
}
