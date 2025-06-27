using Api.Responses.Hackerrank;
using AutoMapper;
using Domain.EntitiesDomain.Hackerrank;

namespace Api.Profiles.Hackerrank
{
    public class HackerrankProfile : Profile
    {
        public HackerrankProfile()
        {
            CreateMap<HackerrankDomain, HackerrankResponse>()
                .ForMember(dest => dest.competition, opt => opt.MapFrom(src => src.Competition))
                .ForMember(dest => dest.year, opt => opt.MapFrom(src => src.Year))
                .ForMember(dest => dest.round, opt => opt.MapFrom(src => src.Round))
                .ForMember(dest => dest.team1, opt => opt.MapFrom(src => src.Team1))
                .ForMember(dest => dest.team2, opt => opt.MapFrom(src => src.Team2))
                .ForMember(dest => dest.team1goals, opt => opt.MapFrom(src => src.Team1Goals))
                .ForMember(dest => dest.team2goals, opt => opt.MapFrom(src => src.Team2Goals))
                .ReverseMap();
        }
    }
}
