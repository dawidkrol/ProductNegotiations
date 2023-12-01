using Microsoft.EntityFrameworkCore;
using ProductNegotiations.Database.Library.Models;

namespace ProductNegotiations.Database.Library
{
    public class NegotiationDbContext : DbContext
    {
        public NegotiationDbContext(DbContextOptions<NegotiationDbContext> options) : base(options) { }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "NegotiationsDb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDbModel>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<NegotiationDbModel>()
                .HasKey(p => p.Id);
        }
        public DbSet<NegotiationDbModel> Negotiations { get; set; }
        public DbSet<ProductDbModel> Products { get; set; }
    }
}
