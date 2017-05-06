using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Metrics
{
    public interface IMetric
    {
        Guid Id { get; set; }

        string Name { get; }
    }
}
