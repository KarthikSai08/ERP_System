using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly AppDbContext _context;

        public SupplierRepository(AppDbContext context)
            => _context = context;

        public async Task<int> AddAsync(Supplier supplier, CancellationToken ct)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
            return supplier.SupplierId;
        }

        public async Task<bool> EmailExistsAsync(string email, CancellationToken ct)
            => await _context.Suppliers.AnyAsync(s => s.Email == email, ct);

        public async Task<IEnumerable<Supplier>> GetAllAsync(CancellationToken ct)
            =>  await _context.Suppliers.AsNoTracking().ToListAsync();


        public async Task<Supplier?> GetByIdAsync(int id, CancellationToken ct)
            =>await _context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == id);

        public async Task UpdateAsync(Supplier supplier, CancellationToken ct)
        {
            _context.Suppliers.Update(supplier);
            await _context.SaveChangesAsync(ct);

        }
    }
}
