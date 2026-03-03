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

	}
}
