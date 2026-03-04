using Microsoft.AspNetCore.Mvc;
using OrderAnalysis.Application.DTOs.OrderDtos;
using OrderAnalysis.Application.Interfaces;

namespace OrderAnalysis.API.Controllers
{
	[Route("api/orders")]
	[ApiController]
	public class OrdersController : ControllerBase
	{
		private readonly IOrderService _orderService;

		public OrdersController(IOrderService orderService)
		{
			_orderService = orderService;
		}

		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
		{
			await _orderService.CreateOrderAsync(createOrderDto);
			return Ok("Sipariş başarıyla eklendi.");
		}

		[HttpGet("test-error")]
		public IActionResult TestError()
		{
			throw new Exception("Bu bir test hatasıdır!");
		}
	}
}
