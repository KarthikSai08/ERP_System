using ERP_System.Domain.Enums;
using ERP_System.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Order
    {
        public int OrderId { get; private set; }
        public string OrderNumber { get; private set; }
        public int CustomerId { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal TotalAmount { get; private set; }
        public string? Notes { get; private set; }
        public DateTime OrderDate { get; private set; }
        public DateTime? UpdatedAt { get; private set; }

        public Customer? Customer { get; private set; }
        public ICollection<OrderItem> Items { get; private set; } = new List<OrderItem>();

        private Order() { }

        public static Order Create(int customerId, string? notes = null) => new()
        {
            OrderNumber = $"ORD-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..6].ToUpper()}",
            CustomerId = customerId,
            Status = OrderStatus.Pending,
            Notes = notes,
            OrderDate = DateTime.UtcNow
        };

        public void RecalculateTotal() => TotalAmount = Items.Sum(i => i.TotalPrice);

        public void Confirm()
        {
            if (Status != OrderStatus.Pending) throw new ValidationException("Only pending orders can be confirmed.");
            if (!Items.Any()) throw new ValidationException("Cannot confirm empty order.");
            Status = OrderStatus.Confirmed; UpdatedAt = DateTime.UtcNow;
        }

        public void Ship()
        {
            if (Status != OrderStatus.Confirmed) throw new ValidationException("Only confirmed orders can be shipped.");
            Status = OrderStatus.Shipped; UpdatedAt = DateTime.UtcNow;
        }

        public void Deliver()
        {
            if (Status != OrderStatus.Shipped) throw new ValidationException("Only shipped orders can be delivered.");
            Status = OrderStatus.Delivered; UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == OrderStatus.Delivered) throw new ValidationException("Cannot cancel a delivered order.");
            Status = OrderStatus.Cancelled; UpdatedAt = DateTime.UtcNow;
        }
    }
}
