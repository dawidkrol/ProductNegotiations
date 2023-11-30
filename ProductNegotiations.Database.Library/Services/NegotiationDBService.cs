using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public class NegotiationDBService : INegotiationDBService
    {
        public NegotiationDBService() { }
        public async Task<NegotiationDbModel> GetNegotiationByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsAsync()
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetNegotiationsAmount(Guid productId, Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
        public async Task<IEnumerable<NegotiationDbModel>> GetResolvedNegotiationsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsByUserIdAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateNegotiationAsync(NegotiationDbModel negotiationDbModel)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateNegotiationAsync(NegotiationDbModel negotiationDbModel)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteNegotiationAsync(NegotiationDbModel negotiationDbModel)
        {
            throw new NotImplementedException();
        }
    }
}
