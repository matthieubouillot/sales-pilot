using Microsoft.EntityFrameworkCore;
using SalesPilot.Api.Features.Stores.Models;
using SalesPilot.Api.Features.Sales.Models;

namespace SalesPilot.Api.Data
{
    public class SalesPilotDbContext : DbContext
    {
        public SalesPilotDbContext(DbContextOptions<SalesPilotDbContext> options) : base(options) { }

        public DbSet<Store> Stores { get; set; }
        public DbSet<Sale> Sales { get; set; }
    }
}