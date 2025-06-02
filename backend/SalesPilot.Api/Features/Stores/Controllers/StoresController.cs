using Microsoft.AspNetCore.Mvc;
using SalesPilot.Api.Features.Stores.Services;
using SalesPilot.Api.Features.Stores.Dtos;
using SalesPilot.Api.Common.Responses;

namespace SalesPilot.Api.Features.Stores.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StoresController : ControllerBase
    {
        private readonly StoreService _service;

        public StoresController(StoreService service)
        {
            _service = service;
        }

        /// <summary>
        /// Retourne la liste de tous les magasins.
        /// </summary>
        /// <response code="200">Liste de tous les magasins.</response>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<List<StoreDto>>), 200)]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<StoreDto>>(stores));
        }

        /// <summary>
        /// Récupère un magasin par son identifiant.
        /// </summary>
        /// <param name="id">Id du magasin</param>
        /// <response code="200">Le magasin correspondant.</response>
        /// <response code="404">Magasin non trouvé.</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<StoreDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> GetStore(int id)
        {
            var store = await _service.GetByIdAsync(id);
            if (store == null)
                return NotFound(new ApiResponse<string>("Magasin non trouvé"));
            return Ok(new ApiResponse<StoreDto>(store));
        }

        /// <summary>
        /// Crée un nouveau magasin.
        /// </summary>
        /// <param name="dto">Données du magasin à créer</param>
        /// <response code="201">Magasin créé.</response>
        /// <response code="400">Modèle invalide.</response>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<StoreDto>), 201)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Modèle invalide"));

            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(
                nameof(GetStore),
                new { id = created.Id },
                new ApiResponse<StoreDto>(created)
            );
        }

        /// <summary>
        /// Met à jour un magasin existant.
        /// </summary>
        /// <param name="id">Id du magasin</param>
        /// <param name="dto">Nouvelles données du magasin</param>
        /// <response code="200">Magasin mis à jour.</response>
        /// <response code="400">Modèle invalide.</response>
        /// <response code="404">Magasin non trouvé.</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse<StoreDto>), 200)]
        [ProducesResponseType(typeof(ApiResponse<string>), 400)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] UpdateStoreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Modèle invalide"));

            var updated = await _service.UpdateAsync(id, dto);
            if (updated == null)
                return NotFound(new ApiResponse<string>("Magasin non trouvé"));

            // DTO conversion
            var updatedDto = new StoreDto { Id = updated.Id, Name = updated.Name, City = updated.City };
            return Ok(new ApiResponse<StoreDto>(updatedDto));
        }

        /// <summary>
        /// Supprime un magasin.
        /// </summary>
        /// <param name="id">Id du magasin</param>
        /// <response code="204">Magasin supprimé.</response>
        /// <response code="404">Magasin non trouvé.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiResponse<string>), 404)]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<string>("Magasin non trouvé"));
            return NoContent();
        }
    }
}