using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Spyglass.Server.Models;

namespace Spyglass.Server.Mapping
{
    public class MetricGroupMap : IEntityTypeConfiguration<MetricGroup>
    {
        public void Configure(EntityTypeBuilder<MetricGroup> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}