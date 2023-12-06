using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public interface INegotiationDBService
    {
        Task CreateNegotiationAsync(NegotiationDbModel negotiationDbModel);
        Task DeleteNegotiationAsync(NegotiationDbModel negotiationDbModel);
        Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsAsync();
        Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsByUserIdAsync(string userId);
        Task<NegotiationDbModel> GetNegotiationByIdAsync(Guid id);
        Task<int> GetNegotiationsAmount(Guid productId, string userId);
        Task<int> GetResolvedNegotiationsByUserIdAndProductAsync(Guid productId, string userId);
        Task<IEnumerable<NegotiationDbModel>> GetResolvedNegotiationsByUserIdAsync(string userId);
        Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsAsync();
        Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsByUserIdAsync(string userId);
        Task UpdateNegotiationAsync(NegotiationDbModel negotiationDbModel);
        Task<bool> IsUnresolvedNegotiationByProductAndUser(Guid productId, string userId);
        Task<bool> IsAlreadyAcceptedNegotiationByProductAndUser(Guid productId, string userId);
    }
}