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

			var urunler = orders.SelectMany(x => x.Items).ToList();

			var toplamCiro = urunler.Sum(y => y.SatisFiyat * y.Adet);

			var toplamNetKar = urunler
				.Sum(y => (y.SatisFiyat - y.AlisFiyat - y.KargoBedeli - (y.SatisFiyat * y.KomisyonOrani / 100)) * y.Adet);

			return new SummaryDto
			{
				ToplamSiparisSayisi = orders.Count,
				ToplamUrunAdedi = urunler.Sum(y => y.Adet),
				ToplamCiro = toplamCiro,
				ToplamNetKar = toplamNetKar
			};
		}

		public async Task<List<PlatformReportDto>> GetPlatformReportAsync()
		{
			var orders = await _orderRepository.GetAllAsync();

			return orders.GroupBy(x => x.Platform).Select(y =>
			{
				var urunler = y.SelectMany(z => z.Items).ToList();

				var toplamCiro = urunler.Sum(t => t.SatisFiyat * t.Adet);

				var toplamNetKar = urunler.Sum(t =>
				{
					var komisyon = t.SatisFiyat * t.KomisyonOrani / 100;
					return (t.SatisFiyat - t.AlisFiyat - t.KargoBedeli - komisyon) * t.Adet;
				});

				return new PlatformReportDto
				{
					Platform = y.Key,
					ToplamCiro = toplamCiro,
					ToplamNetKar = toplamNetKar,
					KarMarjiYuzde = Math.Round(toplamNetKar / toplamCiro * 100, 2)
				};
			}).ToList();
		}
	}
}
