using AutoMapper;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Services.Filters.ContaCorrenteService;

namespace Services.Profiles.ContaCorrenteService
{
    internal class MapperCreateFilterProfile : Profile
    {
        public MapperCreateFilterProfile()
        {
            CreateMap<ContaCorrenteCreateParameter, CreateFilter>()
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => src.Ativo))
                .ForMember(dest => dest.Deleted, opt => opt.MapFrom(src => src.Deleted))
                .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.NomeDoCliente))
                .ForMember(dest => dest.Conta, opt => opt.MapFrom(src => src.NumeroDaConta))
                .ReverseMap();
        }
    }
}
