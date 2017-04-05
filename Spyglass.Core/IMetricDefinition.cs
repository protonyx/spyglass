using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core
{
    public interface IMetricDefinition
    {
        string Name { get; }

        string Description { get; }
    }
}
