using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee?> GetByIdAsync(int id);
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<IEnumerable<Employee>> GetByDepartmentAsync(string department);
        Task<bool> EmailExistsAsync(string email);
        Task<int> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
    }

}
