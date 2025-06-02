using System.Collections.Generic;
using SalesPilot.Api.Features.Sales.Models;

namespace SalesPilot.Api.Features.Stores.Models
{
    public class Store
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}