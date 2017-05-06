using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Spyglass.Api.Models
{
    public class MetricDescriptor
    {
        public string Name { get; set; }

        public IEnumerable<ModelPropertyMetadata> Properties { get; set; }
    }
}
