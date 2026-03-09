using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Supplier
    {
        public int SupplierId { get; private set; }
        public string SupplierName { get; private set; }
        public string ContactPerson { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string? Address { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        private Supplier() { }

        public static Supplier Create(string name, string contactPerson,
            string email, string phone, string? address = null) => new()
            {
                SupplierName = name,
                ContactPerson = contactPerson,
                Email = email.ToLower().Trim(),
                Phone = phone,
                Address = address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

        public void Update(string name, string contactPerson, string email, string phone, string? address)
        {
            SupplierName = name;
            ContactPerson = contactPerson;
            Email = email.ToLower().Trim();
            Phone = phone;
            Address = address;
        }

        public void Deactivate() => IsActive = false;
    }
}
