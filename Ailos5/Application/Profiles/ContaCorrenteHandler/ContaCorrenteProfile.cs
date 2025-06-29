using Application.Requests.ContaCorrente;
using Application.Responses.ContaCorrente;
using AutoMapper;
using Services.Domain;
using Services.Filters.ContaCorrenteService;

namespace Application.Profiles.ContaCorrenteHandler
{
    internal class ContaCorrenteProfile : Profile
    {
        public ContaCorrenteProfile()
        {
            CreateMap<CreateFilter, ContaCorrenteRequest>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => src.Deleted))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente))
                .ReverseMap();

            CreateMap<ContaCorrente, ContaCorrenteResponse>()
                .ForMember(dest => dest.Conta, opt => opt.MapFrom(src => src.Conta));
        }
    }
}
