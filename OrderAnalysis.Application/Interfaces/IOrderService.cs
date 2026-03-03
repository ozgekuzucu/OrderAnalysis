using OrderAnalysis.Application.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.Interfaces
{
	public interface IOrderService
	{
		Task CreateOrderAsync(CreateOrderDto createOrderDto);
	}
}
