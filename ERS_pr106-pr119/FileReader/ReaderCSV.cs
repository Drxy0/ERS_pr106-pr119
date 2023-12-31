﻿using ERS_pr106_pr119.DTO;
using ERS_pr106_pr119.SUBP.RowManagement;
using ERS_pr106_pr119.SUBP.RowManagement.InquiryExectuion;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ERS_pr106_pr119.FileReader
{
    internal class ReaderCSV : FileReader
	{

        private static readonly IPodrucije podrucije = new PodrucijeImpl();
        public List<FileDTO> Ucitaj()
		{
			List<FileDTO> fileDTOs = new();
			List<Element> list = new List<Element>();
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

				fileDTO.Datum.Dan = dan;
				fileDTO.Datum.Mjesec = mjesec;
				fileDTO.Datum.Godina = godina;

				string fileExtension = Path.GetExtension(file);

				if (fileExtension == ".csv")
				{
					ProcessCSV(ref list, file, tip);
					fileDTO.Elements = list;
					list.Clear();
				}
				fileDTOs.Add(fileDTO); 
			}
			return fileDTOs;
		}

		private void ProcessCSV(ref List<Element> list, string file, string tip)
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

					podrucije.InsertRowFromPotrosnja(oblast); //Punjenje tipa entiteta novim oblastima iz fajla

                    Element element = new Element(sat, load, oblast, tip);
					list.Add(element);
				}
			}
		}
	}
}
