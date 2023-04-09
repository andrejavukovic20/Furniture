using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamjestaja.CustomActionFilters;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models;
using SalonNamjestaja.Models.AddressModel;
using SalonNamjestaja.Repository;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
 
    public class AddressController : Controller
    {
        private readonly IAddressRepository addressRepository;
        private readonly IMapper mapper;

        public AddressController(IAddressRepository addressRepository, IMapper mapper)
        {
            this.addressRepository = addressRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca listu svih adresa.
        /// </summary>
        /// <returns>Lista svih adresa</returns>
        [HttpGet]
        [Authorize(Roles ="User, Admin")]
        public async Task<ActionResult<IReadOnlyList<Address>>> GetAddresses()
        {
            var addresses = await addressRepository.GetAddressesAsync();

            return Ok(addresses);
        }
        /// <summary>
        /// Vraca adresu sa prosljedjenim ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetAddress")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<Address>> GetAddress(int id)
        {
            var address = await addressRepository.GetAddressByIdAsync(id);

            if (address == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(address);

        }
        /// <summary>
        /// Dodavanje podataka o adresi.
        /// </summary>
        /// <param name="addAddress"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> AddAddress(AddAddressRequest addAddress)
        {
            try
            {
                var address = mapper.Map<Address>(addAddress);

                address = await addressRepository.AddAsync(address);

                var addressDto = mapper.Map<AddressDto>(address);

                return CreatedAtAction(nameof(GetAddress), new { id = addressDto.AddressId }, addressDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }



        }
        /// <summary>
        /// Izmjena podataka o adresi.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateAddress"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateAddress([FromRoute] int id, UpdateAddressRequest updateAddress)
        {

            try
            {

                var address = mapper.Map<Address>(updateAddress);

                address = await addressRepository.UpdateAsync(id, address);

                if (address == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<AddressDto>(address));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }


        }
        /// <summary>
        /// Brisanje adrese na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> DeleteAddress([FromRoute] int id)
        {
            try
            {
                var deletedAddress = await addressRepository.DeleteAsync(id);

                if (deletedAddress == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<AddressDto>(deletedAddress));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
    }
}
