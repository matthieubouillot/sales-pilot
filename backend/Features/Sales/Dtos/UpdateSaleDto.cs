using System.ComponentModel.DataAnnotations;

namespace SalesPilot.Api.Features.Sales.Dtos
{
    public class UpdateSaleDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Product { get; set; } = default!;

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }

        [Required]
        public int StockAfterSale { get; set; }
    }
}