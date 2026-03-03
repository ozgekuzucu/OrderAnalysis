using AutoMapper;
using OrderAnalysis.Application.DTOs.OrderDtos;
using OrderAnalysis.Application.DTOs.OrderItemDtos;
using OrderAnalysis.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderAnalysis.Application.Mappings
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateOrderDto, Order>();
			CreateMap<CreateOrderItemDto, OrderItem>();
		}
	}
}
