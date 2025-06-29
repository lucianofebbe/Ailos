using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.ContaCorrenteHandler;
using Application.Requests.ContaCorrente;
using Application.Responses.ContaCorrente;
using AutoMapper;
using MediatR;
using Services.Filters.ContaCorrenteService;
using Services.Interfaces;
using Entitie = Services.Domain;

namespace Application.Handlers
{
    public class ContaCorrenteHandler : IRequestHandler<ContaCorrenteRequest, ContaCorrenteResponse>
    {
        private IContaCorrenteService _IContaCorrenteService;
        private IMapperSpecificFactory<CreateFilter, ContaCorrenteRequest> _MapperRequest;
        private IMapperSpecificFactory<ContaCorrenteResponse, Entitie.ContaCorrente> _MapperResponse;
        private IList<Profile> _Profiles;
        public ContaCorrenteHandler(
            IContaCorrenteService iContaCorrenteService,
            IMapperSpecificFactory<CreateFilter, ContaCorrenteRequest> mapperRequest,
            IMapperSpecificFactory<ContaCorrenteResponse, Entitie.ContaCorrente> mapperResponse,
            IList<Profile> profiles)
        {
            _IContaCorrenteService = iContaCorrenteService;
            _MapperRequest = mapperRequest;
            _MapperResponse = mapperResponse;
            _Profiles = profiles;
            _Profiles.Add(new ContaCorrenteProfile());
        }

        public async Task<ContaCorrenteResponse> Handle(ContaCorrenteRequest request, CancellationToken cancellationToken)
        {
            var facRequest = await _MapperRequest.Create(_Profiles);
            var facFilter = await facRequest.MapperAsync(request);

            var result = await _IContaCorrenteService.CreateAsync(facFilter);
            if (result.Success)
            {
                var facResponse = await _MapperResponse.Create(_Profiles);
                var facResult = await facResponse.MapperAsync(result.Item);
                return facResult;
            }
            return null;
        }
    }
}
