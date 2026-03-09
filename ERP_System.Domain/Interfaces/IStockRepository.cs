using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface IStockRepository
    {
        Task<Stock?> GetByProductAndWarehouseAsync(int productId, int warehouseId);
        Task<IEnumerable<Stock>> GetByProductAsync(int productId);
        Task<IEnumerable<Stock>> GetLowStockItemsAsync();
        Task<int> GetTotalStockAsync(int productId);
        Task<int> AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
    }

}
