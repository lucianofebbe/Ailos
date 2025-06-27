using Application.Requests.ValidNewAccount;
using AutoMapper;
using Domain.Filters.CustomerBankAccountsService;

namespace Application.Profiles.ValidNewAccount
{
    internal class CreateCustomersBankAccountsProfile : Profile
    {
        public CreateCustomersBankAccountsProfile()
        {
            CreateMap<CreateCustomerBankAccountsFilter, AccountCreateRequest>()
                .ReverseMap();
        }
    }
}
