using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spyglass.Server.Models;

namespace Spyglass.Server.Mapping
{
    public class MetricMap : IEntityTypeConfiguration<Metric>
    {
        public void Configure(EntityTypeBuilder<Metric> builder)
        {
            builder.HasKey(t => t.Id);

            builder.HasOne<MetricGroup>(t => t.Group)
                .WithMany(t => t.Metrics)
                .HasForeignKey(t => t.MetricGroupId);
        }
    }
}