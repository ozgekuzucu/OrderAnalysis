using AutoMapper;
using MongoDB.Bson;
using OrderAnalysis.Application.DTOs.OrderDtos;
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
	}
}
