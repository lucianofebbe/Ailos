using AutoMapper;
using Domain.Filters.CustomerService;
using Infrastructure.Data.Parameters.Readers.Get;

namespace Domain.Profiles.CustomerService
{
    internal class GetProfile : Profile
    {
        public GetProfile()
        {
            CreateMap<GetCustomerFilter, GetCustomerParameter>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameCustomer))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ReverseMap();
        }
    }
}
