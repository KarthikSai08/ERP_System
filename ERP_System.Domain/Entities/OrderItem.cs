using ERP_System.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal TotalPrice { get; private set; }

        public Order? Order { get; private set; }
        public Product? Product { get; private set; }

        private OrderItem() { }

        public static OrderItem Create(int orderId, int productId, int qty, decimal unitPrice)
        {
            if (qty <= 0) throw new ValidationException("Quantity must be positive.");
            if (unitPrice <= 0) throw new ValidationException("Unit price must be positive.");
            return new OrderItem
            {
                OrderId = orderId,
                ProductId = productId,
                Quantity = qty,
                UnitPrice = unitPrice,
                TotalPrice = qty * unitPrice
            };
        }
    }
}
