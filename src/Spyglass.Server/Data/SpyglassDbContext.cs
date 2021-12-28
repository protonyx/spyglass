using Microsoft.EntityFrameworkCore;

namespace Spyglass.Server.Data
{
    public class SpyglassDbContext : DbContext
    {
        public SpyglassDbContext(DbContextOptions<SpyglassDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }
    }
}