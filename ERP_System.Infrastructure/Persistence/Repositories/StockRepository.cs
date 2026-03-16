using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;
        public StockRepository(AppDbContext context)=> _context = context;
        public Task<int> AddAsync(Stock stock, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Stock?> GetByProductAndWarehouseAsync(int productId, int warehouseId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetByProductAsync(int productId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Stock>> GetLowStockItemsAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetTotalStockAsync(int productId, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public async Task<Dictionary<int, int>> GetTotalStockBatchAsync(List<int> productIds, CancellationToken ct)
        {
            return await _context.Stocks
        .Where(s => productIds.Contains(s.ProductId))
        .GroupBy(s => s.ProductId)
        .Select(g => new { g.Key, Total = g.Sum(s => s.Quantity) })
        .ToDictionaryAsync(x => x.Key, x => x.Total, ct);
        }

        public Task UpdateAsync(Stock stock, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
