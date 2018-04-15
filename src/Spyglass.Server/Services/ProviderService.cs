using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.SDK.Data;
using Spyglass.Server.Models;

namespace Spyglass.Server.Services
{
    public class ProviderService
    {
        protected IMapper Mapper { get; }

        protected Dictionary<string, Type> ProviderMap { get; }

        public ProviderService(IMapper mapper)
        {
            Mapper = mapper;

            var assm = Assembly.Load(new AssemblyName("Spyglass.Providers"));

            ProviderMap = assm.ExportedTypes
                .Where(t => typeof(IMetricValueProvider).IsAssignableFrom(t))
                .ToDictionary(t => t.Name, t => t);
        }

        public ProviderDescriptor GetMetadata(Type providerType)
        {
            var modelExplorer = new EmptyModelMetadataProvider();
            var properties = modelExplorer.GetMetadataForProperties(providerType);

            return new ProviderDescriptor
            {
                Name = providerType.GetTypeInfo().Name,
                Properties = properties.Select(this.Mapper.Map<ModelPropertyMetadata>)
            };
        }

        public Type GetProvider(string name)
        {
            return ProviderMap.ContainsKey(name) ? ProviderMap[name] : null;
        }

        public IReadOnlyDictionary<string, Type> GetProviders()
        {
            return this.ProviderMap;
        }
    }
}
