using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core.Sources
{
    public class PowershellScriptSource : IMetricSource
    {
        public string ScriptPath { get; set; }

        public IEnumerable<IMetric> GetMetrics()
        {
            throw new NotImplementedException();

            // Return code
            // Output
        }
    }
}
