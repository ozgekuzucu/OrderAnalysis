using MongoDB.Driver;
using OrderAnalysis.Domain.Entities;
using OrderAnalysis.Infrastructure.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Infrastructure.Persistence
{
	public class MongoDbContext
	{
		public IMongoCollection<Order> Orders { get; }
		public MongoDbContext(IDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			Orders = database.GetCollection<Order>(settings.OrderCollectionName);
		}


	}
}
