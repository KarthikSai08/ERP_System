using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        private readonly string _conn;

        public ProductRepository(AppDbContext context, string conn)
        {
            _context = context;
            _conn = conn;
        }

        public async Task<int> AddAsync(Product product,CancellationToken ct)
        {
            await _context.Products.AddAsync(product,ct);
            await _context.SaveChangesAsync();
            return product.ProductId;

        }

        public Task DeleteAsync(int id,CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetByIdAsync(int id,CancellationToken ct  )
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> SearchAsync(string? name, int? categoryId, decimal? maxPrice,CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SkuExistAsync(string sku, CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Product product, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
