using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Application.DTOs
{
    public class WarehouseResponseDto
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }
    }
}
