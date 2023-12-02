namespace ProductNegotiations.Database.Library.Models
{
    public class ProductDbModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
