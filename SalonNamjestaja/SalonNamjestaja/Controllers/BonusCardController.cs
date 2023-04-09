using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SalonNamjestaja.Data;
using SalonNamjestaja.Interfaces;
using SalonNamjestaja.Models;
using SalonNamjestaja.Repository;
using SalonNamjestaja.Models.BonusCardModel;
using SalonNamjestaja.Errors;
using SalonNamjestaja.CustomActionFilters;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace SalonNamjestaja.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BonusCardController : Controller
    {
        private readonly IBonusCardRepository bonusCardRepository;
        private readonly IMapper mapper;

        public BonusCardController(IBonusCardRepository bonusCardRepository, IMapper mapper)
        {
            this.bonusCardRepository = bonusCardRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Vraca listu bonus kartica.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<BonusCard>>> GetBonusCards()
        {
            var bonusCards = await bonusCardRepository.GetBonusCardsAsync();

            return Ok(bonusCards);
        }
        /// <summary>
        /// Vraca bonus karticu na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ActionName("GetBonusCard")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BonusCard>> GetBonusCard(int id)
        {
            var bonusCard = await bonusCardRepository.GetBonusCardByIdAsync(id);

            if (bonusCard == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(bonusCard);
        }
        /// <summary>
        /// Dodavanje podataka o bonus kartici.
        /// </summary>
        /// <param name="addCard"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddBonusCard(AddCardRequest addCard)
        {
            try
            {
                var bonusCard = mapper.Map<BonusCard>(addCard);

                bonusCard = await bonusCardRepository.AddAsync(bonusCard);

                var bonusCardDto = mapper.Map<BonusCardDto>(bonusCard);

                return CreatedAtAction(nameof(GetBonusCard), new { id = bonusCardDto.BonusCardId }, bonusCardDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }
        }
        /// <summary>
        /// Izmjena podataka o bonus kartici.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateCard"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ValidateModel]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBonusCard([FromRoute] int id, UpdateCardRequest updateCard)
        {
            try
            {
                var bonusCard = mapper.Map<BonusCard>(updateCard);

                bonusCard = await bonusCardRepository.UpdateAsync(id, bonusCard);

                if (bonusCard == null)
                {
                    return NotFound(new ApiResponse(404));
                }

                return Ok(mapper.Map<BonusCard>(bonusCard));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred while processing the request: {ex: Message}");
            }

        }
        /// <summary>
        /// Brisanje bonus kartice na osnovu prosljedjenog ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCard([FromRoute] int id)
        {
            var deletedCard = await bonusCardRepository.DeleteAsync(id);

            if (deletedCard == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(mapper.Map<BonusCardDto>(deletedCard));


        }
    }
}
