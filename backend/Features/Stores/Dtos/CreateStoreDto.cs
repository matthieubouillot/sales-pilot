using System.ComponentModel.DataAnnotations;

namespace SalesPilot.Api.Features.Stores.Dtos
{
    public class CreateStoreDto
    {
        [Required]
        public string Name { get; set; } = default!;
        [Required]
        public string City { get; set; } = default!;
    }
}