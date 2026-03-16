using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{
    public class ProductResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string SKU { get; set; } = default!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public decimal MarginPercent { get; set; } 
        public string CategoryName { get; set; } = default!;
        public bool IsActive { get; set; }
        public int TotalStock { get; set; }
    }
}
