using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface ISupplierRepository
    {
        Task<Supplier?> GetByIdAsync(int id);
        Task<IEnumerable<Supplier>> GetAllAsync();
        Task<bool> EmailExistsAsync(string email);
        Task<int> AddAsync(Supplier supplier);
        Task UpdateAsync(Supplier supplier);
    }

}
