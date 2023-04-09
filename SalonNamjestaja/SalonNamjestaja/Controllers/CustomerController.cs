using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamjestaja.CustomActionFilters;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models;
using SalonNamjestaja.Models.CustomerModel;
using SalonNamjestaja.Models.ProductModel;
using SalonNamjestaja.Repository;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;

        public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca listu svih kupaca/korisnika.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<Customer>>> GetCustomers()
        {
            var customers = await customerRepository.GetCustomersAsync();

            return Ok(customers);
        }
        /// <summary>
        /// Vraca kupca/korisnika na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetCustomer")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {
            var customer = await customerRepository.GetCustomerByIdAsync(id);

            if (customer == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(customer);
         
        }
        /// <summary>
        /// Dodavanje podataka o kupcu/korisniku.
        /// </summary>
        /// <param name="addCustomer"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> AddCustomer(AddCustomerRequest addCustomer)
        {
            try
            {
                var customer = mapper.Map<Customer>(addCustomer);

                customer = await customerRepository.AddAsync(customer);

                var customerDto = mapper.Map<CustomerDto>(customer);

                return CreatedAtAction(nameof(GetCustomer), new { id = customerDto.CustomerId }, customerDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }
        }
        /// <summary>
        /// Izmjena podataka o kupcu/korisniku.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCustomer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int id, UpdateCustomerRequest updateCustomer)
        {
            try
            {
                var customer = mapper.Map<Customer>(updateCustomer);

                customer = await customerRepository.UpdateAsync(id, customer);

                if (customer == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<CustomerDto>(customer));
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
        /// <summary>
        /// Brisanje kupca/korisnika na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int id)
        {
            var deletedCustomer = await customerRepository.DeleteAsync(id);

            if (deletedCustomer == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(mapper.Map<CustomerDto>(deletedCustomer));


        }


    }
}
