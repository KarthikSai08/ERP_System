using ERP_System.Domain.Entities;
using ERP_System.Domain.Exceptions;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly AppDbContext _context;

        public WarehouseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Warehouse warehouse, CancellationToken ct)
        {
            await _context.Warehouses.AddAsync(warehouse);
            await _context.SaveChangesAsync();
            return warehouse.WarehouseId;
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync(CancellationToken ct)
        {
            var res = await _context.Warehouses.AsNoTracking().ToListAsync();
            return res;
        }

        public async Task<Warehouse?> GetByIdAsync(int id, CancellationToken ct)
        {
            var res = await _context.Warehouses.FirstOrDefaultAsync(w => w.WarehouseId == id)
                ?? throw new NotFoundException("Warehouse", id);
            return res;
        }
        public async Task<bool> WarehouseExistsAsync(string name, CancellationToken ct)
            => await _context.Warehouses.AnyAsync(s => s.WarehouseName == name, ct);
        public async Task UpdateAsync(Warehouse warehouse, CancellationToken ct)
        {
            _context.Warehouses.Update(warehouse);
            await _context.SaveChangesAsync();
        }
    }
}
