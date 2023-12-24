using ERS_pr106_pr119.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERS_pr106_pr119.Export
{
	internal interface ExportTable
	{
		void Export(ExportDTO exportDTO);
	}
}
