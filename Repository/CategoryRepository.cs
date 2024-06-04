using Entities.Models;
using Microsoft.EntityFrameworkCore;


namespace Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Store325953446Context _store325953446Context;

        public CategoryRepository(Store325953446Context store325953446Context)
        {
            _store325953446Context = store325953446Context;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _store325953446Context.Categories.ToListAsync();
        }
    }
}
