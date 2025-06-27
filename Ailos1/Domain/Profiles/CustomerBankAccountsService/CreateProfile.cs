using AutoMapper;
using Domain.Filters.CustomerBankAccountsService;
using Infrastructure.Data.Parameters.Commands.Create;

namespace Domain.Profiles.CustomerBankAccountsService
{
    internal class CreateProfile : Profile
    {
        public CreateProfile()
        {
            CreateMap<CreateCustomerBankAccountsFilter, CreateCustomerBankAccountsParameter>()
                        .ForMember(dest => dest.IdBankAccount, opt => opt.MapFrom(src => src.IdBankAccount))
                        .ForMember(dest => dest.IdCustomer, opt => opt.MapFrom(src => src.IdCustomer))
                        .ForMember(dest => dest.AccountHolder, opt => opt.MapFrom(src => src.AccountHolder))
                        .ReverseMap();
        }
    }
}
