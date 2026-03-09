using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Customer
    {
        public int CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string? Address { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public ICollection<Order> Orders { get; private set; } = new List<Order>();

        private Customer() { }

        public static Customer Create(string name, string email, string phone, string? address = null) => new()
        {
            CustomerName = name,
            Email = email.ToLower().Trim(),
            Phone = phone,
            Address = address,
            IsActive = true,
            CreatedAt = DateTime.Now
        };

        public void Update(string name, string email, string phone, string? address)
        {
            CustomerName = name;
            Email = email.ToLower().Trim();
            Phone = phone;
            Address = address;
        }

        public void Deactivate() => IsActive = false;
    }
}
