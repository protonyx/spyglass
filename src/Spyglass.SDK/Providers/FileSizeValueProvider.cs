using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Spyglass.SDK.Providers
{
    [ConfigurableMetric("File size")]
    public class FileSizeValueProvider : FunctionValueProvider
    {
        [Display(Order = 1)]
        [Required]
        public string FilePath { get; set; }

        public FileSizeValueProvider()
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
