using Microsoft.Extensions.Logging;
using ProductNegotiations.Library.Models;
using ProductNegotiations.Library.Services;

namespace ProductNegotiations.Library.ValidityChecks
{
    public class AttemptSpecification : ISpecification<NegotiationModel>
    {
        private readonly INegotiaitionService _negotiaitionService;
        private readonly int _atttepmpts;
        private readonly ILogger _logger;

        public AttemptSpecification(ILogger logger, IProductService productService, INegotiaitionService negotiaitionService, int atttepmpts)
        {
            _negotiaitionService = negotiaitionService;
            _atttepmpts = atttepmpts;
            _logger = logger;
        }
        public async Task<bool> IsSatisfied(NegotiationModel entity)
        {
            int negotiationTrials = await _negotiaitionService.GetResolvedNegotiationsByUserIdAndProductAsync(entity.Product.Id, entity.UserId);
            if (_atttepmpts > negotiationTrials)
            {
                _logger.LogDebug("We found at least {_atttepmpts} price negotiation attempts for product: {entity.Product.Id} from the user: {entity.UserId}", _atttepmpts, entity.Product.Id, entity.UserId);
                return false;
            }
            return true;
        }
    }
}
