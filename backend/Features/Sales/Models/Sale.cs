using SalesPilot.Api.Features.Stores.Models;

namespace SalesPilot.Api.Features.Sales.Models
{
    public class Sale
    {
        public int Id { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public DateTime Date { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public int StockAfterSale { get; set; }
    }
}