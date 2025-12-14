using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Data.Entity;


namespace Orders.Data.Context
{
    public class OrdersContext:DbContext
    {
       public OrdersContext(DbContextOptions<OrdersContext> options) : base(options) { }    
        public DbSet <Order> Order { get; set; }
    }
}
