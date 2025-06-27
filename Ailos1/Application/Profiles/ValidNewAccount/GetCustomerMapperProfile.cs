using Application.Requests.ValidNewAccount;
using AutoMapper;
using Domain.Filters.CustomerService;

namespace Application.Profiles.ValidNewAccount
{
    internal class GetCustomerMapperProfile : Profile
    {
        public GetCustomerMapperProfile()
        {
            CreateMap<GetCustomerFilter, AccountCreateRequest>()
            .ForMember(dest => dest.GuidCustomer, opt => opt.MapFrom(src => src.Guid))
            .ReverseMap();
        }
    }
}
