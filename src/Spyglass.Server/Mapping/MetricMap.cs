using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spyglass.Server.Models;

namespace Spyglass.Server.Mapping
{
    public class MetricMap : IEntityTypeConfiguration<Monitor>
    {
        public void Configure(EntityTypeBuilder<Monitor> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne<DatabaseConnection>(t => t.Connection)
                .WithMany(t => t.Monitors)
                .HasForeignKey(t => t.ConnectionId);
        }
    }
}