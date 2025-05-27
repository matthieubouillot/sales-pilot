using Xunit;
using SalesPilot.Api.Features.Stores.Services;
using SalesPilot.Api.Features.Stores.Models;
using SalesPilot.Api.Features.Stores.Dtos;
using SalesPilot.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SalesPilot.Api.Tests.Features.Stores
{
    public class StoreServiceTests
    {
        private SalesPilotDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<SalesPilotDbContext>()
                .UseInMemoryDatabase(databaseName: "TestSalesPilotDb")
                .Options;
            return new SalesPilotDbContext(options);
        }

        [Fact]
        public async Task CreateAsync_ShouldCreateStore()
        {
            var context = GetDbContext();
            var service = new StoreService(context);
            var dto = new CreateStoreDto { Name = "Test Store", City = "Lyon" };

            var result = await service.CreateAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("Test Store", result.Name);
            Assert.Equal("Lyon", result.City);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsNull_WhenStoreNotFound()
        {
            var context = GetDbContext();
            var service = new StoreService(context);

            var result = await service.GetByIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateStore()
        {
            var context = GetDbContext();
            var service = new StoreService(context);
            var dto = new CreateStoreDto { Name = "Old", City = "Paris" };
            var created = await service.CreateAsync(dto);

            var updateDto = new UpdateStoreDto { Name = "New", City = "Marseille" };
            var updated = await service.UpdateAsync(created.Id, updateDto);

            Assert.True(updated);
            var store = await service.GetByIdAsync(created.Id);
            Assert.Equal("New", store.Name);
            Assert.Equal("Marseille", store.City);
        }

        [Fact]
        public async Task DeleteAsync_ShouldRemoveStore()
        {
            var context = GetDbContext();
            var service = new StoreService(context);
            var dto = new CreateStoreDto { Name = "Test", City = "Lyon" };
            var created = await service.CreateAsync(dto);

            var deleted = await service.DeleteAsync(created.Id);
            Assert.True(deleted);
            var store = await service.GetByIdAsync(created.Id);
            Assert.Null(store);
        }
    }
}