using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119
{
	public class FileDTO
	{
		public Datum Datum { get; set; }
		public List<Element> Elements { get; set; }

		public FileDTO()
		{
			Elements = new List<Element>();
			Datum = new Datum();
		}
	}
}
