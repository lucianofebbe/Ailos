using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.Movimento;
using Application.Requests.ContaCorrente;
using Application.Requests.Movimento;
using Application.Responses.ContaCorrente;
using Application.Responses.Movimento;
using AutoMapper;
using MediatR;
using Services.Domain;
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
    public class GetSaldoAtualHandler : IRequestHandler<GetSaldoAtualRequest, GetSaldoAtualResponse>
    {
        private IMovimentoService _IMovimentoService;
        private IMapperSpecificFactory<GetSaldoAtualRequest, GetSaldoAtualFilter> _IMapperFilter;
        private IMapperSpecificFactory<Entitie.Movimento, GetSaldoAtualResponse> _IMapperResult;
        private IList<Profile> _IProfiles;
        public GetSaldoAtualHandler(
            IMovimentoService iMovimentoService,
            IMapperSpecificFactory<GetSaldoAtualRequest, GetSaldoAtualFilter> iMapperFilter,
            IMapperSpecificFactory<Entitie.Movimento, GetSaldoAtualResponse> iMapperResult,
            IList<Profile> iProfiles)
        {
            _IMovimentoService = iMovimentoService;
            _IMapperFilter = iMapperFilter;
            _IMapperResult = iMapperResult;
            _IProfiles = iProfiles;
        }

        public async Task<GetSaldoAtualResponse> Handle(GetSaldoAtualRequest request, CancellationToken cancellationToken)
        {
            _IProfiles.Add(new GetSaldoAtualProfile());
            var facMapperFilter = await _IMapperFilter.Create(_IProfiles);
            var filter = await facMapperFilter.MapperAsync(request);
            var result = await _IMovimentoService.GetSaldoAtualAsync(filter);
            if(result.Success)
            {
                var facMapperResult = await _IMapperResult.Create(_IProfiles);
                var response = await facMapperResult.MapperAsync(result.Item);
                return response;
            }
            return new GetSaldoAtualResponse() { DataMovimento = DateTime.UtcNow, Message = result.Message, Success = false };
        }
    }
}
