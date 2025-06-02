using Microsoft.AspNetCore.Mvc;
using SalesPilot.Api.Features.Sales.Services;
using SalesPilot.Api.Features.Sales.Dtos;
using SalesPilot.Api.Common.Responses;

namespace SalesPilot.Api.Features.Sales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly SaleService _service;

        public SalesController(SaleService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retourne la liste de toutes les ventes.
        /// </summary>
        /// <response code="200">Liste de toutes les ventes.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<SaleDto>>), 200)]
        public async Task<IActionResult> GetSales()
        {
            var sales = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<SaleDto>>(sales));
        }

        /// <summary>
        /// Récupère une vente par son identifiant.
        /// </summary>
        /// <param name="id">Id de la vente</param>
        /// <response code="200">Vente trouvée.</response>
        /// <response code="404">Vente non trouvée.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SaleDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> GetSale(int id)
        {
            var sale = await _service.GetByIdAsync(id);
            if (sale == null)
                return NotFound(new ApiResponse<string>("Vente non trouvée"));
            return Ok(new ApiResponse<SaleDto>(sale));
        }

        /// <summary>
        /// Crée une nouvelle vente.
        /// </summary>
        /// <param name="dto">Données de la vente à créer</param>
        /// <response code="201">Vente créée.</response>
        /// <response code="400">Modèle invalide.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<SaleDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Modèle invalide"));
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetSale), new { id = created.Id }, new ApiResponse<SaleDto>(created));
        }

        /// <summary>
        /// Met à jour une vente existante.
        /// </summary>
        /// <param name="id">Id de la vente</param>
        /// <param name="dto">Nouvelles données de la vente</param>
        /// <response code="200">Vente mise à jour.</response>
        /// <response code="400">Modèle invalide.</response>
        /// <response code="404">Vente non trouvée.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<SaleDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] UpdateSaleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Modèle invalide"));

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new ApiResponse<string>("Vente non trouvée"));

            return Ok(new ApiResponse<SaleDto>(updated));
        }

        /// <summary>
        /// Supprime une vente.
        /// </summary>
        /// <param name="id">Id de la vente</param>
        /// <response code="204">Vente supprimée.</response>
        /// <response code="404">Vente non trouvée.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> DeleteSale(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<string>("Vente non trouvée"));
            return NoContent();
        }
    }
}