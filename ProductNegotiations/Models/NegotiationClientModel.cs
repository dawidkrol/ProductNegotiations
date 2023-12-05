using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.API.Models
{
    public class NegotiationClientModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ProductDbModel Product { get; set; }
        public string? AdditiionalInformations { get; set; }
        public decimal ProposedPrice { get; set; }
    }
}
