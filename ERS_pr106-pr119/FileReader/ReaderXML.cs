﻿using System;
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
		private static readonly AuditTableImpl auditTable = new AuditTableImpl();
		private static readonly IPodrucje podrucje = new PodrucjeImpl();
		public void Ucitaj()
		{
			string[] files = new FilesDirectory().GetFiles();

			foreach (string file in files)
			{
				List<Element> listOstvarena = new List<Element>();
				List<Element> listPrognozirana = new List<Element>();

				string fileName = Path.GetFileName(file);
				
				string[] extension = fileName.Split('.');
				string[] nameComponents = extension[0].Split('_');

				string tip = nameComponents[0];
				string godina = nameComponents[1];
				string mjesec = nameComponents[2];
				string dan = nameComponents[3];


				string datumUvoza = string.Empty;
				string satnicaUvoza = string.Empty;
				GetCurrentTime(ref datumUvoza, ref satnicaUvoza);
				string datumImenaFajla = dan + "." + mjesec + "." + godina + ".";
				
				string fileExtension = Path.GetExtension(file);
				if (fileExtension == ".xml")
				{
					ProcessXML(ref listOstvarena, ref listPrognozirana, file, tip, datumUvoza, satnicaUvoza, fileName, datumImenaFajla);
				}

				prognozaImpl.InsertRows(listPrognozirana);
				ostvarenaImpl.InsertRows(listOstvarena);

			}
		}

		private void ProcessXML(ref List<Element> listOstvarena,
								ref List<Element> listPrognozirana,
								string file,
								string tip,
								string datumUvoza,
								string satnicaUvoza,
								string datumImenaFajla,
								string fileName)
		{
			XmlDocument xmlDoc = new XmlDocument();
			try
			{
				xmlDoc.Load(file);          //absolute address to file
			}
			catch (Exception e)
			{
				Console.WriteLine("Error trying to read the file");
				Console.WriteLine(e.Message); return;
			}

			List<Element> listOstvarenaTest = new List<Element>();
			List<Element> listPrognoziranaTest = new List<Element>();


			foreach (XmlNode node in xmlDoc.DocumentElement.ChildNodes)
			{
				XmlNodeList child = node.ChildNodes;

				if (child == null)
				{
					Console.WriteLine("File format invalid!");
					continue;
				}

				string sat = child.Item(0).InnerText;
				string load = child.Item(1).InnerText;
				string oblast = child.Item(2).InnerText;

				podrucje.InsertRowFromPotrosnja(oblast);  //Punjenje tipa entiteta novim oblastima iz XML fajla

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
					listOstvarenaTest.Add(element);
				}
				else if (element.Tip == "prog")
				{
					listPrognoziranaTest.Add(element);
				}
			}

			bool invalidFileOccured = CheckFileValidity(listOstvarenaTest, listPrognoziranaTest, file);

			if (invalidFileOccured == false)
			{
				listOstvarena = listOstvarenaTest;
				listPrognozirana = listPrognoziranaTest;
			}
		}


		private bool CheckFileValidity(List<Element> listOstvarenaTest, List<Element> listPrognoziranaTest, string file)
		{
			var uniqueOblastValuesOstvarena = listOstvarenaTest.Select(e => e.Oblast).Distinct().ToList();
			foreach (string value in uniqueOblastValuesOstvarena)
			{
				var occurrenceCountOstvarena = listOstvarenaTest.Count(e => e.Oblast == value);
				if (occurrenceCountOstvarena != 23 && occurrenceCountOstvarena != 24 && occurrenceCountOstvarena != 25)
				{
					DateTime vrijemeUcitavanja = DateTime.Now;
					string imeFajla = listOstvarenaTest[0].FileName;
					string lokacijaFajla = file;
					int brojRedova = listOstvarenaTest.Count;

					InvalidFile invalidFile = new InvalidFile(vrijemeUcitavanja.ToString(),
															  imeFajla,
															  lokacijaFajla,
															  brojRedova);

					auditTable.InsertSingleRow(invalidFile);
					Console.WriteLine("File " + imeFajla + " je nevalidan!");
					return true;
				}
			}

			var uniqueOblastValuesPrognozirana = listPrognoziranaTest.Select(e => e.Oblast).Distinct().ToList();
			foreach (string value in uniqueOblastValuesPrognozirana)
			{
				var occurrenceCountPrognozirana = listPrognoziranaTest.Count(e => e.Oblast == value);
				if (occurrenceCountPrognozirana != 23 && occurrenceCountPrognozirana != 24 && occurrenceCountPrognozirana != 25)
				{
					DateTime vrijemeUcitavanja = DateTime.Now;
					string imeFajla = listPrognoziranaTest[0].FileName;
					string lokacijaFajla = file;
					int brojRedova = listPrognoziranaTest.Count;

					InvalidFile invalidFile = new InvalidFile(vrijemeUcitavanja.ToString(),
															  imeFajla,
															  lokacijaFajla,
															  brojRedova);

					auditTable.InsertSingleRow(invalidFile);
					Console.WriteLine("File " + imeFajla + " je nevalidan!");
					return true;
				}
			}

			return false;
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
