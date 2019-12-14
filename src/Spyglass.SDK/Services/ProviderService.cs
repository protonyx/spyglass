using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spyglass.SDK.Data;
using Spyglass.SDK.Models;

namespace Spyglass.SDK.Services
{
    public class ProviderService
    {
        protected static Dictionary<string, Type> ProviderMap { get; }

        static ProviderService()
        {
            var assm = Assembly.Load(new AssemblyName("Spyglass.Providers"));

            ProviderMap = assm.ExportedTypes
                .Where(t => typeof(IMetricValueProvider).IsAssignableFrom(t))
                .ToDictionary(t => t.Name, t => t);
        }

        public static Type GetProvider(string name)
        {
            return ProviderMap.ContainsKey(name) ? ProviderMap[name] : null;
        }

        public static IReadOnlyDictionary<string, Type> GetProviders()
        {
            return ProviderMap;
        }
        
        public static IMetricValueProvider BuildProvider(string typeName, IDictionary<string, string> config)
        {
            var pt = GetProvider(typeName);
            var obj = (IMetricValueProvider)Activator.CreateInstance(pt);
            var props = pt.GetProperties();
            
            foreach (var prop in props)
            {
                if (config.ContainsKey(prop.Name))
                {
                    prop.SetValue(obj, config[prop.Name]);
                }
            }

            return obj;
        }
    }
}
