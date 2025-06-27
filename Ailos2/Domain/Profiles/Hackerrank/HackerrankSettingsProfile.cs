using AutoMapper;
using Domain.Services.Hackerrank.Settings;
using Infrastructure.Apis.Hackerrank.Settings;

namespace Domain.Profiles.Hackerrank
{
    internal class HackerrankSettingsProfile : Profile
    {
        public HackerrankSettingsProfile()
        {
            CreateMap<GetHackerrankSettings, HackerrankSettings>()
                .ForMember(dest => dest.Team, opt => opt.MapFrom(src => src.Team1))
                .ForMember(dest => dest.Year, opt => opt.MapFrom(src => src.Year))
                .ReverseMap();
        }
    }
}
