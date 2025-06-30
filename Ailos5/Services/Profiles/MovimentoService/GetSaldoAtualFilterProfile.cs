using AutoMapper;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Services.Domain;
using Services.Filters.ContaCorrenteService;
using Services.Filters.MovimentoService;

namespace Services.Profiles.MovimentoService
{
    internal class GetSaldoAtualFilterProfile : Profile
    {
        public GetSaldoAtualFilterProfile()
        {
            CreateMap<GetSaldoAtualFilter, GetContaCorrenteFilter>()
                .ForMember(dest => dest.NumeroDaConta, opt => opt.MapFrom(src => src.NumeroDaConta))
                .ReverseMap();

            CreateMap<ContaCorrente, MovimentoGetByIdContaCorrenteParameter>()
                .ForMember(dest => dest.IdContaCorrente, opt => opt.MapFrom(src => src.Id));
        }
    }
}
