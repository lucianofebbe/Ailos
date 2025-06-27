using AutoMapper;
using Domain.Filters.AccountsService;
using Infrastructure.Data.Parameters.Commands.Create;

namespace Domain.Profiles.AccountsService
{
    internal class CreateProfile : Profile
    {
        public CreateProfile()
        {
            CreateMap<CreateAccountFilter, CreateAccountParameter>()
                .ForMember(dest => dest.CurrentBalance, opt => opt.MapFrom(src => src.CurrentBalance))
                .ForMember(dest => dest.IdBankAccount, opt => opt.MapFrom(src => src.IdBankAccount))
                .ForMember(dest => dest.IdFather, opt => opt.MapFrom(src => src.IdFather))
                .ReverseMap();
        }
    }
}
