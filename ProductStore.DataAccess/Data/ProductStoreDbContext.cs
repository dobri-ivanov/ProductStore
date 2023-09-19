namespace ProductStore.DataAcess.Data
{
    using Microsoft.EntityFrameworkCore;
    
    using ProductStore.Models;

    public class ProductStoreDbContext : DbContext
    {
        public ProductStoreDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Category>().HasData(
                new { Id = 1, Name = "FirstCategory", DisplayOrder = 1 },
                new { Id = 2, Name = "SecondCategory", DisplayOrder = 2 },
                new { Id = 3, Name = "ThirdCategory", DisplayOrder = 3 }
            );
        }
    }
}
