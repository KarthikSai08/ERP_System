using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetByIdAsync(int id, CancellationToken ct);
        Task<Customer?> GetByEmailAsync(string email, CancellationToken ct);
        Task<IEnumerable<Customer>> GetAllAsync(CancellationToken ct);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct);
        Task<int> AddAsync(Customer customer, CancellationToken ct);
        Task UpdateAsync(Customer customer, CancellationToken ct);
    }
}
