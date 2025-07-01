using Application.Requests.Movimento;
using Application.Responses.Movimento;
using AutoMapper;
using Services.Filters.MovimentoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitie = Services.Domain;

namespace Application.Profiles.Movimento
{
    internal class InitMovimentoProfile : Profile
    {
        public InitMovimentoProfile()
        {
            CreateMap<InitMovimentoRequest, InitMovimentoFilter>()
                .ForMember(dest => dest.NumeroContaCorrente, opt => opt.MapFrom(src => src.NumeroDaConta))
                .ForMember(dest => dest.TipoDeMovimento, opt => opt.MapFrom(src => src.TipoMovimento))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
                .ReverseMap();

            CreateMap<Entitie.Movimento, InitMovimentoResponse>()
                .ForMember(dest => dest.DataMovimento, opt => opt.MapFrom(src => src.DataMovimento))
                .ForMember(dest => dest.Guid, opt => opt.MapFrom(src => src.Guid))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor))
                .ReverseMap();
        }
    }
}
