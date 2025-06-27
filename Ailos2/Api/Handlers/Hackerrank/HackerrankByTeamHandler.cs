using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Api.Profiles.Hackerrank;
using Api.Requests.Hackerrank;
using Api.Responses.Hackerrank;
using AutoMapper;
using Domain.EntitiesDomain.Hackerrank;
using Domain.Services.Hackerrank;
using Domain.Services.Hackerrank.Settings;
using MediatR;

namespace Api.Handlers.Hackerrank
{
    public class HackerrankByTeamHandler : IRequestHandler<HackerrankByTeamRequest, List<HackerrankByTeamResponse>>
    {
        private IHackerrankService _IHackerrankService;
        private IMapperSpecific<HackerrankSettings, HackerrankByTeamRequest> _MapperFilter;
        private IMapperSpecificFactory<HackerrankDomainByTeam, HackerrankByTeamResponse> _MapperResponse;
        private IList<Profile> _Profiles;

        public HackerrankByTeamHandler(
            IHackerrankService iHackerrankService,
            IMapperSpecific<HackerrankSettings, HackerrankByTeamRequest> mapperFilter,
            IMapperSpecificFactory<HackerrankDomainByTeam, HackerrankByTeamResponse> mapperResponse,
            IList<Profile> profiles)
        {
            _IHackerrankService = iHackerrankService;
            _MapperFilter = mapperFilter;
            _MapperResponse = mapperResponse;
            _Profiles = profiles;
            _Profiles.Add(new HackerrankByTeamResponseProfile());
        }

        public async Task<List<HackerrankByTeamResponse>> Handle(HackerrankByTeamRequest request, CancellationToken cancellationToken)
        {
            var resultRequest = await _MapperFilter.MapperItemToListAsync(request);
            var listGet = new List<HackerrankDomainByTeam>();
            foreach(var item in resultRequest)
            {
                var resultGet = await _IHackerrankService.GetFootballMatchesByTeam(item);
                if (resultGet.Success)
                    listGet.Add(resultGet.Item);
            }

            var result = new List<HackerrankByTeamResponse>();
            var facResponse = await _MapperResponse.Create(_Profiles);
            foreach (var item in listGet)
            {
                var resultMapper = await facResponse.MapperAsync(item);
                result.Add(resultMapper);
            }

            return result;

        }
    }
}
