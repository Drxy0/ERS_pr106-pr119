using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERS_pr106_pr119.Model;

namespace ERS_pr106_pr119.DTO
{
    public class InMemoryDataBaseDTO
	{
		public virtual List<Element> OstvarenaPotrosnja { get; set; }
		public virtual List<Element> PrognoziranaPotrosnja { get; set; }

		public InMemoryDataBaseDTO()
		{
			OstvarenaPotrosnja = new List<Element>();
			PrognoziranaPotrosnja = new List<Element>();
		}
	}
}
