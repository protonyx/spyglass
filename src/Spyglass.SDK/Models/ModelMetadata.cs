using System.Collections.Generic;

namespace Spyglass.SDK.Models
{
    public class ModelMetadata
    {
        public string Name { get; set; }

        public IEnumerable<ModelPropertyMetadata> Properties { get; set; }
    }
}
