using Microsoft.EntityFrameworkCore;
using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public class ProductDBService : IProductDBService
    {
        private readonly NegotiationDbContext _dbContext;

        //TODO: implement paging
        public ProductDBService(NegotiationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Returns only not deleted products.
        /// </summary>
        private IQueryable<ProductDbModel> geNotDeletedProducts()
        {
            return _dbContext.Products.Where(x => x.IsDeleted == false);
        }
        /// <summary>
        /// Returning product model by id (including deleted).
        /// </summary>
        /// <param name="id">Product id</param>
        public async Task<ProductDbModel> GetProductByIdAsync(Guid id)
        {
            //TODO: Checking errors
            //TODO: logging
            var output = await _dbContext.Products.Where(x => x.Id == id).SingleAsync();
            return output;
        }
        /// <summary>
        /// Returns only not deleted products.
        /// </summary>
        public async Task<IEnumerable<ProductDbModel>> GetAllProductsAsync()
        {
            //TODO: Checking errors
            //TODO: logging
            var output = geNotDeletedProducts();
            return output;
        }

        //TODO: implement filter
        //public async Task<IEnumerable<ProductDbModel>> GetProductsByFilterAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}
        /// <summary>
        /// Creating new product.
        /// </summary>
        /// <param name="product">Model to create</param>
        public async Task CreateProductAsync(ProductDbModel product)
        {
            //TODO: Checking errors
            //TODO: logging
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Updating model, model is selected by id, other properties are changing to given.
        /// </summary>
        public async Task UpdateProduct(ProductDbModel product)
        {
            var data = await _dbContext.Products.SingleAsync(x => x.Id == product.Id);

            data.Name = product.Name;
            data.Description = product.Description;
            data.Price = product.Price;

            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Changing model flag to the deleted.
        /// </summary>
        public async Task DeleteProduct(ProductDbModel product)
        {
            var data = await _dbContext.Products.SingleAsync(x => x.Id == product.Id);

            data.IsDeleted = true;

            await _dbContext.SaveChangesAsync();
        }
    }
}
