using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Spyglass.Api.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Spyglass.Api.MappingProfiles
{
    public class ModelMetadataProfile : Profile
    {
        public ModelMetadataProfile()
        {
            CreateMap<ModelMetadata, ModelPropertyMetadata>()
                .ForMember(t => t.ModelType, opt => opt.MapFrom(t => t.UnderlyingOrModelType.Name));
        }
    }
}
