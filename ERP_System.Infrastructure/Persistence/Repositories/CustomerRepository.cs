using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;
        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Customer customer, CancellationToken ct)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return customer.CustomerId;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
            => await _context.Customers.AnyAsync(c => c.Email == email.ToLower());

        public async Task<IEnumerable<Customer>> GetAllAsync(CancellationToken ct)
            => await _context.Customers.AsNoTracking().ToListAsync();

        public async Task<Customer?> GetByEmailAsync(string email, CancellationToken ct)
            =>await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Email == email.ToLower());

        public async Task<Customer?> GetByIdAsync(int id, CancellationToken ct)
            => await _context.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == id);
        
        public async Task UpdateAsync(Customer customer, CancellationToken ct)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }
    }
}
