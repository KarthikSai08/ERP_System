using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task<Order?> GetByIdAsync(int id, CancellationToken ct);
        Task<Order?> GetByIdWithItemsAsync(int id, CancellationToken ct);
        Task<IEnumerable<Order>> GetAllAsync(CancellationToken ct);
        Task<IEnumerable<Order>> GetByCustomerAsync(int customerId, CancellationToken ct);
        Task<int> AddAsync(Order order, CancellationToken ct);
        Task UpdateAsync(Order order, CancellationToken ct);
        Task AddItemAsync(OrderItem item, CancellationToken ct);

    }
}
