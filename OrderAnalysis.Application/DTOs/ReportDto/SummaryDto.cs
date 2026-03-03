using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.ReportDto
{
	public class SummaryDto
	{
		public int ToplamSiparisSayisi { get; set; }
		public int ToplamUrunAdedi { get; set; }
		public decimal ToplamCiro { get; set; }
		public decimal ToplamNetKar { get; set; }
	}
}
