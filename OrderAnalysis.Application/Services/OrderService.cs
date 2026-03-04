using AutoMapper;
using OrderAnalysis.Application.DTOs.OrderDtos;
using OrderAnalysis.Application.DTOs.ReportDto;
using OrderAnalysis.Application.Interfaces;
using OrderAnalysis.Domain.Entities;

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

		public async Task<List<LossReportDto>> GetLossReportAsync()
		{
			var orders = await _orderRepository.GetAllAsync();

			var urunler = orders.SelectMany(x => x.Items).ToList();

			return urunler.GroupBy(x => x.Urun)
				.Select(y =>
					{
						var komisyon = y.Sum(x => x.SatisFiyat * x.KomisyonOrani / 100);
						var netKar = y.Sum(y =>
						{
							var kar = y.SatisFiyat - y.AlisFiyat - y.KargoBedeli - (y.SatisFiyat * y.KomisyonOrani / 100);
							return kar * y.Adet;
						});
						return new { Urun = y.Key, NetKar = netKar };
					})
					.Where(x => x.NetKar < 0)
					.Select(x => new LossReportDto
					{
						Urun = x.Urun,
						ToplamZarar = Math.Abs(x.NetKar)
					}).ToList();
		}

		// Her ürünün ortalama satış fiyatından ne kadar saptığını hesaplar
		public async Task<List<AnomalyReportDto>> GetAnomalyReportAsync()
		{
			var orders = await _orderRepository.GetAllAsync();

			var urunler = orders.SelectMany(x => x.Items).ToList();

			return urunler.GroupBy(x => x.Urun).Select(y =>
			{
				var ortalama = y.Average(z => z.SatisFiyat);
				var mevcutFiyat = y.Last().SatisFiyat;
				var sapma = Math.Round((mevcutFiyat - ortalama) / ortalama * 100, 2);

				return new AnomalyReportDto
				{
					Urun = y.Key,
					OrtalamaSatisFiyat = Math.Round(ortalama, 2),
					MevcutSatisFiyat = mevcutFiyat,
					SapmaYuzde = sapma
				};
			}).ToList();
		}

		//Her gün ne kadar sattık, bir önceki güne göre daha mı iyi daha mı kötü?
		public async Task<List<TrendReportDto>> GetTrendReportAsync()
		{
			var orders = await _orderRepository.GetAllAsync();
			var gunlukData = orders.GroupBy(x => x.Tarih.Date).Select(y => new
			{
				Tarih = y.Key,
				GunlukCiro = y.SelectMany(z => z.Items)
				.Sum(t => t.SatisFiyat * t.Adet),
				GunlukNetKar = y.SelectMany(z => z.Items)
				.Sum(t => (t.SatisFiyat - t.AlisFiyat - t.KargoBedeli - (t.SatisFiyat * t.KomisyonOrani / 100)) * t.Adet)
			}).OrderBy(x => x.Tarih)
			.ToList();

			var result = new List<TrendReportDto>();

			for (int i = 0; i < gunlukData.Count; i++)
			{
				var degisim = i == 0 ? 0
					: gunlukData[i - 1].GunlukCiro == 0 ? 0
			 : Math.Round(
				 (gunlukData[i].GunlukCiro - gunlukData[i - 1].GunlukCiro)
				 / gunlukData[i - 1].GunlukCiro * 100, 2);

				result.Add(new TrendReportDto
				{
					Tarih = gunlukData[i].Tarih.ToString("yyyy-MM-dd"),
					GunlukCiro = gunlukData[i].GunlukCiro,
					GunlukNetKar = gunlukData[i].GunlukNetKar,
					OncekiGuneGoreDegisimYuzde = degisim
				});

			}
			return result;
		}

		public Task<List<RiskReportDto>> GetRiskReportAsync()
		{
			throw new NotImplementedException();
		}
	}
}

