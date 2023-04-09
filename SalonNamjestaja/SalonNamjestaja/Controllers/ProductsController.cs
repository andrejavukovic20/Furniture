using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SalonNamjestaja.CustomActionFilters;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models;
using SalonNamjestaja.Models.ProductModel;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;

        public ProductsController(IProductRepository productRepository, IMapper mapper)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
        }
        /// <summary>
        /// Vraca listu svih proizvoda.
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Product>>> GetProducts()
        {
            var products = await productRepository.GetProductsAsync();

            return Ok(products);
        }
        /// <summary>
        /// Vraca proizvod na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetProduct")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await productRepository.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(product);
        }
        /// <summary>
        /// Dodavanje podataka o proizvodu.
        /// </summary>
        /// <param name="addProduct"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct(AddProductRequest addProduct)
        {
            try
            {
                var product = mapper.Map<Product>(addProduct);

                product = await productRepository.AddAsync(product);

                var productDto = mapper.Map<ProductDto>(product);

                return CreatedAtAction(nameof(GetProduct), new { id = productDto.ProductId }, productDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
        /// <summary>
        /// Izmjena podataka o proizvodu.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateProduct"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, UpdateProductRequest updateProduct)
        {
            try
            {
                var product = mapper.Map<Product>(updateProduct);

                product = await productRepository.UpdateAsync(id, product);

                if (product == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<ProductDto>(product));
            }
             catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
        /// <summary>
        /// Brisanje proizvoda na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {

            var deletedProduct = await productRepository.DeleteAsync(id);

            if (deletedProduct == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(mapper.Map<ProductDto>(deletedProduct));
          
        }
       
    }
}
