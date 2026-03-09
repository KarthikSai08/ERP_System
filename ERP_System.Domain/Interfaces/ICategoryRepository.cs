using ERP_System.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP_System.Domain.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetByIdAsync(int id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<bool> ExistsByNameAsync(string name);
        Task<int> AddAsync(Category category);
        Task UpdateAsync(Category category);
    }
}
