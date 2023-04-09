using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalonNamjestaja.CustomActionFilters;
using SalonNamjestaja.Data;
using SalonNamjestaja.Errors;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models.CategoryModel;
using SalonNamjestaja.Models.ProductModel;
using SalonNamjestaja.Repository;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : Controller
    {
        private readonly IProductCategoryRepository productCategoryRepository;
        private readonly IMapper mapper;

        public ProductCategoryController(IProductCategoryRepository productCategoryRepository, IMapper mapper)
        {
            this.productCategoryRepository = productCategoryRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca listu kategorija proizvoda.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<List<ProductCategory>>> GetProductCategories()
        {
            var productCategories = await productCategoryRepository.GetProductCategoriesAsync();

            return Ok(productCategories);
        }
        /// <summary>
        /// Vraca kategoriju proizvoda na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetProductCategory")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var productCategory = await productCategoryRepository.GetProductCategoryByIdAsync(id);

            if (productCategory == null)
            {
                return NotFound(new ApiResponse(404));
            }
            return Ok(productCategory);
        }

        /// <summary>
        /// Dodavanje podataka o kategoriji proizvoda.
        /// </summary>
        /// <param name="addCategory"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory(AddCategoryRequest addCategory)
        {
            try
            {
                var productCategory = mapper.Map<ProductCategory>(addCategory);
                productCategory = await productCategoryRepository.AddAsync(productCategory);

                var productCategoryDto = mapper.Map<CategoryDto>(productCategory);

                return CreatedAtAction(nameof(GetProductCategory), new { id = productCategoryDto.CategoryId }, productCategoryDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }
        }
        /// <summary>
        /// Izmjena podataka o kategoriji proizvoda.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCategory"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateCategory([FromRoute] int id, UpdateCategoryRequest updateCategory)
        {
            try
            {
                var productCategory = mapper.Map<ProductCategory>(updateCategory);

                productCategory = await productCategoryRepository.UpdateAsync(id, productCategory);

                if (productCategory == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<CategoryDto>(productCategory));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }
        }
        /// <summary>
        /// Brisanje kategorije proizvoda na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            var deletedCategory = await productCategoryRepository.DeleteAsync(id);

            if (deletedCategory == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(mapper.Map<CategoryDto>(deletedCategory));



        }
    }
}
