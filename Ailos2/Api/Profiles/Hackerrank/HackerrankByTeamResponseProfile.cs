using Api.Responses.Hackerrank;
using AutoMapper;
using Domain.EntitiesDomain.Hackerrank;

namespace Api.Profiles.Hackerrank
{
    public class HackerrankByTeamResponseProfile : Profile
    {
        public HackerrankByTeamResponseProfile() {
            CreateMap<HackerrankDomainByTeam, HackerrankByTeamResponse>()
        .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team))
        .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
        .ForMember(dest => dest.Result, opt => opt.MapFrom(src => src.Result))
        .ReverseMap();
        }
    }
}
