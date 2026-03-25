using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByProductAndWarehouseAsync(int productId, int warehouseId, CancellationToken ct);
        Task<IEnumerable<Stock>> GetByProductAsync(int productId, CancellationToken ct);
        Task<IEnumerable<Stock>> GetLowStockItemsAsync(CancellationToken ct);
        Task<int> GetTotalStockAsync(int productId, CancellationToken ct);
        Task<int> AddAsync(Stock stock, CancellationToken ct);
        Task UpdateAsync(Stock stock, CancellationToken ct);
        Task<Dictionary<int, int>> GetStockByProductIdsAsync(List<int> productIds, CancellationToken ct);
    }

}
