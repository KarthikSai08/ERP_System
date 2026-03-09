using ERP_System.Domain.Exceptions;
using System;
using System.Collections.Generic;

using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Stock
    {
        public int StockId { get; set; }
        public int ProductId { get; set; }
        public int WarehouseId { get; set; }

        public int Quantity { get; set; }

        public int ReorderLevel { get; set; }
        public DateTime LastUpdated { get; set; }

        public Product? Product { get; set; }
        public Warehouse Warehouse { get; set; }

        private Stock() { }

        public static Stock Create(int prdId, int warehouseId, int qty, int reorderLevel = 10) => new()
        {
            ProductId = prdId,
            WarehouseId = warehouseId,
            Quantity = qty,
            ReorderLevel = reorderLevel,
            LastUpdated = DateTime.Now
        };

        public void AddStock(int qty)
        {
            if(qty <= 0) throw new ValidationException("Quantity must be greater than Zero");

            Quantity += qty;
            LastUpdated = DateTime.Now;
        }

        public void DeductStock(int qty) 
        {
            if(qty <= 0) throw new ValidationException("Quantity must be greater than Zero");
            if (qty > Quantity)
                throw new InsufficientStockException(Product?.ProductName ?? "Product", qty, Quantity);

            Quantity -= qty;
            LastUpdated = DateTime.Now;
        }

        public bool IsLowstock => Quantity <= ReorderLevel;
        internal void SetProduct(Product p) => Product = p;
        internal void SetWarehouse(Warehouse w) => Warehouse = w;
    }
}
