using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.CustomActionFilters;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models.BillModel;
using SalonNamjestaja.Repository;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillController : Controller
    {
        private readonly IBillRepository billRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public BillController(IBillRepository billRepository,IOrderRepository orderRepository, IMapper mapper)
        {
            this.billRepository = billRepository;
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca listu svih racuna/faktura.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<Bill>>> GetBills()
        {
            var bills = await billRepository.GetBillsAsync();

            return Ok(bills);
        }
        /// <summary>
        /// Vraca racun/fakturu na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetBill")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Bill>> GetBill(int id)
        {
            var bill = await billRepository.GetBillByIdAsync(id);

            if (bill == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(bill);
        }
        /// <summary>
        /// Dodavanje podataka o racunu/fakturi.
        /// </summary>
        /// <param name="addBill"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> AddBill(AddBillRequest addBill)
        {
            try
            {

                var bill = mapper.Map<Bill>(addBill);

                bill = await billRepository.AddAsync(bill);

                var billDto = mapper.Map<BillDto>(bill);

                return CreatedAtAction(nameof(GetBill), new { id = billDto.BillId }, billDto);
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Message.Contains("Can not insert or update an Bill, because there are no orders with that ID."))
                {
                    ModelState.AddModelError("OrderID", "You can not add an Bill, because there are no orders with that ID.");
                    return BadRequest(new { Errors = ModelState });
                }
                else if (ex.InnerException is SqlException && (ex.InnerException.Message.Contains("Violation of UNIQUE KEY") || ex.InnerException.Message.Contains("Cannot insert duplicate key")))
                {
                    return Conflict(new ApiResponse(409));
                }
                else
                {
                    return BadRequest(new ApiResponse(400));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400));
            }
        }
        /// <summary>
        /// Izmjena podataka o racunu/fakturi.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateBill"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBill([FromRoute] int id, UpdateBillRequest updateBill)
        {
           try
            {
                var bill = mapper.Map<Bill>(updateBill);

                bill = await billRepository.UpdateAsync(id, bill);

                if (bill == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<BillDto>(bill));
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sqlException && sqlException.Message.Contains("Can not insert or update an Bill, because there are no orders with that ID."))
                {
                    ModelState.AddModelError("OrderID", "You can not add an Bill, because there are no orders with that ID.");
                    return BadRequest(new { Errors = ModelState });
                }
                else if (ex.InnerException is SqlException && (ex.InnerException.Message.Contains("Violation of UNIQUE KEY") || ex.InnerException.Message.Contains("Cannot insert duplicate key")))
                {
                    return Conflict(new ApiResponse(409));
                }
                else
                {
                    return BadRequest(new ApiResponse(400));
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResponse(400));
            }

        }
        /// <summary>
        /// Brisanje racuna/fakture na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBill([FromRoute] int id)
        {
            
            var deletedBill = await billRepository.DeleteAsync(id);

            if (deletedBill == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(mapper.Map<BillDto>(deletedBill));


        }
    }
}
