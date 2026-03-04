using OrderAnalysis.Application.DTOs.OrderDtos;
using OrderAnalysis.Application.DTOs.ReportDto;
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
		Task<SummaryDto> GetSummaryAsync();
		Task<List<PlatformReportDto>> GetPlatformReportAsync();
		Task<List<LossReportDto>> GetLossReportAsync();
		Task<List<AnomalyReportDto>> GetAnomalyReportAsync();
		Task<List<TrendReportDto>> GetTrendReportAsync();
		Task<List<RiskReportDto>> GetRiskReportAsync();
	}
}
