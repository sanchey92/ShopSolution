using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopSolution.API.Dtos;
using ShopSolution.API.Errors;
using ShopSolution.API.Extensions;
using ShopSolution.Core.Entities.OrderAggregate;
using ShopSolution.Core.Interfaces;

namespace ShopSolution.API.Controllers
{
    [Authorize]
    public class OrdersController : BaseApiController
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IMapper mapper)
        {
            _orderService = orderService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var email = HttpContext.User?.RetrieveEmailFromPrincipal();
            var address = _mapper.Map<AddressDto, Address>(orderDto.ShipToAddress);
            var order = await _orderService.CreateOrderAsync(email, orderDto.DeliveryMethodId,
                orderDto.BasketId, address);
            if (order == null) return BadRequest(new ApiResponse(400, "Problem creating order"));
            return Ok(order);
        }
    }
}