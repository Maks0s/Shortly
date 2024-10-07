using Microsoft.EntityFrameworkCore;
using Shortly.Domain.Entities;
using Shortly.Infrastructure.Persistence.Common;

namespace Shortly.Infrastructure.Persistence.DbContexts
{
    public class ShortUrlDbContext : DbContext
    {
        public DbSet<ShortUrl> ShortUrls { get; set; }

        public ShortUrlDbContext(DbContextOptions<ShortUrlDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShortUrlDbContext).Assembly);

            modelBuilder.HasDefaultSchema(DbSchemaConstants.ShortUrlDbSchema);
        }
    }
}