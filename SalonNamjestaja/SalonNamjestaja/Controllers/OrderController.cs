using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models;
using SalonNamjestaja.Repository;
using SalonNamjestaja.Models.OrderModel;
using SalonNamjestaja.Errors;
using SalonNamjestaja.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca listu svih porudzbina.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<Order>>> GetOrders()
        {
            var orders = await orderRepository.GetOrders();

            return Ok(orders);
        }
        /// <summary>
        /// Vraca porudzbinu na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetOrder")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Order>> GetOrder(int id)
        {
            var order = await orderRepository.GetOrderByIdAsync(id);

            if (order == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(order);

        }

        /// <summary>
        /// Dodavanje podataka o porudzbini.
        /// </summary>
        /// <param name="addOrder"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddOrder(AddOrderRequest addOrder)
        {
            try
            {
                var order = mapper.Map<Order>(addOrder);

                order = await orderRepository.AddAsync(order);

                var orderDto = mapper.Map<OrderDto>(order);

                return CreatedAtAction(nameof(GetOrder), new { id = orderDto.OrderId }, orderDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }


        }
        /// <summary>
        /// Izmjena podataka o porudzbini.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateOrder"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, UpdateOrderRequest updateOrder)
        {
            try
            {
                var order = mapper.Map<Order>(updateOrder);

                order = await orderRepository.UpdateAsync(id, order);

                if (order == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<OrderDto>(order));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
        /// <summary>
        /// Brisanje porudzbine na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            var deletedOrder = await orderRepository.DeleteAsync(id);

            if (deletedOrder == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(mapper.Map<OrderDto>(deletedOrder));


        }
    }
}
