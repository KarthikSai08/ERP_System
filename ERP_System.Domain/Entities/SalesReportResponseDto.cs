using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{
    public class SalesReportResponseDto
    {
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public int TotalQuantitySold { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalProfit { get; set; }
    }
}
