using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.API.Models
{
    public class NegotiationClientCreateModel
    {
        public ProductClientModel Product { get; set; }
        public string? AdditiionalInformations { get; set; }
        public decimal ProposedPrice { get; set; }
    }
}
