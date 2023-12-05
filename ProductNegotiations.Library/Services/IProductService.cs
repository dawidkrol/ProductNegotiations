using ProductNegotiations.Library.Models;

namespace ProductNegotiations.Library.Services
{
    public interface IProductService
    {
        Task CreateProductAsync(ProductModel product);
        Task DeleteProduct(ProductModel product);
        Task<IEnumerable<ProductModel>> GetAllProductsAsync();
        Task<ProductModel> GetProductByIdAsync(Guid id);
        Task UpdateProduct(ProductModel product);
    }
}