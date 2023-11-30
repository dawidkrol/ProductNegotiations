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
        public async Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<NegotiationDbModel>> GetNegotiationsByUserIdAsync(Guid userId)
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
