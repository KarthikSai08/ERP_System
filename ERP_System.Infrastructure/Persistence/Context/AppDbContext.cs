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
        public DbSet<Warehouse> Warehouses {  get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Supplier> Suppliers{ get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(c =>
            {
                c.HasKey(c => c.CategoryId);
                c.Property(c => c.CategoryName).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.HasKey(c => c.ProductId);
                p.Property(p => p.ProductName).IsRequired().HasMaxLength(150);
                p.Property(p => p.SKU).IsRequired().HasMaxLength(50);
                p.HasIndex(p => p.SKU).IsUnique();
                p.Property(e => e.IsActive).HasColumnName("IsActive");
                p.Property(p => p.Description).HasMaxLength(500).IsRequired(false);
                p.Property(p => p.Price).HasColumnType("decimal(10,2)");
                p.Property(p => p.CostPrice).HasColumnType("decimal(10,2)");
                p.HasOne(p => p.Category).WithMany(p => p.Products).HasForeignKey(p => p.CategoryId);
            });

            modelBuilder.Entity<Stock>(s =>
            {
                s.HasKey(s => s.StockId);
                s.HasIndex(s => new { s.ProductId, s.WarehouseId }).IsUnique();
                s.HasOne(s => s.Product).WithMany(p => p.Stocks).HasForeignKey(s => s.ProductId);
                s.HasOne(s => s.Warehouse).WithMany(w => w.Stocks).HasForeignKey(s => s.WarehouseId);
            });

            modelBuilder.Entity<Warehouse>(w =>
            {
                w.HasKey(w => w.WarehouseId);
                w.Property(w => w.WarehouseName).IsRequired().HasMaxLength(100);
                w.HasIndex(w => w.WarehouseName).IsUnique(); 
            });

            modelBuilder.Entity<Order>(o =>
            {
                o.HasKey(o => o.OrderId);
                o.Property(o => o.OrderNumber).IsRequired().HasMaxLength(50);
                o.HasIndex(o => o.OrderNumber).IsUnique();
                o.Property(o => o.TotalAmount).HasColumnType("decimal(10,2)");
                o.Property(o => o.Status).HasConversion<int>();
                o.HasOne(o => o.Customer).WithMany(p => p.Orders).HasForeignKey(o => o.CustomerId);
            });

            modelBuilder.Entity<OrderItem>(oi =>
            {
                oi.HasKey(oi => oi.OrderItemId);
                oi.Property(oi => oi.UnitPrice).HasColumnType("decimal(10,2)");
                oi.Property(oi => oi.TotalPrice).HasColumnType("decimal(10,2)");
                oi.HasOne(oi => oi.Order).WithMany(o => o.Items).HasForeignKey(oi => oi.OrderId);
                oi.HasOne(oi => oi.Product).WithMany().HasForeignKey(oi => oi.ProductId);
            });

            modelBuilder.Entity<Employee>(e =>
            {
                e.HasKey(e => e.EmployeeId);
                e.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
                e.Property(e => e.LastName).IsRequired().HasMaxLength(50);
                e.Property(e => e.Email).IsRequired().HasMaxLength(50);
                e.HasIndex(e => e.EmployeeId).IsUnique();
                e.Property(e => e.Salary).HasColumnType("decimal(10,2)");
                e.Property(e => e.Status).HasConversion<int>();
            });


            modelBuilder.Entity<Supplier>(s =>
            {
                s.HasKey(s => s.SupplierId);
                s.Property(s => s.SupplierName).IsRequired().HasMaxLength(50);
                s.Property(s => s.Email).IsRequired().HasMaxLength(50);
                s.HasIndex(s => s.Email).IsUnique();
            });
       }
    }
}
