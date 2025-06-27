using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Api.Profiles.Hackerrank;
using Api.Requests.Hackerrank;
using Api.Responses.Hackerrank;
using AutoMapper;
using Domain.EntitiesDomain.Hackerrank;
using Domain.Services.Hackerrank;
using MediatR;

namespace Api.Handlers.Hackerrank
{
    public class HackerrankHandler : IRequestHandler<HackerrankRequest, List<HackerrankResponse>>
    {
        private IHackerrankService _IHackerrankService;
        private IMapperSpecificFactory<List<HackerrankDomain>, List<HackerrankResponse>> _Mapper;
        private IList<Profile> _Profiles;

        public HackerrankHandler(
            IHackerrankService hackerrankService,
            IMapperSpecificFactory<List<HackerrankDomain>, List<HackerrankResponse>> mapper,
            IList<Profile> profiles)
        {
            _IHackerrankService = hackerrankService;
            _Mapper = mapper;
            _Profiles = profiles;
            _Profiles.Add(new HackerrankProfile());
        }

        public async Task<List<HackerrankResponse>> Handle(HackerrankRequest request, CancellationToken cancellationToken)
        {
            var getResult = await _IHackerrankService.GetFootballMatches();

            var facMapper = await _Mapper.Create(_Profiles);
            var mapperResult = await facMapper.MapperAsync(getResult.Item);

            return mapperResult;
        }
    }
}
