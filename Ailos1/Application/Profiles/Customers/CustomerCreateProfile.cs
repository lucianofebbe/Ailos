using Application.Requests.Customers;
using Application.Responses.Customers;
using AutoMapper;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.CustomerService;

namespace Application.Profiles.Customers
{
    internal class CustomerCreateProfile : Profile
    {
        public CustomerCreateProfile() {
            CreateMap<CreateCustomerFilter, CustomerCreateRequest>()
                        .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                        .ForMember(dest => dest.NameCustomer, opt => opt.MapFrom(src => src.NameCustomer))
                        .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                        .ReverseMap();

            CreateMap<CustomerDomain, CustomerCreateResponse>()
            .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
            .ForMember(dest => dest.NameCustomer, opt => opt.MapFrom(src => src.NameCustomer))
            .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
            .ReverseMap();
        }
    }
}
