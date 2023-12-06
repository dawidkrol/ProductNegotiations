using ProductNegotiations.Library.Helpers;
using ProductNegotiations.Library.Models;

namespace ProductNegotiations.Library.Services
{
    public interface INegotiaitionService
    {
        Task AcceptNegotiation(Guid negotiationId, string description);
        Task RefuseNegotiation(Guid negotiationId, string description);
        Task<NegotiationModel> CreateNegotiationAsync(Guid guid, NegotiationModel negotiationModel);
        Task DeleteNegotiationAsync(NegotiationModel negotiationModel);
        Task<PagedList<NegotiationModel>> GetAllNegotiationsAsync(PagingModel paging);
        Task<PagedList<NegotiationModel>> GetAllNegotiationsByUserIdAsync(PagingModel paging, Guid userId);
        Task<NegotiationModel> GetNegotiationByIdAsync(Guid id);
        Task<int?> GetNegotiationsAmount(Guid productId, Guid userId);
        Task<PagedList<NegotiationModel>> GetResolvedNegotiationsByUserIdAsync(PagingModel paging, Guid userId);
        Task<PagedList<NegotiationModel>> GetUnresolvedNegotiationsAsync(PagingModel paging);
        Task<PagedList<NegotiationModel>> GetUnresolvedNegotiationsByUserIdAsync(PagingModel paging, Guid userId);
        Task UpdateNegotiationAsync(NegotiationModel negotiationModel);
        Task<int> GetResolvedNegotiationsByUserIdAndProductAsync(Guid productId, Guid userId);
    }
}