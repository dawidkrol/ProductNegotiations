using ProductNegotiations.Library.Helpers;
using ProductNegotiations.Library.Models;

namespace ProductNegotiations.Library.Services
{
    public interface IProductService
    {
        Task<ProductModel> CreateProductAsync(ProductModel product);
        Task DeleteProduct(ProductModel product);
        Task<PagedList<ProductModel>> GetAllProductsAsync(PagingModel paging);
        Task<ProductModel> GetProductByIdAsync(Guid id);
        Task UpdateProduct(ProductModel product);
    }
}