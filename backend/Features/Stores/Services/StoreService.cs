using Microsoft.EntityFrameworkCore;
using SalesPilot.Api.Data;
using SalesPilot.Api.Features.Stores.Dtos;
using SalesPilot.Api.Features.Stores.Models;

namespace SalesPilot.Api.Features.Stores.Services
{
    public class StoreService
    {
        private readonly SalesPilotDbContext _context;
        public StoreService(SalesPilotDbContext context)
        {
            _context = context;
        }

        public async Task<List<StoreDto>> GetAllAsync()
        {
            return await _context.Stores
                .Select(s => new StoreDto { Id = s.Id, Name = s.Name, City = s.City })
                .ToListAsync();
        }

        public async Task<StoreDto?> GetByIdAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            return store == null ? null : new StoreDto { Id = store.Id, Name = store.Name, City = store.City };
        }

        public async Task<StoreDto> CreateAsync(CreateStoreDto dto)
        {
            var store = new Store { Name = dto.Name, City = dto.City };
            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return new StoreDto { Id = store.Id, Name = store.Name, City = store.City };
        }

        public async Task<bool> UpdateAsync(int id, UpdateStoreDto dto)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return false;
            store.Name = dto.Name;
            store.City = dto.City;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var store = await _context.Stores.FindAsync(id);
            if (store == null) return false;
            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}