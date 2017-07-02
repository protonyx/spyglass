using System.Collections.Generic;

namespace Spyglass.Server.Models
{
    public class MetricDescriptor
    {
        public string Name { get; set; }

        public IEnumerable<ModelPropertyMetadata> Properties { get; set; }
    }
}
