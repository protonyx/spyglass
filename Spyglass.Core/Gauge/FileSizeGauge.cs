using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Text;
using Spyglass.Core.Metrics;

namespace Spyglass.Core.Gauge
{
    public class FileSizeGauge : FunctionGauge
    {
        public string FilePath { get; set; }

        public string Units => "MB";

        public FileSizeGauge()
        {
            this._valueProvider = GetFileSize;
        }

        private double GetFileSize()
        {
            if (string.IsNullOrWhiteSpace(FilePath) || !File.Exists(FilePath))
                throw new InvalidOperationException("Invalid File Path");

            var file = new FileInfo(FilePath);

            return file.Length / (1024.0 * 1024.0);
        }
    }
}
