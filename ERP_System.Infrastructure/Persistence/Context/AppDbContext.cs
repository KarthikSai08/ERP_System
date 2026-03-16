using ERP_System.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get;  set; }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
