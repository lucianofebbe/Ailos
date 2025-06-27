using Application.Requests.ValidNewAccount;
using AutoMapper;
using Domain.Filters.CustomerService;

namespace Application.Profiles.ValidNewAccount
{
    public class ValidNewAccountProfile : Profile
    {
        public ValidNewAccountProfile() {
            CreateMap<GetCustomerFilter, ValidNewAccountRequest>()
                    .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => src.CPF))
                    .ReverseMap();
        }
    }
}
