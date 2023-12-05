using ProductNegotiations.Library.Models;

namespace ProductNegotiations.Library.Services
{
    public interface INegotiaitionService
    {
        Task AcceptNegotiation(Guid negotiationId, string description);
        Task RefuseNegotiation(Guid negotiationId, string description);
        Task<bool> CreateNegotiationAsync(Guid guid, NegotiationModel negotiationModel);
        Task DeleteNegotiationAsync(NegotiationModel negotiationModel);
        Task<IEnumerable<NegotiationModel>> GetAllNegotiationsAsync();
        Task<IEnumerable<NegotiationModel>> GetAllNegotiationsByUserIdAsync(Guid userId);
        Task<NegotiationModel> GetNegotiationByIdAsync(Guid id);
        Task<int?> GetNegotiationsAmount(Guid productId, Guid userId);
        Task<IEnumerable<NegotiationModel>> GetResolvedNegotiationsByUserIdAsync(Guid userId);
        Task<IEnumerable<NegotiationModel>> GetUnresolvedNegotiationsAsync();
        Task<IEnumerable<NegotiationModel>> GetUnresolvedNegotiationsByUserIdAsync(Guid userId);
        Task UpdateNegotiationAsync(NegotiationModel negotiationModel);
    }
}