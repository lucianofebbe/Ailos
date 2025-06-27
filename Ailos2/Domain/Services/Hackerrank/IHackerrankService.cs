using AilosInfra.Util.TransportsResults;
using Domain.EntitiesDomain.Hackerrank;
using Domain.Services.Hackerrank.Settings;

namespace Domain.Services.Hackerrank
{
    public interface IHackerrankService
    {
        Task<TransportResult<List<HackerrankDomain>>> GetFootballMatches();
        Task<TransportResult<HackerrankDomainByTeam>> GetFootballMatchesByTeam(HackerrankSettings settings);
    }
}
