using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models.CustomerModel;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly FurnitureDbContext dbContext;
        private readonly IConfiguration config;
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly PasswordHasher<Customer> passwordHasher;

        public AuthController(FurnitureDbContext dbContext, IConfiguration config, ICustomerRepository customerRepository, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.config = config;
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            passwordHasher = new PasswordHasher<Customer>();
        }
        /// <summary>
        /// Registracija korisnika.
        /// </summary>
        /// <param name="registerRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult<CustomerDto>> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var existingCustomer = dbContext.Customers.Where(a => a.Username == registerRequest.Username).FirstOrDefault();

            if (existingCustomer != null) 
            {
                return BadRequest("Username already exists!");
            }
            var newCustomer = new Customer
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Phone = registerRequest.Phone,
                Email = registerRequest.Email,
                Username = registerRequest.Username,
                Password = passwordHasher.HashPassword(null, registerRequest.Password),
                AddressId = registerRequest.AddressId,
                UserId = 1

            };

            await customerRepository.AddAsync(newCustomer);
            await customerRepository.GetCustomerByIdAsync(newCustomer.CustomerId);
            return mapper.Map<Customer, CustomerDto>(newCustomer);
        }
        /// <summary>
        /// Prijava korisnika ili admina.
        /// </summary>
        /// <param name="loginRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequestDto)
        {
            var existingCustomer = dbContext.Customers.Where(a => a.Username == loginRequestDto.Username).FirstOrDefault();
            if (existingCustomer == null) 
            {
                return Unauthorized("Your credenitals are not valid!");
            }

            var result = passwordHasher.VerifyHashedPassword(existingCustomer, existingCustomer.Password, loginRequestDto.Password);

            if(result != PasswordVerificationResult.Success)
            {
                return Unauthorized("Your credenitals are not valid!");
            }

            string role = null!;
            switch (existingCustomer.UserId)
            {
                case 1:
                    role = "User";
                    break;
                case 2:
                    role = "Admin";
                    break;
            }

            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, existingCustomer.Username),
                new Claim("JWTID", Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role),
            };

            var token = GenerateNewJsonWebToken(authClaims);
            return Ok(token);

        }

        private string GenerateNewJsonWebToken(List<Claim> claims)
        {
            var authKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"]));
            var tokenObject = new JwtSecurityToken(
                issuer: config["JWT:ValidIssuer"],
                audience: config["JWT:ValidAudience"],
                expires:DateTime.Now.AddHours(1),
                claims:claims,
                signingCredentials:new SigningCredentials(authKey, SecurityAlgorithms.HmacSha256)
                );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenObject);
            return token;


        }
        /// <summary>
        /// Dodjeljivanje uloge "Admin".
        /// </summary>
        /// <param name="adminRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Admin")]
        public async Task<IActionResult> Admin([FromBody] AdminRequestDto adminRequestDto)
        {
            var existingCustomer = dbContext.Customers.Where(a => a.Username == adminRequestDto.Username).FirstOrDefault();
            if (existingCustomer == null)
            {
                return BadRequest("Username is invalid!");

            }
            existingCustomer.UserId = 2;
            dbContext.Customers.Update(existingCustomer);
            dbContext.SaveChanges();

            return Ok("User is now admin.");

        }
    }
}
