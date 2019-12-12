using AutoMapper;
using Spyglass.SDK.Models;
using Spyglass.Server.DTO;

namespace Spyglass.Server.MappingProfiles
{
    public class DTOProfile : Profile
    {
        public DTOProfile()
        {
            this.CreateMap<Metric, MetricDTO>()
                .ReverseMap()
                .ForMember(t => t.Id, opt => opt.Ignore());
        }
    }
}