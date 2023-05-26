using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Northwind_App.Interfaces.IRepositories;
using Northwind_Core.Domain.Entities;
using Northwind_Infrastructure.Data;

namespace Northwind_Infrastructure.Repositories
{
    public class CategoryRepository : AsyncBaseRepository<Category>, ICategoryRepository
    {
        private readonly NorthwindContext _northwndContext;

        public CategoryRepository(NorthwindContext northwndContext) : base(northwndContext)
        {
            _northwndContext = northwndContext;
        }

        public async Task<IEnumerable<Category>> CategoryNames()
        {
            var categories = await _northwndContext.Categories.Select(category => new Category
            { 
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            }).ToListAsync();

            return categories;
        }
    }
}
