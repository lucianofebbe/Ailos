using AutoMapper;
using Domain.Filters.AccountsService;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Parameters.Readers.Get;

namespace Domain.Profiles.AccountsService
{
    internal class GetProfile : Profile
    {
        public GetProfile()
        {
            CreateMap<GetAccountFilter, GetAccountParameter>()
                .ForMember(dest => dest.IdBankAccount, opt => opt.MapFrom(src => src.IdAccount))
                .ReverseMap();

            CreateMap<CreateAccountFilter, GetAccountFilter>()
                .ForMember(dest => dest.IdAccount, opt => opt.MapFrom(src => src.IdBankAccount))
                .ReverseMap();
        }
    }
}
