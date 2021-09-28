using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.Server.Models;

namespace Spyglass.Server.Services
{
    public class MetadataService
    {
        protected IMapper Mapper { get; }


        public MetadataService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public MetricProviderMetadata GetMetadata(Type providerType)
        {
            var modelExplorer = new EmptyModelMetadataProvider();
            var properties = modelExplorer.GetMetadataForProperties(providerType);

            return new MetricProviderMetadata
            {
                Name = providerType.GetTypeInfo().Name,
                Properties = properties.Select(this.Mapper.Map<ModelPropertyMetadata>)
            };
        }
    }
}
