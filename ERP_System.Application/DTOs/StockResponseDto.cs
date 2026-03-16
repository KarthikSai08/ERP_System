using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{

    public class StockResponseDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; } = default!;
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        public bool IsLowStock { get; set; }
    }

    public class LowStockAlertResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public int CurrentStock { get; set; }
        public int ReorderLevel { get; set; }
        public string WarehouseName { get; set; } = default!;
    }
}
