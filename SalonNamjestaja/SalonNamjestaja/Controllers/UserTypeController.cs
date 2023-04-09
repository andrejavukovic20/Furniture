using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamjestaja.CustomActionFilters;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models.UserTypeModel;
using SalonNamjestaja.Repository;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserTypeController : Controller
    {
        private readonly IUserTypeRepository userTypeRepository;
        private readonly IMapper mapper;

        public UserTypeController(IUserTypeRepository userTypeRepository, IMapper mapper)
        {
            this.userTypeRepository = userTypeRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraca listu tipova korisnika.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<UserType>>> GetUserTypes()
        {
            var userTypes = await userTypeRepository.GetUserTypesAsync();

            return Ok(userTypes);
        }
        /// <summary>
        /// Vraca tip korisnika sa prosljedjenim ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetUserType")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserType>> GetUserType(int id)
        {
            var userType = await userTypeRepository.GetUserTypeByIdAsync(id);

            if (userType == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(userType);
           
        }
        /// <summary>
        /// Dodavanje tipa korisnika.
        /// </summary>
        /// <param name="addUserType"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddUserType(AddUserTypeRequest addUserType)
        {
            try
            {
                var userType = mapper.Map<UserType>(addUserType);

                userType = await userTypeRepository.AddAsync(userType);

                var userTypeDto = mapper.Map<UserTypeDto>(userType);

                return CreatedAtAction(nameof(GetUserType), new { id = userTypeDto.UserId }, userTypeDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }


        }
        /// <summary>
        /// Izmjena tipa korisnika.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserType"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateUserType([FromRoute] int id, UpdateUserTypeRequest updateUserType)
        {
            try
            {
                var userType = mapper.Map<UserType>(updateUserType);

                userType = await userTypeRepository.UpdateAsync(id, userType);

                if (userType == null)
                {
                    return NotFound();
                }

                return Ok(mapper.Map<UserTypeDto>(userType));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
        /// <summary>
        /// Brisanje tipa korisnika.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUserType([FromRoute] int id)
        {
            var deletedUserType = await userTypeRepository.DeleteAsync(id);

            if (deletedUserType == null)
            {
                return NotFound();
            }

            return Ok(mapper.Map<UserTypeDto>(deletedUserType));
        }
    }
}
