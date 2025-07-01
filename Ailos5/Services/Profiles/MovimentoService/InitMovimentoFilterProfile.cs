using AutoMapper;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Services.Filters.ContaCorrenteService;
using Services.Filters.MovimentoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitieDomain = Domain.Entities.Sql;
using EntitieServices = Services.Domain;

namespace Services.Profiles.MovimentoService
{
    internal class InitMovimentoFilterProfile : Profile
    {
        public InitMovimentoFilterProfile()
        {
            CreateMap<GetSaldoAtualFilter, GetContaCorrenteFilter>()
                .ForMember(dest => dest.NumeroDaConta, opt => opt.MapFrom(src => src.NumeroDaConta))
                .ReverseMap();

            CreateMap<UltimoMovimentoGetByIdContaCorrenteParameter, EntitieDomain.ContaCorrente>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.IdContaCorrente))
                .ReverseMap();

            CreateMap<InitMovimentoFilter, GetContaCorrenteFilter>()
                .ForMember(dest => dest.NumeroDaConta, opt => opt.MapFrom(src => src.NumeroContaCorrente))
                .ReverseMap();

            CreateMap<EntitieServices.ContaCorrente, UltimoMovimentoGetByIdContaCorrenteParameter>()
                .ForMember(dest => dest.IdContaCorrente, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();


            CreateMap<EntitieServices.Movimento, EntitieDomain.Movimento>()
                .ForMember(dest => dest.IdContaCorrente, opt => opt.MapFrom(src => src.Id))
                .ReverseMap();

            CreateMap<GetSaldoAtualFilter, InitMovimentoFilter>()
                .ForMember(dest => dest.NumeroContaCorrente, opt => opt.MapFrom(src => src.NumeroDaConta))
                .ReverseMap();
        }
    }
}
