using System.Collections.Generic;

namespace Spyglass.Server.Models
{
    public class ProviderDescriptor
    {
        public string Name { get; set; }

        public IEnumerable<ModelPropertyMetadata> Properties { get; set; }
    }
}
