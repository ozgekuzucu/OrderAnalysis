using MongoDB.Driver;
using OrderAnalysis.Application.Interfaces;
using OrderAnalysis.Domain.Entities;
using OrderAnalysis.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Infrastructure.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly IMongoCollection<Order> _orderCollection;

		public OrderRepository(MongoDbContext context)
		{
			_orderCollection = context.Orders;
		}

		public async Task CreateAsync(Order order)
		{
			await _orderCollection.InsertOneAsync(order);
		}

		public async Task<List<Order>> GetAllAsync()
		{
			return await _orderCollection.Find(x => true).ToListAsync();
		}
	}
}
