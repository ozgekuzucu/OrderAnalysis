using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.ReportDto
{
	public class AnomalyReportDto
	{
		public string Urun { get; set; }
		public decimal OrtalamaSatisFiyat { get; set; }
		public decimal MevcutSatisFiyat { get; set; }
		public decimal SapmaYuzde { get; set; }
	}
}
