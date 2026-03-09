using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Category
    {
        public int CategoryId { get; private set; }
        public string CategoryName { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public ICollection<Product> Products { get; private set; } = new List<Product>();

        private Category() { }

        public static Category Create(string name, string? description = null) => new Category()
        {
            CategoryName = name,
            Description = description,
            IsActive = true,
            CreatedAt = DateTime.Now

        };

        public void Update(string name, string? description)
        {
            CategoryName = name;
            Description = description;
        }
        public void Deactivate() => IsActive = false;
    }
}
