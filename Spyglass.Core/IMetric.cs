using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core
{
    public interface IMetric
    {
        string Name { get; }

        object Value { get; }

        string Units { get; }
    }
}
