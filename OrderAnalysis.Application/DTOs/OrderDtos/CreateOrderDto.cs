using OrderAnalysis.Application.DTOs.OrderItemDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.OrderDtos
{
	public class CreateOrderDto
	{
		[Required(ErrorMessage = "Platform zorunludur.")]
		public string Platform { get; set; }

		[Required(ErrorMessage = "Tarih zorunludur.")]
		public DateTime Tarih { get; set; }

		[Required(ErrorMessage = "En az bir ürün girilmelidir.")]
		[MinLength(1, ErrorMessage = "En az bir ürün girilmelidir.")]
		public List<CreateOrderItemDto> Items { get; set; }
	}
}
