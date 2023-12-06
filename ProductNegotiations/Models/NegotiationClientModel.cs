using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.API.Models
{
    public class NegotiationClientModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ProductClientModel Product { get; set; }
        public string? AdditiionalInformations { get; set; }
        public decimal ProposedPrice { get; set; }
        public bool? Decision { get; set; }
        public string? DecisionDescription { get; set; }
        public bool IsNegotiationResolved { get; set; } = false;
    }
}
