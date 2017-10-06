﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Spyglass.SDK.Data;
using Spyglass.SDK.Providers;

namespace Spyglass.Providers
{
    public class DatabaseQueryValueProvider : MetricValueProviderBase
    {
        [Required]
        public string Driver { get; set; }

        [Required]
        public string ConnectionString { get; set; }

        [Required]
        public string Query { get; set; }

        public string Units { get; set; }

        public override string GetTypeName()
        {
          return "Database Query";
        }

        public override IEnumerable<IMetricValue> GetValue()
        {
            yield break;
        }
    }
}
