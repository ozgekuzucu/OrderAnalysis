using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.ReportDto
{
	public class LossReportDto
	{
		public string Urun { get; set; }
		public decimal ToplamZarar { get; set; }
	}
}
