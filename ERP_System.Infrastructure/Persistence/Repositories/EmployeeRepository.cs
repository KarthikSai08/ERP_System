using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;
        public EmployeeRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(Employee employee, CancellationToken ct)
        {
           _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return employee.EmployeeId;
        }
        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
        {
            return await _context.Employees.AnyAsync(e => e.Email ==  email,ct);
        }
        public async Task<IEnumerable<Employee>> GetAllAsync(CancellationToken ct)
            => await _context.Employees.AsNoTracking().ToListAsync();   

        public async Task<IEnumerable<Employee>> GetByDepartmentAsync(string department, CancellationToken ct)
            =>await _context.Employees.AsNoTracking().Where(e => e.Department == department).ToListAsync();

        public async Task<Employee?> GetByIdAsync(int id, CancellationToken ct)
            => await _context.Employees.AsNoTracking().FirstOrDefaultAsync(e => e.EmployeeId == id);
        public async Task UpdateAsync(Employee employee, CancellationToken ct)
        {
            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();
        }
    }
}
