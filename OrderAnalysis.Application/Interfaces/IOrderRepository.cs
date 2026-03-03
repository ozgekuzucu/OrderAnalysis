using OrderAnalysis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.Interfaces
{
	public interface IOrderRepository
	{
		Task CreateAsync(Order order);
		Task<List<Order>> GetAllAsync();
	}
}
