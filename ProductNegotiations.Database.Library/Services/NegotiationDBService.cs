using Microsoft.EntityFrameworkCore;
using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library.Services
{
    public class NegotiationDBService : INegotiationDBService
    {
        private readonly NegotiationDbContext _dbContext;

        public NegotiationDBService(NegotiationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Returns only not deleted negotitations.
        /// </summary>
        private IQueryable<NegotiationDbModel> getAllNotDeleted()
        {
            return _dbContext.Negotiations.Where(x => x.IsDeleted == false).Include("Product");
        }
        /// <summary>
        /// Returning negotiation model by id (including deleted).
        /// </summary>
        /// <param name="id">Negotiation id</param>
        public async Task<NegotiationDbModel> GetNegotiationByIdAsync(Guid id)
        {
            var output = _dbContext.Negotiations.Where(x => x.Id == id).Include("Product").Single();
            return output;
        }
        /// <summary>
        /// Returning not deleted and unresolved negotiations models.
        /// </summary>
        public async Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsAsync()
        {
            var output = getAllNotDeleted().Where(x => x.IsNegotiationResolved == false).AsEnumerable();
            return output;
        }
        /// <summary>
        /// Returning all negotiation models (including deleted).
        /// </summary>
        public async Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsAsync()
        {
            var output = _dbContext.Negotiations.Include("Product").AsEnumerable();
            return output;
        }
        /// <summary>
        /// Returns the number of attempts to negotiate a specific product by a specific customer.
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="userId">Users id</param>
        public async Task<int> GetNegotiationsAmount(Guid productId, string userId)
        {
            var output = getAllNotDeleted().Where(x => x.Product.Id == productId && x.UserId == userId).Count();
            return output;
        }
        /// <summary>
        /// Returns all negotiations attemps by specific user.
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IEnumerable<NegotiationDbModel>> GetAllNegotiationsByUserIdAsync(string userId)
        {
            var output = getAllNotDeleted().Where(x => x.UserId == userId).AsEnumerable();
            return output;
        }
        /// <summary>
        /// Returns all resolved negotiations attemps by specific user.
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IEnumerable<NegotiationDbModel>> GetResolvedNegotiationsByUserIdAsync(string userId)
        {
            var output = getAllNotDeleted().Where(x => x.UserId == userId && x.IsNegotiationResolved == true).AsEnumerable();
            return output;
        }
        /// <summary>
        /// Returns all unresolved negotiations attemps by specific user.
        /// </summary>
        /// <param name="userId">User id</param>
        public async Task<IEnumerable<NegotiationDbModel>> GetUnresolvedNegotiationsByUserIdAsync(string userId)
        {
            var output = getAllNotDeleted().Where(x => x.UserId == userId && x.IsNegotiationResolved == false).AsEnumerable();
            return output;
        }
        /// <summary>
        /// Creating new negotiation.
        /// </summary>
        /// <param name="negotiationDbModel">Model to create</param>
        public async Task CreateNegotiationAsync(NegotiationDbModel negotiationDbModel)
        {
            var product = _dbContext.Products.Single(x => x.Id == negotiationDbModel.Product.Id);
            negotiationDbModel.Product = product;
            await _dbContext.Negotiations.AddAsync(negotiationDbModel);
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Updating model, model is selected by id, other properties are changing to given.
        /// </summary>
        public async Task UpdateNegotiationAsync(NegotiationDbModel negotiationDbModel)
        {
            var data = await _dbContext.Negotiations.SingleAsync(x => x.Id == negotiationDbModel.Id);
            
            data.DecisionDescription = negotiationDbModel.DecisionDescription;
            data.Decision = negotiationDbModel.Decision;
            data.Product = negotiationDbModel.Product;
            data.AdditiionalInformations = negotiationDbModel.AdditiionalInformations;
            data.ProposedPrice = negotiationDbModel.ProposedPrice;
            data.IsNegotiationResolved = negotiationDbModel.IsNegotiationResolved;
            data.IsDeleted = negotiationDbModel.IsDeleted;

            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Changing model flag to the deleted
        /// </summary>
        public async Task DeleteNegotiationAsync(NegotiationDbModel negotiationDbModel)
        {
            var data = await _dbContext.Negotiations.SingleAsync(x => x.Id == negotiationDbModel.Id);
            data.IsDeleted = true; 
            await _dbContext.SaveChangesAsync();
        }
        /// <summary>
        /// Returns number of resolved negotiations by user and product
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="userId">User id</param>
        public async Task<int> GetResolvedNegotiationsByUserIdAndProductAsync(Guid productId, string userId)
        {
            var output = getAllNotDeleted().Where(x => x.Product.Id == productId && x.UserId == userId && x.IsNegotiationResolved == true).Count();
            return output;
        }
        /// <summary>
        /// Returns true if there exists any unresolved negotiation by product and user
        /// </summary>
        /// <param name="productId">Product id</param>
        /// <param name="userId">User id</param>
        public async Task<bool> IsUnresolvedNegotiationByProductAndUser(Guid productId, string userId)
        {
            var output = getAllNotDeleted().Where(x => x.Product.Id == productId && x.UserId == userId && x.IsNegotiationResolved == false).Any();
            return output;
        }
        /// <summary>
        /// Returns true if there exists any already accepted negotiation by product and user
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> IsAlreadyAcceptedNegotiationByProductAndUser(Guid productId, string userId)
        {
            var output = getAllNotDeleted().Where(x => x.Product.Id == productId && x.UserId == userId && x.IsNegotiationResolved == true && x.Decision == true).Any();
            return output;
        }
    }
}
