using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAnalysis.Application.Interfaces;

namespace OrderAnalysis.API.Controllers
{
	[Route("api/report")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public ReportController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpGet("summary")]
		public async Task<IActionResult> GetSummary()
		{
			var result = await _orderService.GetSummaryAsync();
			return Ok(result);
		}
	}
}
