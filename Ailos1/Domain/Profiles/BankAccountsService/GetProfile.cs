using AutoMapper;
using Domain.Filters.BankAccountsService;
using Infrastructure.Data.Parameters.Readers.Get;

namespace Domain.Profiles.BankAccountsService
{
    internal class GetProfile : Profile
    {
        public GetProfile() {
            CreateMap<GetBankAccountFilter, GetBankAccountParameter>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                    .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
                    .ReverseMap();
        }
    }
}
