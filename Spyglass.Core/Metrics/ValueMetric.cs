using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Metrics
{
    public class ValueMetric : IMetric
    {
        public string Name { get; set; }

        public object Value { get; set; }

        public string Units { get; set; }
    }
}
