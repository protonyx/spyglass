using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.SDK.Models;

namespace Spyglass.Server.Services
{
    public class MetadataService
    {
        protected IMapper Mapper { get; }


        public MetadataService(IMapper mapper)
        {
            Mapper = mapper;
        }

        public Spyglass.SDK.Models.MetricProviderMetadata GetMetadata(Type providerType)
        {
            var modelExplorer = new EmptyModelMetadataProvider();
            var properties = modelExplorer.GetMetadataForProperties(providerType);

            return new SDK.Models.MetricProviderMetadata
            {
                Name = providerType.GetTypeInfo().Name,
                Properties = properties.Select(this.Mapper.Map<ModelPropertyMetadata>)
            };
        }
    }
}
