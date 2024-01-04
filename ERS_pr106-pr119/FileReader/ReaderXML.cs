using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.SUBP.RowManagement;
using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ERS_pr106_pr119.FileReader
{
	public class ReaderXML : IFileReader
	{
		private static readonly PrognozaEnergijeImpl prognozaImpl = new PrognozaEnergijeImpl();
		private static readonly OstvarenaEnergijaImpl ostvarenaImpl = new OstvarenaEnergijaImpl();
		private static readonly IPodrucije podrucije = new PodrucijeImpl();
		public List<FileDTO> Ucitaj()
		{
			List<FileDTO> fileDTOs = new();
			string[] files = new FilesDirectory().GetFiles();

			List<Element> listOstvarena = new List<Element>();
			List<Element> listPrognozirana = new List<Element>();

			foreach (string file in files)
			{
				string fileName = Path.GetFileName(file);
				
				string[] extension = fileName.Split('.');
				string[] nameComponents = extension[0].Split('_');

				string tip = nameComponents[0]; //tip
				string godina = nameComponents[1];
				string mjesec = nameComponents[2];
				string dan = nameComponents[3];


				string datumUvoza = string.Empty;
				string satnicaUvoza = string.Empty;
				GetCurrentTime(ref datumUvoza, ref satnicaUvoza);

				FileDTO fileDTO = new FileDTO();

				string datumImenaFajla = dan + "." + mjesec + "." + godina + ".";
				fileDTO.Datum = new Datum(dan, mjesec, godina);


				string fileExtension = Path.GetExtension(file);

				if (fileExtension == ".xml")
				{
					ProcessXML(ref listOstvarena, ref listPrognozirana, file, tip, datumUvoza, satnicaUvoza, fileName, datumImenaFajla);
					fileDTO.Elements = listOstvarena;
					fileDTOs.Add(fileDTO);
				}

			}

	
				prognozaImpl.InsertRows(listPrognozirana);
			//	ostvarenaImpl.InsertRows(listOstvarena);

			return fileDTOs;
		}

		private void ProcessXML(ref List<Element> listOstvarena,
								ref List<Element> listPrognozirana,
								string file,
								string tip,
								string datumUvoza,
								string satnicaUvoza,
								string fileName,
								string datumImenaFajla)
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

				Element element = new Element(Int32.Parse(sat),
											 load,
											 oblast,
											 tip,
											 datumUvoza,
											 satnicaUvoza,
											 file,
											 fileName,
											 datumImenaFajla);
				if (element.Tip == "ostv")
				{
					listOstvarena.Add(element);
				}
				else if (element.Tip == "prog")
				{
					listPrognozirana.Add(element);
				}

			}
		}

		private void GetCurrentTime(ref string datumUvoza, ref string satnicaUvoza)
		{
			DateTime dateTime = DateTime.Now;
			string[] dateTimeComponents = dateTime.ToString().Split(' ');
			datumUvoza = dateTimeComponents[0];
			satnicaUvoza = dateTimeComponents[1];
		}
	}
}
