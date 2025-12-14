using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Orders.Services;
using Orders.Services.DTO;

namespace OrdersApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder ([FromBody] OrderCreateDto ordderCreateDto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            await _orderService.AddOrderAsync(ordderCreateDto);
            return Ok(new { message = "Order Created Successfully" });
        }

        [HttpGet("GetAllOrder")]
        public async Task<ActionResult<List<OrderDto>>> GetAllOrders()
        {  
            var order=await _orderService.GetAllOrdersAsync();
            return Ok(order);
        }
        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<OrderDto>>GetById(Guid id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound (new { message = "Order Not Found" });
            }
            return Ok(order);

        }
        [HttpDelete("DeleteOrder/{id}")]
        public async Task<IActionResult>DeleteOrder (Guid id)
        {
            var order= await _orderService.GetOrderByIdAsync (id);
            if (order == null)
            { return NotFound(new { message = "Order Not Found" }); }

            await _orderService.DeleteOrderAsync(id);
            return Ok(new {message="Order Deleted Successfully" });

        }


       
    }
}
