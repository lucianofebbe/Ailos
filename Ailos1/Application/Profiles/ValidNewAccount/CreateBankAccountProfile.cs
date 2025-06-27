using Application.Requests.ValidNewAccount;
using AutoMapper;
using Domain.Filters.BankAccountsService;

namespace Application.Profiles.ValidNewAccount
{
    internal class CreateBankAccountProfile : Profile
    {
        public CreateBankAccountProfile()
        {
            CreateMap<CreateBankAccountFilter, AccountCreateRequest>()
                .ForMember(dest => dest.JointAccount, opt => opt.MapFrom(src => src.JointAccount))
                .ReverseMap();
        }
    }
}
