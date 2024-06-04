using Entities.Models;
using Repositories;


namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<List<Product>> GetAllProducts(string? desc, int? minPrice, int? maxPrice, int?[] categoryIds)
        {
            return await _productRepository.GetAllProducts(desc, minPrice,maxPrice,categoryIds);
        }
    }
}
