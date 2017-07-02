using System;
using System.Collections.Generic;
using System.Text;

namespace Spyglass.Core
{
    public class ConfigurableMetricAttribute : Attribute
    {
        public string Name { get; set; }

        public ConfigurableMetricAttribute(string name)
        {
            Name = name;
        }
    }
}
