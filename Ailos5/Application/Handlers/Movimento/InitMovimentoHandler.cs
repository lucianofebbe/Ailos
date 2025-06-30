using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.Movimento;
using Application.Requests.Movimento;
using Application.Responses.Movimento;
using AutoMapper;
using MediatR;
using Services.Filters.MovimentoService;
using Services.Interfaces.MovimentoService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entitie = Services.Domain;
namespace Application.Handlers.Movimento
{
    public class InitMovimentoHandler : IRequestHandler<InitMovimentoRequest, InitMovimentoResponse>
    {
        private IMovimentoService _IMovimentoService;
        private IMapperSpecificFactory<InitMovimentoRequest, InitMovimentoFilter> _IMapperFilter;
        private IMapperSpecificFactory<Entitie.Movimento, InitMovimentoResponse> _IMapperResult;
        private IList<Profile> _IProfiles;

        public InitMovimentoHandler(
            IMovimentoService iMovimentoService,
            IMapperSpecificFactory<InitMovimentoRequest, InitMovimentoFilter> iMapperFilter,
            IMapperSpecificFactory<Entitie.Movimento, InitMovimentoResponse> iMapperResult,
            IList<Profile> iProfiles) 
        {
            _IMovimentoService = iMovimentoService;
            _IMapperFilter = iMapperFilter;
            _IMapperResult = iMapperResult;
            _IProfiles = iProfiles;
        }

        public async Task<InitMovimentoResponse> Handle(InitMovimentoRequest request, CancellationToken cancellationToken)
        {
            _IProfiles.Add(new InitMovimentoProfile());
            var facFilter = await _IMapperFilter.Create(_IProfiles);
            var filter = await facFilter.MapperAsync(request);
            var movimento = await _IMovimentoService.InitMovimentoAsync(filter);
            if (movimento.Success)
            {
                var facResult = await _IMapperResult.Create(_IProfiles);
                var response = await facResult.MapperAsync(movimento.Item);
                return response;
            }
            return new InitMovimentoResponse() { DataMovimento = DateTime.UtcNow, Success = false, Message = movimento.Message };
        }
    }
}
