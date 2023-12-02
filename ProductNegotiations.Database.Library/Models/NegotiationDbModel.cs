namespace ProductNegotiations.Database.Library.Models
{
    public class NegotiationDbModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public ProductDbModel Product { get; set; }
        public string? AdditiionalInformations { get; set; }
        public decimal ProposedPrice { get; set; }
        public bool? Decision { get; set; }
        public string? DecisionDescription { get; set; }
        public bool IsNegotiationResolved { get; set; } = false;
        public bool IsDeleted { get; set; } = false;
    }
}
