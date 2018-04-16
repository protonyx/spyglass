using AutoMapper;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Spyglass.SDK.Models;

namespace Spyglass.Server.MappingProfiles
{
    public class ModelMetadataProfile : Profile
    {
        public ModelMetadataProfile()
        {
            CreateMap<Microsoft.AspNetCore.Mvc.ModelBinding.ModelMetadata, ModelPropertyMetadata>()
                .ForMember(t => t.ModelType, opt => opt.MapFrom(t => t.UnderlyingOrModelType.Name));
        }
    }
}
