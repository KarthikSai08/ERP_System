using Dapper;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapperContext;

        public ProductRepository(AppDbContext context,DapperContext dapperContext)
        {
            _context = context;
            _dapperContext = dapperContext;
        }

        public async Task<int> AddAsync(Product product,CancellationToken ct)
        {

            if (product.Category != null)
                _context.Entry(product.Category).State = EntityState.Unchanged;

            await _context.Products.AddAsync(product, ct);
            await _context.SaveChangesAsync();
            return product.ProductId;

        }

        public async Task DeleteAsync(int id,CancellationToken ct)
        {
            var p = await _context.Products.FindAsync(id);
            if(p != null)
            {
                _context.Products.Remove(p);
                await _context.SaveChangesAsync(ct);
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct)
        {
            using var con = _dapperContext.CreateConnection();

            var sql = @"SELECT p.*, c.CategoryName, c.Description, c.IsActive
                        FROM Products p
                        LEFT JOIN Categories c On p.CategoryId = c.CategoryId
                        WHERE  p.IsActive = 1
                        ORDER BY p.ProductName";

            var result = await con.QueryAsync<Product, Category, Product>(
                sql,
                (product, cat) =>
                {
                    product.SetCategory(cat);
                    return product;

                },
                splitOn: "CategoryId");

            return result;
        }

        public async Task<Product?> GetByIdAsync(int id,CancellationToken ct)
        {
            using var con = _dapperContext.CreateConnection();
            var sql = @"SELECT p.*, c.CategoryName, c.Description, c.IsActive
                        FROM Products p
                        LEFT JOIN Categories c On p.CategoryId = c.CategoryId
                        WHERE p.ProductId = @Id";

            var result = await con.QueryAsync<Product, Category, Product>(
                sql,
                (product, cat) =>
                {
                    product.SetCategory(cat);
                    return product;
                },
                new { Id = id },
                splitOn: "ProductId");

            return result.FirstOrDefault();
                        
        }

        public async Task<IEnumerable<Product>> SearchAsync(string? name, int? categoryId, decimal? maxPrice,CancellationToken ct)
        {
            using var con = _dapperContext.CreateConnection();
            var sql = new StringBuilder(@"SELECT p.ProductId, p.ProductName, p.SKU, p.Description,
                p.Price, p.CostPrice, p.IsActive, p.CategoryId,
                c.CategoryId, c.CategoryName, c.Description, c.IsActive
                        FROM Products p
                        LEFT JOIN Categories c On p.CategoryId = c.CategoryId
                        WHERE p.IsActive = 1");

            var param = new DynamicParameters();

            if(!string.IsNullOrEmpty(name))
            {
                sql.Append(" AND p.ProductName Like @Name");
                param.Add("Name", $"%{name}%");
            }
            if (categoryId.HasValue)
            {
                sql.Append(" AND p.CategoryId = @CategoryId");
                param.Add("CategoryId", categoryId.Value);
            }
            if (maxPrice.HasValue)
            {
                sql.Append(" AND p.Price <= @MaxPrice");
                param.Add("MaxPrice",maxPrice.Value);
            }
            sql.Append(" ORDER BY p.ProductName");

            return await con.QueryAsync<Product, Category, Product>(
                sql.ToString(),
                (prd, cat) =>
                {
                    prd.SetCategory(cat);
                    return prd;
                },
                param,
                splitOn: "CategoryId");
        }

        public async Task<bool> SkuExistAsync(string sku, CancellationToken ct)
        {
            using var con = _dapperContext.CreateConnection();

            return await con.QuerySingleOrDefaultAsync<bool>(
                "SELECT CAST(CASE WHEN EXISTS(SELECT 1 FROM Products WHERE SKU =@SKU) THEN 1 ELSE 0 END AS BIT)",
                new { SKU = sku.ToUpper() });
        }

        public async Task UpdateAsync(Product product, CancellationToken ct)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }
    }
}
