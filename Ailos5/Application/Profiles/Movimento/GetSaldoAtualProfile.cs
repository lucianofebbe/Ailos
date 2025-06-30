using Application.Requests.Movimento;
using Application.Responses.Movimento;
using AutoMapper;
using Services.Filters.MovimentoService;
using Entitie = Services.Domain;

namespace Application.Profiles.Movimento
{
    internal class GetSaldoAtualProfile : Profile
    {
        public GetSaldoAtualProfile()
        {
            CreateMap<GetSaldoAtualRequest, GetSaldoAtualFilter>()
                .ForMember(dest => dest.NumeroDaConta, opt => opt.MapFrom(src => src.NumeroDaConta))
                .ReverseMap();

            CreateMap<Entitie.Movimento, GetSaldoAtualResponse>()
                .ForMember(dest => dest.DataMovimento, opt => opt.MapFrom(src => src.DataMovimento))
                .ForMember(dest => dest.Valor, opt => opt.MapFrom(src => src.Valor));
        }
    }
}
