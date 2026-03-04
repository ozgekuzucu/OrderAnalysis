using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAnalysis.Application.Interfaces;

namespace OrderAnalysis.API.Controllers
{
	[Route("api/report")]
	[ApiController]
	public class ReportsController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public ReportsController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet("summary")]
		public async Task<IActionResult> GetSummary()
		{
			var result = await _orderService.GetSummaryAsync();
			return Ok(result);
		}

		[HttpGet("platform")]
		public async Task<IActionResult> GetPlatformReport()
		{
			var result = await _orderService.GetPlatformReportAsync();
			return Ok(result);
		}

		[HttpGet("loss")]
		public async Task<IActionResult> GetLossReport()
		{
			var result = await _orderService.GetLossReportAsync();
			return Ok(result);
		}

		[HttpGet("anomaly")]
		public async Task<IActionResult> GetAnomalyReport()
		{
			var result = await _orderService.GetAnomalyReportAsync();
			return Ok(result);
		}

		[HttpGet("trend")]
		public async Task<IActionResult> GetTrendReport()
		{
			var result = await _orderService.GetTrendReportAsync();
			return Ok(result);
		}

		[HttpGet("risk")]
		public async Task<IActionResult> GetRiskReport()
		{
			var result = await _orderService.GetRiskReportAsync();
			return Ok(result);
		}

	}
}
