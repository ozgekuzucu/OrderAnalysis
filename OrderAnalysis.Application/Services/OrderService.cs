using AutoMapper;
using MongoDB.Bson;
using OrderAnalysis.Application.DTOs.OrderDtos;
using OrderAnalysis.Application.DTOs.ReportDto;
using OrderAnalysis.Application.Interfaces;
using OrderAnalysis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;
		private readonly IMapper _mapper;

		public OrderService(IOrderRepository orderRepository, IMapper mapper)
		{
			_orderRepository = orderRepository;
			_mapper = mapper;
		}

		public async Task CreateOrderAsync(CreateOrderDto createOrderDto)
		{
			var order = _mapper.Map<Order>(createOrderDto);
			await _orderRepository.CreateAsync(order);
		}

		public async Task<SummaryDto> GetSummaryAsync()
		{
			var orders = await _orderRepository.GetAllAsync();

			var toplamCiro = orders.SelectMany(x => x.Items).Sum(y => y.SatisFiyat * y.Adet);

			var toplamNetKar = orders.SelectMany(x => x.Items)
				.Sum(y => (y.SatisFiyat - y.AlisFiyat - y.KargoBedeli - (y.SatisFiyat * y.KomisyonOrani / 100)) * y.Adet);

			return new SummaryDto
			{
				ToplamSiparisSayisi = orders.Count,
				ToplamUrunAdedi = orders.SelectMany(x => x.Items).Sum(y => y.Adet),
				ToplamCiro = toplamCiro,
				ToplamNetKar = toplamNetKar
			};
		}
	}
}
