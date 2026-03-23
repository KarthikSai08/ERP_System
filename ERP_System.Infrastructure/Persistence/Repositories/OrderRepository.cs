using Dapper;
using ERP_System.Application.DTOs;
using ERP_System.Application.Features.Orders.Queries.GetSalesReport;
using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly DapperContext _dapper;
        public async Task<int> AddAsync(Order order, CancellationToken ct)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order.OrderId;
        }

        public async Task AddItemAsync(OrderItem item, CancellationToken ct)
        {
            _context.OrderItems.Add(item);
            await _context.SaveChangesAsync();  
        }

        public async Task<IEnumerable<Order>> GetAllAsync(CancellationToken ct)
            =>  await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items).ThenInclude(i => i.Product)
                .AsNoTracking()
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        

        public async Task<IEnumerable<Order>> GetByCustomerAsync(int customerId, CancellationToken ct)
            => await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items)
                .AsNoTracking()
                .Where(o => o.CustomerId == customerId)
                .ToListAsync();

        public async Task<Order?> GetByIdAsync(int id, CancellationToken ct)
            => await _context.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.OrderId == id) ;


        public async Task<Order?> GetByIdWithItemsAsync(int id, CancellationToken ct)
            => await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Items).ThenInclude(i => i.Product)
                .AsNoTracking()
                .FirstOrDefaultAsync(o => o.OrderId == id);

        public async Task UpdateAsync(Order order, CancellationToken ct)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<SalesReportResponseDto>> GetSalesReportAsync(DateTime from, DateTime to)
        {
            using var con = _dapper.CreateConnection();
            var sql = @"
            SELECT
                p.Name  AS ProductName,
                p.SKU   AS SKU,
                SUM(oi.Quantity)                                   AS TotalQuantitySold,
                SUM(oi.TotalPrice)                                 AS TotalRevenue,
                SUM(oi.TotalPrice - (p.CostPrice  \\\\\\\* oi.Quantity))   AS TotalProfit
            FROM OrderItems oi
            INNER JOIN Products p ON oi.ProductId = p.Id
            INNER JOIN Orders   o ON oi.OrderId   = o.Id
            WHERE o.OrderDate BETWEEN @From AND @To
              AND o.Status NOT IN (5)
            GROUP BY p.Name, p.SKU
            ORDER BY TotalRevenue DESC";

            return await con.QueryAsync<SalesReportResponseDto>(sql, new { From = from, To = to });
        }

    }
}
