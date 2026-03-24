using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<Warehouse?> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<Warehouse>> GetAllAsync(CancellationToken ct);
        Task<int> AddAsync(Warehouse warehouse, CancellationToken ct);
        Task UpdateAsync(Warehouse warehouse, CancellationToken ct);
        Task<bool> WarehouseExistsAsync(string name, CancellationToken ct);
    }
}
