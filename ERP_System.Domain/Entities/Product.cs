using ERP_System.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace ERP_System.Domain.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SKU { get; set; }
        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal CostPrice { get; set; }
        public int CategoryId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public Category? Category { get; set; }
        public ICollection<Stock> Stocks { get; set; } = new List<Stock>();

        private Product() { }

        public static Product Create(string name , string sku, decimal price,decimal costPrice, int categoryId, string? description = null)
        {
            if (price <= 0) throw new ValidationException("Price must be greater than Zero");
            if (costPrice < 0) throw new ValidationException("Cost Price cannot be Negative");

            return new Product
            {
                ProductName = name,
                SKU = sku,
                Price = price,
                CostPrice = costPrice,
                CategoryId = categoryId,
                Description = description,
                IsActive = true,
                CreatedAt = DateTime.Now
            };
        }
        public void Update(string name, string? description, decimal price,decimal costPrice)
        {
            if(price <= 0) throw new ValidationException("Price must be greater than Zero");

            ProductName = name;
            Description = description;
            Price = price;
            CostPrice = costPrice;
            UpdatedAt = DateTime.Now;
        }

        public void Deactivate() { IsActive = false; UpdatedAt = DateTime.Now; }
        internal void SetCategory(Category c) => Category = c;  
    }
}
