using AutoMapper;
using Domain.Filters.BankAccountsService;
using Infrastructure.Data.Parameters.Commands.Create;

namespace Domain.Profiles.BankAccountsService
{
    internal class CreateProfile : Profile
    {
        public CreateProfile() {
            CreateMap<CreateBankAccountFilter, CreateBankAccountParameter>()
                .ForMember(dest => dest.JointAccount, opt => opt.MapFrom(src => src.JointAccount))
                    .ReverseMap();
        }
    }
}
