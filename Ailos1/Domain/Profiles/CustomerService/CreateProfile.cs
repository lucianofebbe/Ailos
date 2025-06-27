using AutoMapper;
using Domain.Filters.CustomerService;
using Infrastructure.Data.Parameters.Commands.Create;

namespace Domain.Profiles.CustomerService
{
    internal class CreateProfile : Profile
    {
        public CreateProfile()
        {
            CreateMap<CreateCustomerFilter, CreateCustomerParameter>()
                .ForMember(dest => dest.NameCustomer, opt => opt.MapFrom(src => src.NameCustomer))
                .ForMember(dest => dest.CPF, opt => opt.MapFrom(src => src.CPF))
                .ReverseMap();
        }
    }
}
