using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private readonly IDbContextTransaction _transaction;

        public UnitOfWork(AppDbContext context) => _context = context;
        public async Task BeginTransactionAsync()
            => await _context.Database.BeginTransactionAsync();

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }

        public void Dispose()
            => _transaction?.Dispose();

        public async Task RollbackAsync()
            => await _transaction.RollbackAsync();  

        public async Task<int> SaveChangesAsync()
            => await _context.SaveChangesAsync();
    }
}
