using Mapster;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using ProductNegotiations.Database.Library.Models;
using ProductNegotiations.Database.Library.Services;
using ProductNegotiations.Library.Helpers;
using ProductNegotiations.Library.Models;

namespace ProductNegotiations.Library.Services
{
    public class ProductService : IProductService
    {
        private readonly ILogger<ProductService> _logger;
        private readonly IProductDBService _service;

        public ProductService(ILogger<ProductService> logger, IProductDBService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<ProductModel> GetProductByIdAsync(Guid id)
        {
            try
            {
                _logger.LogTrace("Getting information about product: {id}", id);

                var data = await _service.GetProductByIdAsync(id);
                return data?.Adapt<ProductModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<PagedList<ProductModel>> GetAllProductsAsync(PagingModel paging)
        {
            try
            {
                _logger.LogTrace("Getting information about all products");

                var data = await _service.GetAllProductsAsync();
                return PagedList<ProductModel>.ToPagedList(data.AsEnumerable(),
                                                           paging.PageNumber,
                                                           paging.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        //TODO: implement filter
        //public async Task<IEnumerable<ProductDbModel>> GetProductsByFilterAsync(Guid id)
        //{
        //    throw new NotImplementedException();
        //}

        public async Task<ProductModel> CreateProductAsync(ProductModel product)
        {
            try
            {
                _logger.LogTrace("Creating new product");

                var data = product.Adapt<ProductDbModel>();

                data.Id = Guid.NewGuid();

                await _service.CreateProductAsync(data);

                return data.Adapt<ProductModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task UpdateProduct(ProductModel product)
        {
            try
            {
                var productId = product.Id;
                _logger.LogTrace("Updating product: {productId}", productId);

                var data = product.Adapt<ProductDbModel>();
                await _service.UpdateProduct(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public async Task DeleteProduct(ProductModel product)
        {
            try
            {
                var productId = product.Id;
                _logger.LogTrace("Deleting product: {productId}", productId);

                var data = product.Adapt<ProductDbModel>();
                await _service.DeleteProduct(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
