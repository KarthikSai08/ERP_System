using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken ct);
        Task<bool> EmailExistsAsync(string email,CancellationToken ct);
        Task<int> AddAsync(Supplier supplier, CancellationToken ct);
        Task UpdateAsync(Supplier supplier, CancellationToken ct);
    }

}
