using Application.Requests.Transactions;
using AutoMapper;
using Domain.Filters.BankAccountsService;

namespace Application.Profiles.Transactions
{
    internal class WithdrawProfile : Profile
    {
        public WithdrawProfile()
        {
            CreateMap<GetBankAccountFilter, WithdrawRequest>()
                .ForMember(dest => dest.AccountNumber, opt => opt.MapFrom(src => src.AccountNumber))
                .ReverseMap();
        }
    }
}
