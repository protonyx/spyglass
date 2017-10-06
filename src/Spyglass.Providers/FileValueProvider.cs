using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;

namespace Spyglass.SDK.Providers
{
    public class FileValueProvider : MetricValueProviderBase
    {
        [Display(Name="Path")]
        [Description("Path to the file")]
        [Required]
        public string FilePath { get; set; }

        public override string GetTypeName()
        {
          return "File";
        }

        public override IEnumerable<IMetricValue> GetValue()
        {
            var file = new FileInfo(FilePath);

            yield return new MetricValue
            {
              Name = "Exists",
              Value = file.Exists,
              Units = ""
            };

            if (!file.Exists)
              yield break;

            yield return new MetricValue
            {
              Name = "Since Last Modified",
              Value = DateTime.Now - file.LastWriteTime,
              Units = "Seconds"
            };

            yield return new MetricValue
            {
              Name = "Size",
              Value = file.Length,
              Units = "Bytes"
            };
        }
    }
}
