using AutoMapper;
using NZWalks.Api.Models.Domain;
using NZWalks.Api.Models.DTO;
using NZWalks.Models.DTO;

namespace NZWalks.Configuration
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<Region, AddRegionRequestDto>().ReverseMap();
            CreateMap<Region, UpdateRegionRequestDto>().ReverseMap();
        }
    }
}
