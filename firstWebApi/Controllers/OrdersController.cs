using AutoMapper;
using DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace firstWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> Post([FromBody] OrderDTO order)
        {
            Order ord = _mapper.Map<OrderDTO, Order>(order);
            Order newOrder = await _orderService.AddOrder(ord);
            OrderDTO newOrderDto = _mapper.Map<Order, OrderDTO>(newOrder);
            return CreatedAtAction(nameof(Post), new { id = newOrderDto.Id }, newOrderDto);
        }

    }
}
