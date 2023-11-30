using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public class ProductDBService : IProductDBService
    {
        //TODO: implement paging
        public ProductDBService() { }
        public async Task<ProductDbModel> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDbModel>> GetAllProductsAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        //TODO: implement filter
        //public async Task<IEnumerable<ProductDbModel>> GetProductsByFilterAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task CreateProductAsync(ProductDbModel product)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateProduct(ProductDbModel product)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteProduct(ProductDbModel product)
        {
            throw new NotImplementedException();
        }
    }
}
