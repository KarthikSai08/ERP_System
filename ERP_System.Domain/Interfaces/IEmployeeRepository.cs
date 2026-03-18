using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int id, CancellationToken ct);
        Task<IEnumerable<Employee>> GetAllAsync(CancellationToken ct);
        Task<IEnumerable<Employee>> GetByDepartmentAsync(string department, CancellationToken ct);
        Task<bool> EmailExistsAsync(string email, CancellationToken ct);
        Task<int> AddAsync(Employee employee, CancellationToken ct);
        Task UpdateAsync(Employee employee, CancellationToken ct);
    }

}
