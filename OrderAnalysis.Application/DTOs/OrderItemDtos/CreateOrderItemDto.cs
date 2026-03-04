using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.OrderItemDtos
{
	public class CreateOrderItemDto
	{
		[Required(ErrorMessage = "Ürün adı zorunludur.")]
		public string Urun { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "Alış fiyatı 0' dan büyük olmalıdır")]
		public decimal AlisFiyat { get; set; }

		[Range(0.01, double.MaxValue, ErrorMessage = "Satış fiyatı 0' dan büyük olmalıdır")]
		public decimal SatisFiyat { get; set; }

		[Range(0,100,ErrorMessage ="Komisyon oranı 0-100 arasında olmalıdır.")]
		public decimal KomisyonOrani { get; set; }

		[Range(0,double.MaxValue,ErrorMessage ="Kargo bedeli 0 ya da 0'dan büyük olmalıdır.")]
		public decimal KargoBedeli { get; set; }

		[Range(1,int.MaxValue,ErrorMessage ="En az 1 adet ürün eklenmelidir")]
		public int Adet { get; set; }
	}
}
