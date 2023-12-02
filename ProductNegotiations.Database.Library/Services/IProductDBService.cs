using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public interface IProductDBService
    {
        Task CreateProductAsync(ProductDbModel product);
        Task DeleteProduct(ProductDbModel product);
        Task<IEnumerable<ProductDbModel>> GetAllProductsAsync();
        Task<ProductDbModel> GetProductByIdAsync(Guid id);
        Task UpdateProduct(ProductDbModel product);
    }
}