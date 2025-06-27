using Application.Requests.NewDeposit;
using Application.Requests.Transactions;
using AutoMapper;
using Domain.Filters.BankAccountsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
