using Microsoft.EntityFrameworkCore;
using SalesPilot.Api.Data;
using SalesPilot.Api.Features.Sales.Models;
using SalesPilot.Api.Features.Sales.Dtos;

namespace SalesPilot.Api.Features.Sales.Services
{
    public class SaleService
    {
        private readonly SalesPilotDbContext _context;

        public SaleService(SalesPilotDbContext context)
        {
            _context = context;
        }

        public async Task<List<SaleDto>> GetAllAsync()
        {
            return await _context.Sales
                .Select(s => new SaleDto
                {
                    Id = s.Id,
                    StoreId = s.StoreId,
                    Date = s.Date,
                    Product = s.Product,
                    Quantity = s.Quantity,
                    Amount = s.Amount,
                    StockAfterSale = s.StockAfterSale
                }).ToListAsync();
        }

        public async Task<SaleDto?> GetByIdAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return null;
            return new SaleDto
            {
                Id = sale.Id,
                StoreId = sale.StoreId,
                Date = sale.Date,
                Product = sale.Product,
                Quantity = sale.Quantity,
                Amount = sale.Amount,
                StockAfterSale = sale.StockAfterSale
            };
        }

        public async Task<SaleDto> CreateAsync(CreateSaleDto dto)
        {
            var sale = new Sale
            {
                StoreId = dto.StoreId,
                Date = dto.Date,
                Product = dto.Product,
                Quantity = dto.Quantity,
                Amount = dto.Amount,
                StockAfterSale = dto.StockAfterSale
            };
            _context.Sales.Add(sale);
            await _context.SaveChangesAsync();
            return new SaleDto
            {
                Id = sale.Id,
                StoreId = sale.StoreId,
                Date = sale.Date,
                Product = sale.Product,
                Quantity = sale.Quantity,
                Amount = sale.Amount,
                StockAfterSale = sale.StockAfterSale
            };
        }

        public async Task<SaleDto?> UpdateAsync(int id, UpdateSaleDto dto)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return null;
            sale.Date = dto.Date;
            sale.Product = dto.Product;
            sale.Quantity = dto.Quantity;
            sale.Amount = dto.Amount;
            sale.StockAfterSale = dto.StockAfterSale;
            await _context.SaveChangesAsync();
            return new SaleDto
            {
                Id = sale.Id,
                StoreId = sale.StoreId,
                Date = sale.Date,
                Product = sale.Product,
                Quantity = sale.Quantity,
                Amount = sale.Amount,
                StockAfterSale = sale.StockAfterSale
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var sale = await _context.Sales.FindAsync(id);
            if (sale == null) return false;
            _context.Sales.Remove(sale);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}