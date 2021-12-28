using AutoMapper;
using Spyglass.Server.DTO;
using Spyglass.Server.Models;

namespace Spyglass.Server.MappingProfiles
{
    public class DTOProfile : Profile
    {
        public DTOProfile()
        {
            this.CreateMap<MetricGroup, MetricGroupDTO>()
                .ReverseMap()
                .ForMember(t => t.Id, opt => opt.Ignore());
            
            this.CreateMap<Metric, MetricDTO>()
                .ReverseMap()
                .ForMember(t => t.Id, opt => opt.Ignore());

            this.CreateMap<DatabaseConnection, ConnectionDTO>()
                .ReverseMap()
                .ForMember(t => t.Id, opt => opt.Ignore());
        }
    }
}