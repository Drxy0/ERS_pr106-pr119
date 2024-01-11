using ERS_pr106_pr119.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.InMemoryDB
{
	public interface IinMemoryDB
	{
		public List<Element> PullOstvPotrosnjaByDateAndArea(string datum, string geoOblast);
		public List<Element> PullProgPotrosnjaByDateAndArea(string datum, string geoOblast);
	}
}
