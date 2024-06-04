
using Entities.Models;
using Microsoft.EntityFrameworkCore;



namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly Store325953446Context _store325953446Context;
        public ProductRepository(Store325953446Context store325953446Context)
        {
            _store325953446Context = store325953446Context;
        }

        public async Task<List<Product>> GetAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            var query = _store325953446Context.Products.Where(product =>
            (desc == null ? (true) : (product.ProdDescription.Contains(desc)))
            && ((minPrice == null) ? (true) : (product.Price >= minPrice))
            && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
            && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
           .Include(i => i.Category)
           .OrderBy(product => product.Price);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.ToListAsync();
            return products;
        }
        public async Task<List<Product>> GetAllProducts()
        {
            List<Product> products = await _store325953446Context.Products.ToListAsync();
            return products;
        }
    }
}
