using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;

namespace Spyglass.SDK.Providers
{
    public class FileValueProvider : IMetricValueProvider
    {
        [Display(Name="Path")]
        [Description("Path to the file")]
        [Required]
        public string FilePath { get; set; }

        public string GetTypeName()
        {
          return "File";
        }

        public async Task<IEnumerable<IMetricValue>> GetValueAsync()
        {
            return GetValues();
        }

        public IEnumerable<IMetricValue> GetValues() 
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
