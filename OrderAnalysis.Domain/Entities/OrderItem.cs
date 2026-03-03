using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Domain.Entities
{
	public class OrderItem
	{
		public string Urun { get; set; }
		public decimal AlisFiyat { get; set; }
		public decimal SatisFiyat { get; set; }
		public decimal KomisyonOrani { get; set; }
		public decimal KargoBedeli { get; set; }
		public int Adet { get; set; }
	}
}
