using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.ReportDto
{
	public class TrendReportDto
	{
		public string Tarih { get; set; }
		public decimal GunlukCiro { get; set; }
		public decimal GunlukNetKar  { get; set; }
		public decimal OncekiGuneGoreDegisimYuzde { get; set; }
	}
}
