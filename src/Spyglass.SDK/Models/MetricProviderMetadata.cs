using System.Collections.Generic;

namespace Spyglass.SDK.Models
{
    public class MetricProviderMetadata
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<ModelPropertyMetadata> Properties { get; set; }
    }
}
