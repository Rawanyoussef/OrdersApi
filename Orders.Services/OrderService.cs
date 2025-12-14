using Orders.Data.Entity;
using Orders.Repository;
using Orders.Services.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Orders.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo _orderRepo;
        private readonly ICacheService _cache;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IOrderRepo orderRepo, ICacheService cache, IMapper mapper ,ILogger<OrderService> logger)
        {
            _orderRepo = orderRepo;
            _cache = cache;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task AddOrderAsync(OrderCreateDto orderDto)
        {
            try
            {
                var order = _mapper.Map<Order>(orderDto);
                order.Id = Guid.NewGuid();
                order.CreatedAt = DateTime.UtcNow;
                await _orderRepo.AddAsync(order);
                _logger.LogInformation("Order Created");
                string cacheKey = $"order:{order.Id}";
                await _cache.SetAsync(cacheKey, order, TimeSpan.FromMinutes(5));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error while Creating order");
                throw;
            }
        }
        public async Task DeleteOrderAsync(Guid id)
        {
            try
            {
                var order = await _orderRepo.GetByIdAsync(id);
                if (order == null)
                {
                    _logger.LogWarning("Order Not Found");
                    return;
                }
                await _orderRepo.DeleteAsync(order);
                await _cache.RemoveAsync($"order:{id}");
                _logger.LogInformation("Order Deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Deleting order");
                throw;
            }
            
        }

        public async Task<List<OrderDto>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepo.GetAllAsync();
                _logger.LogInformation("Feteched all orders");
                return _mapper.Map<List<OrderDto>>(orders);
            }
            catch (Exception ex) 
            { 
                _logger.LogError(ex,"Error While fetching orders");
                throw;
            } 

        }

        public async Task<OrderDto> GetOrderByIdAsync(Guid id)
        {
            try
            {
                string cacheKey = $"order:{id}";

                var cachedOrder = await _cache.GetAsync<Order>(cacheKey);
                if (cachedOrder != null)
                {
                    _logger.LogInformation("Order Fetched from cache");
                    return _mapper.Map<OrderDto>(cachedOrder);
                }

                var order = await _orderRepo.GetByIdAsync(id);

                if (order != null)
                {
                    await _cache.SetAsync(cacheKey, order, TimeSpan.FromMinutes(5));
                    _logger.LogInformation("Order Fetched");                  
                }
                return _mapper.Map<OrderDto>(order);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While fetching");
                throw;
            }
            
        }
    }
}
