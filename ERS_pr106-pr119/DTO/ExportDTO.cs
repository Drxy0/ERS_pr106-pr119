using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERS_pr106_pr119.Model;

namespace ERS_pr106_pr119.DTO
{
    public class ExportDTO
    {
        public List<Element>? OstvarenaP { get; set; }
		public List<Element>? PrognoziranaP { get; set; }
        public List<string> Odstupanja {  get; set; }
        public Datum Datum { get; set; }
        public string? Oblast {  get; set; }

    }
}
