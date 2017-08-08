using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spyglass.SDK.Providers;

namespace Spyglass.SDK.Services
{
    public class ProviderService
    {
        protected IDictionary<string, Type> ProviderMap { get; set; }

        public ProviderService()
        {
            var assm = GetType().GetTypeInfo().Assembly;

            ProviderMap = assm.ExportedTypes
                .Where(t => typeof(IMetricValueProvider).GetTypeInfo().IsAssignableFrom(t))
                .Where(t => t.GetTypeInfo().GetCustomAttribute<ConfigurableMetricAttribute>() != null)
                .ToDictionary(t => t.GetTypeInfo().GetCustomAttribute<ConfigurableMetricAttribute>().Name,
                t => t);
        }

        public Type GetProvider(string name)
        {
            return ProviderMap.ContainsKey(name) ? ProviderMap[name] : null;
        }
    }
}
