using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Infrastructure.Settings
{
	public class DatabaseSettings : IDatabaseSettings
	{
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
		public string OrderCollectionName { get; set; }
	}
}
