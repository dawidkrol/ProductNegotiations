using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public interface INegotiationDBService
    {
        Task CreateNegotiationAsync(NegotiationDbModel negotiationDbModel);
        Task DeleteNegotiationAsync(NegotiationDbModel negotiationDbModel);
        Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsAsync();
        Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsByUserIdAsync(Guid userId);
        Task<NegotiationDbModel> GetNegotiationByIdAsync(Guid id);
        Task<int> GetNegotiationsAmount(Guid productId, Guid userId);
        Task<IEnumerable<NegotiationDbModel>> GetResolvedNegotiationsByUserIdAsync(Guid userId);
        Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsAsync();
        Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsByUserIdAsync(Guid userId);
        Task UpdateNegotiationAsync(NegotiationDbModel negotiationDbModel);
    }
}