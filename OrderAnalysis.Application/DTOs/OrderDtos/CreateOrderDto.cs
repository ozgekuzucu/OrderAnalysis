using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.DTOs.OrderDtos
{
	public class CreateOrderDto
	{
		public string Platform { get; set; }
		public DateTime Tarih { get; set; }
		public List<CreateOrderItemDto> Items { get; set; }
	}
}
