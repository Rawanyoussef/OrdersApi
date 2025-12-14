using AutoMapper;
using Orders.Data.Entity;
using Orders.Services.DTO;
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
            CreateMap<Order, OrderDto>().ForMember(d => d.OrderId, o => o.MapFrom(s => s.Id)); ;
            CreateMap<OrderCreateDto, Order>();
        }
    }
}
