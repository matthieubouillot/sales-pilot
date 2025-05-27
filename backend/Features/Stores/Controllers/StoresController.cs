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
        [HttpGet]
        public async Task<IActionResult> GetStores()
        {
            var stores = await _service.GetAllAsync();
            return Ok(new ApiResponse<List<StoreDto>>(stores));
        }

        /// <summary>
        /// Récupère un magasin par son identifiant.
        /// </summary>
        /// <param name="id">Id du magasin</param>
        [HttpGet("{id}")]
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
        [HttpPost]
        public async Task<IActionResult> CreateStore([FromBody] CreateStoreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Modèle invalide"));
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetStore), new { id = created.Id }, new ApiResponse<StoreDto>(created));
        }

        /// <summary>
        /// Met à jour un magasin existant.
        /// </summary>
        /// <param name="id">Id du magasin</param>
        /// <param name="dto">Nouvelles données du magasin</param>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStore(int id, [FromBody] UpdateStoreDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ApiResponse<string>("Modèle invalide"));
            var updated = await _service.UpdateAsync(id, dto);
            if (!updated)
                return NotFound(new ApiResponse<string>("Magasin non trouvé"));
            return NoContent();
        }

        /// <summary>
        /// Supprime un magasin.
        /// </summary>
        /// <param name="id">Id du magasin</param>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStore(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted)
                return NotFound(new ApiResponse<string>("Magasin non trouvé"));
            return NoContent();
        }
    }
}