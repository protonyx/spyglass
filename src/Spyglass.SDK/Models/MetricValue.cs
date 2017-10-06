using System;
using Spyglass.SDK.Data;

namespace Spyglass.SDK.Models
{
  public class MetricValue : IMetricValue
  {
    public string Name { get; set; }

    public object Value { get; set; }

    public string Units { get; set; }
  }
}
