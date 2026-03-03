using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Infrastructure.Settings
{
	public interface IDatabaseSettings
	{
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
		string OrderCollectionName { get; set; }
	}
}
