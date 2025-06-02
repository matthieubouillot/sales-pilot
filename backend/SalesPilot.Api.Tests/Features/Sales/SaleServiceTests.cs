using Xunit;
using SalesPilot.Api.Features.Sales.Services;
using SalesPilot.Api.Features.Sales.Models;
using SalesPilot.Api.Features.Sales.Dtos;
using SalesPilot.Api.Data;
using SalesPilot.Api.Features.Stores.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;

namespace SalesPilot.Api.Tests.Features.Sales
{
    public class SaleServiceTests
    {
        private SalesPilotDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SalesPilotDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // base isolée à chaque test
                .Options;
            var context = new SalesPilotDbContext(options);

            // Seed un store pour FK
            context.Stores.Add(new Store { Id = 1, Name = "Magasin test", City = "Lyon" });
            context.SaveChanges();
            return context;
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateSale()
        {
            var context = GetDbContext();
            var service = new SaleService(context);

            var dto = new CreateSaleDto
            {
                StoreId = 1,
                Date = DateTime.Today,
                Product = "Produit test",
                Quantity = 10,
                Amount = 2.5M,
                StockAfterSale = 90
            };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("Produit test", result.Product);
            Assert.Equal(10, result.Quantity);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenSaleNotFound()
        {
            var context = GetDbContext();
            var service = new SaleService(context);

            var result = await service.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateSale()
        {
            var context = GetDbContext();
            var service = new SaleService(context);

            var dto = new CreateSaleDto
            {
                StoreId = 1,
                Date = DateTime.Today,
                Product = "Ancien produit",
                Quantity = 3,
                Amount = 5,
                StockAfterSale = 97
            };
            var created = await service.CreateAsync(dto);

            var updateDto = new UpdateSaleDto
            {
                Date = DateTime.Today.AddDays(1),
                Product = "Nouveau produit",
                Quantity = 5,
                Amount = 10,
                StockAfterSale = 92
            };

            var updated = await service.UpdateAsync(created.Id, updateDto);

            Assert.True(updated);
            var sale = await service.GetByIdAsync(created.Id);
            Assert.Equal("Nouveau produit", sale.Product);
            Assert.Equal(5, sale.Quantity);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveSale()
        {
            var context = GetDbContext();
            var service = new SaleService(context);

            var dto = new CreateSaleDto
            {
                StoreId = 1,
                Date = DateTime.Today,
                Product = "Produit à supprimer",
                Quantity = 2,
                Amount = 3,
                StockAfterSale = 98
            };
            var created = await service.CreateAsync(dto);

            var deleted = await service.DeleteAsync(created.Id);
            Assert.True(deleted);

            var sale = await service.GetByIdAsync(created.Id);
            Assert.Null(sale);
        }
    }
}