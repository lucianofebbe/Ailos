using Application.Requests.ValidNewAccount;
using AutoMapper;
using Domain.Filters.AccountsService;

namespace Application.Profiles.ValidNewAccount
{
    internal class CreateAccountProfile : Profile
    {
        public CreateAccountProfile()
        {
            CreateMap<CreateAccountFilter, AccountCreateRequest>()
            .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.CurrentBalance))
            .ReverseMap();
        }
    }
}
