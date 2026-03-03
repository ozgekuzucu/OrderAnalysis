using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.ReportDto
{
	public class PlatformReportDto
	{
		public string Platform { get; set; }
		public decimal ToplamCiro { get; set; }
		public decimal ToplamNetKar { get; set; }
		public decimal KarMarjiYuzde { get; set; }
	}
}
