using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.ContaCorrenteHandler;
using Application.Requests.ContaCorrente;
using Application.Responses.ContaCorrente;
using AutoMapper;
using MediatR;
using Services.Filters.ContaCorrenteService;
using Services.Interfaces.ContaCorrenteService;
using Entitie = Services.Domain;

namespace Application.Handlers.ContaCorrente
{
    public class ContaCorrenteCreateHandler : IRequestHandler<ContaCorrenteCreateRequest, ContaCorrenteCreateResponse>
    {
        private IContaCorrenteService _IContaCorrenteService;
        private IMapperSpecificFactory<CreateFilter, ContaCorrenteCreateRequest> _MapperRequest;
        private IMapperSpecificFactory<ContaCorrenteCreateResponse, Entitie.ContaCorrente> _MapperResponse;
        private IList<Profile> _Profiles;
        public ContaCorrenteCreateHandler(
            IContaCorrenteService iContaCorrenteService,
            IMapperSpecificFactory<CreateFilter, ContaCorrenteCreateRequest> mapperRequest,
            IMapperSpecificFactory<ContaCorrenteCreateResponse, Entitie.ContaCorrente> mapperResponse,
            IList<Profile> profiles)
        {
            _IContaCorrenteService = iContaCorrenteService;
            _MapperRequest = mapperRequest;
            _MapperResponse = mapperResponse;
            _Profiles = profiles;
            _Profiles.Add(new ContaCorrenteCreateProfile());
        }

        public async Task<ContaCorrenteCreateResponse> Handle(ContaCorrenteCreateRequest request, CancellationToken cancellationToken)
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
