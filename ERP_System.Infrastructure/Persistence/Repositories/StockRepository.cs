using Dapper;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapperContext;
        private readonly IMemoryCache _cache; 
        public StockRepository(AppDbContext context,DapperContext dapperContext, IMemoryCache cache)
        {
            _cache = cache;
            _context = context;
            _dapperContext = dapperContext;
        } 



        public async Task<int> AddAsync(Stock stock, CancellationToken ct)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }


        public async Task<Stock?> GetByProductAndWarehouseAsync(int productId, int warehouseId, CancellationToken ct)
        {
            var cacheKey = $"stock:{productId}:{warehouseId}";

            if(_cache.TryGetValue(cacheKey, out Stock? cachedStock))
            {
                return cachedStock;
            }

            using var con = _dapperContext.CreateConnection();
            var sql = @"
                        SELECT s.*, p.ProductId, p.ProductName, p.SKU, 
                              w.WarehouseId, w.WarehouseName, w.Location
                       FROM Stocks s
                       INNER JOIN Products p ON s.ProductId = p.ProductId
                       INNER JOIN Warehouses w ON s.WarehouseId = w.WarehouseId
                       WHERE s.ProductId = @productId AND s.WarehouseId = @warehouseId";

            var res = await con.QueryAsync<Stock, Product, Warehouse, Stock>(
                sql,
                (s, p, w) =>
                {
                    s.SetProduct(p);
                    s.SetWarehouse(w);
                    return s;
                },
                new { ProductId = productId, WarehouseId = warehouseId },
                splitOn: "ProductId,WarehouseId");

            
            var stock = res.FirstOrDefault();

            var cachedOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2) 
            };
            
            _cache.Set(cacheKey, stock, cachedOptions);

            return stock;

        }

        public async Task<IEnumerable<Stock>> GetByProductAsync(int productId, CancellationToken ct)
        {
            var cacheKey = $"stock:{productId}";

            if (_cache.TryGetValue(cacheKey, out IEnumerable<Stock>? cachedStock))
            { return cachedStock; }

            using var con = _dapperContext.CreateConnection();
            var sql = @"SELECT s.*, p.ProductId, p.ProductName,p.SKU,
                                    w.WarehouseId, w.WarehouseName, w.Location
                         FROM Stocks s
                         INNER JOIN Products p ON s.ProductId = p.ProductId
                         INNER JOIN Warehouses w On s.WarehouseId = w.WarehouseId
                         WHERE s.ProductId = @productId";

            var res = await con.QueryAsync<Stock, Product, Warehouse, Stock>(
                sql,
                (s, p, w) =>
                {
                    s.SetProduct(p);
                    s.SetWarehouse(w);
                    return s;
                },
                new { ProductId = productId },
                splitOn: "ProductId,WarehouseId");

            var stocks = res.ToList();   

            var cachedOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            };

            _cache.Set(cacheKey, stocks, cachedOptions);

            return stocks;
                            
        }

        public async Task<IEnumerable<Stock>> GetLowStockItemsAsync(CancellationToken ct)
        {
            var cacheKey = $"stock:low";

            if (_cache.TryGetValue(cacheKey, out List<Stock>? cachedStock))
                return cachedStock;

            using var con = _dapperContext.CreateConnection();
            var sql = @"SELECT s.*, p.ProductId, p.ProductName,p.SKU,
                                    w.WarehouseId, w.WarehouseName, w.Location
                         FROM Stocks s
                         INNER JOIN Products p ON s.ProductId = p.ProductId
                         INNER JOIN Warehouses w On s.WarehouseId = w.WarehouseId
                         WHERE s.Quantity <= s.ReorderLevel
                         ORDER BY s.Quantity ASC";

            var res = await con.QueryAsync<Stock, Product, Warehouse, Stock>(
                sql,
                (s,p,w) =>
                {
                    s.SetProduct(p);
                    s.SetWarehouse(w);
                    return s;
                },
                splitOn: "ProductId,WarehouseId");

            var stocks = res.ToList();

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            };

            _cache.Set(cacheKey, stocks, cacheOptions);

            return stocks;

        }

        public async Task<int> GetTotalStockAsync(int productId, CancellationToken ct)
        {
            var cacheKey = $"stock:{productId}";

            if (_cache.TryGetValue(cacheKey, out int cachedStock))
                return cachedStock;

            using var con = _dapperContext.CreateConnection();
            var sql = "SELECT ISNULL(SUM(Quantity), 0) FROM Stocks WHERE ProductId = @ProductId";

            var res = await con.QuerySingleAsync<int>(
                sql,
                new { ProductId = productId
                });

            var cacheOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
            };

            _cache.Set(cacheKey, res, cacheOptions);

            return res;
        }

        //public async Task<Dictionary<int, int>> GetTotalStockBatchAsync(List<int> productIds, CancellationToken ct)
        //{
        //    return await _context.Stocks
        //.Where(s => productIds.Contains(s.ProductId))
        //.GroupBy(s => s.ProductId)
        //.Select(g => new { g.Key, Total = g.Sum(s => s.Quantity) })
        //.ToDictionaryAsync(x => x.Key, x => x.Total, ct);
        //}

        public async Task UpdateAsync(Stock stock, CancellationToken ct)
        {
            _context.Stocks.Update(stock);
            await _context.SaveChangesAsync();
        }
    }
}
