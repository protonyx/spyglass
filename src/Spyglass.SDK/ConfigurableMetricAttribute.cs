using System;

namespace Spyglass.SDK
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
