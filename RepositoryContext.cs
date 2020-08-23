using Microsoft.EntityFrameworkCore;

namespace APIExample
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<ProductViewModel> Products { get; set; }

    }
}
