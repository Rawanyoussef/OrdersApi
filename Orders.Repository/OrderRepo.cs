using Orders.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Orders.Data.Context;


namespace Orders.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly OrdersContext _context;
        public OrderRepo(OrdersContext context) 
        {
            _context = context;
        }

        
        public async Task AddAsync(Order order)
        {
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

        }

        public  async Task DeleteAsync(Order order)
        {
             _context.Order.Remove(order);
            await _context.SaveChangesAsync();

        }

        public async Task<List<Order>> GetAllAsync()
        {
          return await _context.Order.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _context.Order.FirstOrDefaultAsync(o => o.Id == id);

        }
    }
}
