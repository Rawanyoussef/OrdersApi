using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orders.Data.Entity;


namespace Orders.Data.Entity
{
    public class Order
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }


    }
}
