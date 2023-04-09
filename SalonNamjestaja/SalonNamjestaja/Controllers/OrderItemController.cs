using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamjestaja.CustomActionFilters;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models.OrderItemModel;
using SalonNamjestaja.Models.ProductModel;
using SalonNamjestaja.Repository;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : Controller
    {
        private readonly IOrderItemRepository orderItemRepository;
        private readonly IMapper mapper;

        public OrderItemController(IOrderItemRepository orderItemRepository, IMapper mapper)
        {
            this.orderItemRepository = orderItemRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca listu stavki porudzbine.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<IReadOnlyList<OrderItem>>> GetOrderItems()
        {
            var orderItems = await orderItemRepository.GetOrderItems();

            return Ok(orderItems);
        }
        /// <summary>
        /// Vraca stavku porudzbine na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetOrderItem")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<OrderItem>> GetOrderItem(int id)
        {
            var orderItem = await orderItemRepository.GetOrderItemByIdAsync(id);

            if (orderItem == null)
            {
                 return NotFound(new ApiResponse(404));
            }
            return Ok(orderItem);
           
        }
        /// <summary>
        /// Dodavanje podataka o stavci porudzbine.
        /// </summary>
        /// <param name="addOrderItem"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddOrderItem(AddOrderItemRequset addOrderItem)
        {
           try
            {
                var orderItem = mapper.Map<OrderItem>(addOrderItem);

                orderItem = await orderItemRepository.AddAsync(orderItem);

                var orderItemDto = mapper.Map<OrderItem>(orderItem);

                return CreatedAtAction(nameof(GetOrderItem), new { id = orderItemDto.OrderItemId }, orderItemDto);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }


        }
        /// <summary>
        /// Izmjena podataka o stavci porudzbine.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateOrderItem"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateOrderItem([FromRoute] int id, UpdateOrderItemRequest updateOrderItem)
        {
            try
            {
                var orderItem = mapper.Map<OrderItem>(updateOrderItem);

                orderItem = await orderItemRepository.UpdateAsync(id, orderItem);

                if (orderItem == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<OrderItemDto>(orderItem));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
        /// <summary>
        /// Brisanje stavke porudzbine na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteOrderItem([FromRoute] int id)
        {
            var deletedOrderItem = await orderItemRepository.DeleteAsync(id);

            if (deletedOrderItem == null)
            {
                  return NotFound(new ApiResponse(404));
            }
            
            return Ok(mapper.Map<OrderItemDto>(deletedOrderItem));
          
        }
    }
}
