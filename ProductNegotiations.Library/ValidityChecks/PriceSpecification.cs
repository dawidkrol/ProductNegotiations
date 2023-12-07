using Microsoft.Extensions.Logging;
using ProductNegotiations.Library.Models;
using ProductNegotiations.Library.Services;

namespace ProductNegotiations.Library.ValidityChecks
{
    public class PriceSpecification : ISpecification<NegotiationModel>
    {
        private readonly ILogger _logger;
        private readonly IProductService _productService;
        private readonly INegotiaitionService _negotiaitionService;
        private readonly int _maxTimesLowerPrice;

        public PriceSpecification(ILogger logger, IProductService productService, INegotiaitionService negotiaitionService, int maxTimesLowerPrice)
        {
            _logger = logger;
            _productService = productService;
            _negotiaitionService = negotiaitionService;
            _maxTimesLowerPrice = maxTimesLowerPrice;
        }
        public async Task<bool> IsSatisfied(NegotiationModel entity)
        {
            var productPrice = _productService.GetProductByIdAsync(entity.Product.Id).GetAwaiter().GetResult().Price;
            if (productPrice / _maxTimesLowerPrice > entity.ProposedPrice)
            {
                _logger.LogDebug("Proposed price exceeds {productPrice} times less price of the product, the proposal is rejected", productPrice);

                entity.IsNegotiationResolved = true;
                entity.Decision = false;
                entity.DecisionDescription = "Proposed price exceeds twice the base price of the product";
            }
            return true;
        }
    }
}
