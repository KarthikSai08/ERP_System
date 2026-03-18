using ERP_System.Domain.Entities;
using ERP_System.Domain.Interfaces;
using ERP_System.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;


namespace ERP_System.Infrastructure.Persistence.Repositories
{
        public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) => _context = context;

        public async Task<int> AddAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category.CategoryId;
        }

        public async Task<bool> ExistsByNameAsync(string name)
            => await _context.Categories.AnyAsync(c => c.CategoryName == name);

        public async Task<IEnumerable<Category>> GetAllAsync()
            => await _context.Categories.AsNoTracking().ToListAsync();

        public async Task<Category?> GetByIdAsync(int id)
            => await _context.Categories.AsNoTracking().FirstOrDefaultAsync(c => c.CategoryId == id);
        public async Task UpdateAsync(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }
    }
}
