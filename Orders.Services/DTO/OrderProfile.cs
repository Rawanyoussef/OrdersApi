using AutoMapper;
using Orders.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Services.DTO
{
    public class OrderProfile :Profile
    {
        public OrderProfile() 
        {
            CreateMap<Order, OrderDto>().ReverseMap();
            CreateMap<OrderCreateDto, Order>().ReverseMap();
        }
    }
}
