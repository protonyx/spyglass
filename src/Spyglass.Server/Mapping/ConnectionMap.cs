using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spyglass.Server.Models;

namespace Spyglass.Server.Mapping
{
    public class ConnectionMap : IEntityTypeConfiguration<DatabaseConnection>
    {
        public void Configure(EntityTypeBuilder<DatabaseConnection> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}