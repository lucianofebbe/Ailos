using Application.Requests.Transactions;
using AutoMapper;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.AccountsService;
using Domain.Filters.BankAccountsService;

namespace Application.Profiles.Transactions
{
    internal class NewDepositProfile : Profile
    {
        public NewDepositProfile() {

            CreateMap<GetBankAccountFilter, NewDepositRequest>()
            .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
            .ReverseMap();

            CreateMap<CreateAccountFilter, GetAccountFilter>()
                .ForMember(dest => dest.IdAccount, opt => opt.MapFrom(src => src.IdBankAccount))
                .ReverseMap();
        }
    }
}
