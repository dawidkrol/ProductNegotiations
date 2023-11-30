using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public interface INegotiationDBService
    {
        Task CreateNegotiationAsync(NegotiationDbModel negotiationDbModel);
        Task DeleteNegotiationAsync(NegotiationDbModel negotiationDbModel);
        Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsAsync();
        Task<NegotiationDbModel> GetNegotiationByIdAsync(Guid id);
        Task<IEnumerable<NegotiationDbModel>> GetNegotiationsByUserIdAsync(Guid userId);
        Task UpdateNegotiationAsync(NegotiationDbModel negotiationDbModel);
    }
}