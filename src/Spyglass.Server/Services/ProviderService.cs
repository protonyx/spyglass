using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Spyglass.SDK.Data;

namespace Spyglass.Server.Services
{
    public class ProviderService
    {
        protected IDictionary<string, Type> ProviderMap { get; set; }

        public ProviderService()
        {
            var assm = GetType().GetTypeInfo().Assembly;

            ProviderMap = new Dictionary<string, Type>();
        }

        public Type GetProvider(string name)
        {
            return ProviderMap.ContainsKey(name) ? ProviderMap[name] : null;
        }
    }
}
