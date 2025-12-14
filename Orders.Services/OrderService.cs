using Orders.Data.Entity;
using Orders.Repository;
using Orders.Services.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;

        public OrderService(IOrderRepo orderRepo, ICacheService cache, IMapper mapper)
        {
            _orderRepo = orderRepo;
            _cache = cache;
            _mapper = mapper;
        }

        public async Task AddOrderAsync(OrderCreateDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);
            order.Id = Guid.NewGuid();
            order.CreatedAt = DateTime.UtcNow;
            await _orderRepo.AddAsync(order);
            string cacheKey = $"order:{order.Id}";
            await _cache.SetAsync(cacheKey, order, TimeSpan.FromMinutes(5));
        }
        public async Task DeleteOrderAsync(Guid id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order != null)
            {
                await _orderRepo.DeleteAsync(order);
                await _cache.RemoveAsync($"order:{id}");
            }
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return _mapper.Map<List<OrderDto>>(orders);
        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
        {
            string cacheKey = $"order:{id}";

            var cachedOrder = await _cache.GetAsync<Order>(cacheKey);
            if (cachedOrder != null)
                return _mapper.Map<OrderDto>(cachedOrder);

            var order = await _orderRepo.GetByIdAsync(id);
            if (order != null)
            {
                await _cache.SetAsync(cacheKey, order, TimeSpan.FromMinutes(5));
                return _mapper.Map<OrderDto>(order);
            }

            return null; 
        }
    }
}
