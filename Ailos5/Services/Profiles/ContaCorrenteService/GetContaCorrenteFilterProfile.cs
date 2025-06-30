using AutoMapper;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Readers;
using Services.Filters.ContaCorrenteService;

namespace Services.Profiles.ContaCorrenteService
{
    internal class GetContaCorrenteFilterProfile : Profile
    {
        public GetContaCorrenteFilterProfile()
        {
            CreateMap<ContaCorrenteByNumeroDaContaParameter, GetContaCorrenteFilter>()
                .ForMember(dest => dest.NumeroDaConta, opt => opt.MapFrom(src => src.NumeroDaConta))
                .ForMember(dest => dest.IncluirAtivas, opt => opt.MapFrom(src => src.IncluirAtivas))
                .ForMember(dest => dest.IncluirDeletadas, opt => opt.MapFrom(src => src.IncluirDeletadas))
                .ReverseMap();
        }
    }
}
