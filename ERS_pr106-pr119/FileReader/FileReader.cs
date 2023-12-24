using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.FileReader
{
	internal interface FileReader
	{
		public List<FileDTO> Ucitaj();
	}
}
