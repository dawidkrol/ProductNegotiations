using Mapster;
using Microsoft.Extensions.Logging;
using ProductNegotiations.Database.Library.Models;
using ProductNegotiations.Database.Library.Services;
using ProductNegotiations.Library.Helpers;
using ProductNegotiations.Library.Models;
using ProductNegotiations.Library.ValidityChecks;

namespace ProductNegotiations.Library.Services
{
    public class NegotiaitionService : INegotiaitionService
    {
        private readonly ILogger<NegotiaitionService> _logger;
        private readonly INegotiationDBService _service;
        private readonly IProductService _productService;

        public NegotiaitionService(ILogger<NegotiaitionService> logger, INegotiationDBService service, IProductService productService)
        {
            _logger = logger;
            _service = service;
            _productService = productService;
        }

        public async Task<NegotiationModel> GetNegotiationByIdAsync(Guid id)
        {
            NegotiationDbModel data = null;
            try
            {
                _logger.LogTrace("Getting information about negotation id = {id}", id);

                data = await _service.GetNegotiationByIdAsync(id);
                return data?.Adapt<NegotiationModel>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<PagedList<NegotiationModel>> GetUnresolvedNegotiationsAsync(PagingModel paging)
        {
            try
            {
                _logger.LogTrace("Getting information about unresolved negotation");

                var data = await _service.GetUnresolvedNegotiationsAsync();
                return PagedList<NegotiationModel>.ToPagedList(data.AsEnumerable(),
                                           paging.PageNumber,
                                           paging.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<PagedList<NegotiationModel>> GetAllNegotiationsAsync(PagingModel paging)
        {
            try
            {
                _logger.LogTrace("Getting information about all negotation");

                var data = await _service.GetAllNegotiationsAsync();
                return PagedList<NegotiationModel>.ToPagedList(data.AsEnumerable(),
                           paging.PageNumber,
                           paging.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<int?> GetNegotiationsAmount(Guid productId, Guid userId)
        {
            try
            {
                _logger.LogTrace("Getting information about all negotation about created by user: {userId} about product: {productId}", userId, productId);

                var data = await _service.GetNegotiationsAmount(productId, userId);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<PagedList<NegotiationModel>> GetAllNegotiationsByUserIdAsync(PagingModel paging, Guid userId)
        {
            try
            {
                _logger.LogTrace("Getting information about negotation created by user: {userId}", userId);

                var data = await _service.GetAllNegotiationsByUserIdAsync(userId);
                return PagedList<NegotiationModel>.ToPagedList(data.AsEnumerable(),
                           paging.PageNumber,
                           paging.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<PagedList<NegotiationModel>> GetResolvedNegotiationsByUserIdAsync(PagingModel paging, Guid userId)
        {
            try
            {
                _logger.LogTrace("Getting information about resolved negotiations created by user: {userId}", userId);

                var data = await _service.GetResolvedNegotiationsByUserIdAsync(userId);
                return PagedList<NegotiationModel>.ToPagedList(data.AsEnumerable(),
                           paging.PageNumber,
                           paging.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<PagedList<NegotiationModel>> GetUnresolvedNegotiationsByUserIdAsync(PagingModel paging, Guid userId)
        {
            try
            {
                _logger.LogTrace("Getting information about resolved negotiations created by user: {userId}", userId);

                var data = await _service.GetUnresolvedNegotiationsByUserIdAsync(userId);
                return PagedList<NegotiationModel>.ToPagedList(data.AsEnumerable(),
                           paging.PageNumber,
                           paging.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }
        }

        public async Task<int> GetResolvedNegotiationsByUserIdAndProductAsync(Guid productId, Guid userId)
        {
            return await _service.GetResolvedNegotiationsByUserIdAndProductAsync(productId, userId);
        }

        public async Task<NegotiationModel> CreateNegotiationAsync(Guid userId, NegotiationModel negotiationModel)
        {
            try
            {
                var productId = negotiationModel.Product.Id;

                negotiationModel.Id = Guid.NewGuid();

                _logger.LogTrace("Creating negotiaition model by user: {userId}, about product: {Id}", userId, productId);

                if (await _service.IsUnresolvedNegotiationByProductAndUser(productId, userId))
                {
                    _logger.LogDebug("We found unresolved negotiation for product: {productId} from the user: {userId}", userId, productId);
                    return null;
                }

                var checksResult = negotiationModel.Check(_logger, this, _productService, x => new CheckValues() { MaxAttempts = 3, MaxTimesLowerPrice = 2 });
                if (!checksResult)
                {
                    return null;
                }

                var data = negotiationModel.Adapt<NegotiationDbModel>();

                var product = await _productService.GetProductByIdAsync(productId);

                await _service.CreateNegotiationAsync(data);

                _logger.LogDebug("New negotiation created");

                return negotiationModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return null;
            }

        }

        public async Task AcceptNegotiation(Guid negotiationId, string description)
        {
            try
            {
                var data = await _service.GetNegotiationByIdAsync(negotiationId);
                data.IsNegotiationResolved = true;
                data.Decision = true;
                data.DecisionDescription = description;

                await _service.UpdateNegotiationAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public async Task RefuseNegotiation(Guid negotiationId, string description)
        {
            try
            {
                var data = await _service.GetNegotiationByIdAsync(negotiationId);
                data.IsNegotiationResolved = true;
                data.Decision = false;
                data.DecisionDescription = description;

                await _service.UpdateNegotiationAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public async Task UpdateNegotiationAsync(NegotiationModel negotiationModel)
        {
            try
            {
                var data = negotiationModel.Adapt<NegotiationDbModel>();
                await _service.UpdateNegotiationAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        public async Task DeleteNegotiationAsync(NegotiationModel negotiationModel)
        {
            try
            {
                var data = negotiationModel.Adapt<NegotiationDbModel>();
                await _service.DeleteNegotiationAsync(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }
    }
}
