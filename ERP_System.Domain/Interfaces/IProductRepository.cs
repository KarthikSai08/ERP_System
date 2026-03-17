using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<Product?> GetByIdAsync(int id, CancellationToken ct);
        Task<Product?> GetByIdsAsync(List<int> id,CancellationToken ct);
        Task<IEnumerable<Product>> GetAllAsync(CancellationToken ct);
        Task<IEnumerable<Product>> SearchAsync(string? name, int? categoryId, decimal? maxPrice,CancellationToken ct);
        Task<bool> SkuExistAsync(string sku, CancellationToken ct);
        Task<int> AddAsync(Product product, CancellationToken ct);
        Task UpdateAsync(Product product, CancellationToken ct);
        Task DeleteAsync(int id, CancellationToken ct);
    }
}
