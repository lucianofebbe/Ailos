using AutoMapper;
using Domain.Filters.CustomerBankAccountsService;
using Infrastructure.Data.Parameters.Readers.GetAll;

namespace Domain.Profiles.CustomerBankAccountsService
{
    internal class GetJoinProfile : Profile
    {
        public GetJoinProfile()
        {
            CreateMap<GetAllCustomerBankAccountsFilter, GetAllCustomerParameter>()
            .ForMember(dest => dest.IdCustomer, opt => opt.MapFrom(src => src.IdCustomer))
            .ReverseMap();
        }
    }
}
