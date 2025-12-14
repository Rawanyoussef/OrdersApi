using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Data.Entity;


namespace Orders.Repository
{
    public interface  IOrderRepo
    {
        Task<Order> GetByIdAsync(Guid id);
        Task<List<Order>> GetAllAsync();
         Task AddAsync(Order order);    
        Task DeleteAsync( Order order);



    }
}
