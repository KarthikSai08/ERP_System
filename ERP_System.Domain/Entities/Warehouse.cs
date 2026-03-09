using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Warehouse
    {
        public int WarehouseId { get; set; }
        public string WarehouseName { get; set; }
        public string Location { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

        private Warehouse() { }

        public static Warehouse Create(string name, string location) => new()
        {
            WarehouseName = name,
            Location = location,
            IsActive = true
        };

        internal void SetWarehouse(Warehouse w) { }
    }
}
