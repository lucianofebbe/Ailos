using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.EntitiesDomain.Hackerrank;
using Domain.Profiles.Hackerrank;
using Domain.Services.Hackerrank.Settings;
using Infrastructure.Apis.Hackerrank;
using Infrastructure.Apis.Hackerrank.Settings;
using Infrastructure.Entities.Hackerrank;

namespace Domain.Services.Hackerrank
{
    public class HackerrankService : IHackerrankService
    {
        private IHackerrank _IHackerrank;
        private IMapperSpecific<FootballMatches, List<HackerrankDomain>> _MapperGetFootballMatches;
        private IMapperSpecific<FootballMatchesByTeam, HackerrankDomainByTeam> _MapperGetFootballMatchesByTeam;
        private IMapperSpecificFactory<GetHackerrankSettings, HackerrankSettings> _MapperSettings;
        private IList<Profile> _IProfiles;

        public HackerrankService(
            IHackerrank iHackerrank,
            IMapperSpecific<FootballMatches, List<HackerrankDomain>> mapperGetFootballMatches,
            IMapperSpecific<FootballMatchesByTeam, HackerrankDomainByTeam> mapperGetFootballMatchesByTeam,
            IMapperSpecificFactory<GetHackerrankSettings, HackerrankSettings> mapperSettings,
            IList<Profile> iProfiles)
        {
            _IHackerrank = iHackerrank;
            _MapperGetFootballMatches = mapperGetFootballMatches;
            _MapperGetFootballMatchesByTeam = mapperGetFootballMatchesByTeam;
            _MapperSettings = mapperSettings;
            _IProfiles = iProfiles;
        }

        public async Task<TransportResult<List<HackerrankDomain>>> GetFootballMatches()
        {
            var result = await _IHackerrank.GetFootballMatches();
            if (result.Success)
            {
                var mapResult = await _MapperGetFootballMatches.MapperAsync(result.Item);
                return TransportResult<List<HackerrankDomain>>.Create(mapResult);
            }
            return TransportResult<List<HackerrankDomain>>.Create(null);
        }

        public async Task<TransportResult<HackerrankDomainByTeam>> GetFootballMatchesByTeam(HackerrankSettings settings)
        {
            _IProfiles.Add(new HackerrankSettingsProfile());
            var facMapper = await _MapperSettings.Create(_IProfiles);
            var mapperResult = await facMapper.MapperAsync(settings);

            var result = await _IHackerrank.GetFootballMatchesByTeam(mapperResult);
            if (result.Success)
            {
                var mapResult = await _MapperGetFootballMatchesByTeam.MapperAsync(result.Item);
                return TransportResult<HackerrankDomainByTeam>.Create(mapResult);
            }
            return TransportResult<HackerrankDomainByTeam>.Create(null);
        }
    }
}
