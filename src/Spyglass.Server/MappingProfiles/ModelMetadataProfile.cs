using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.Server.Models;

namespace Spyglass.Server.MappingProfiles
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
