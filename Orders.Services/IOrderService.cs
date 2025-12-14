using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orders.Data.Entity;
using Orders.Services.DTO;


namespace Orders.Services
{
    public interface IOrderService
    {
        Task AddOrderAsync(OrderCreateDto OrderCreateDto);
        Task DeleteOrderAsync(Guid id);
        Task<OrderDto> GetOrderByIdAsync(Guid id);
        Task<List<OrderDto>> GetAllOrdersAsync();


    }
}
