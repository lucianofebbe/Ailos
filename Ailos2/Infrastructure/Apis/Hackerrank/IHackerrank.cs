using AilosInfra.Util.TransportsResults;
using Infrastructure.Apis.Hackerrank.Settings;
using Infrastructure.Entities.Hackerrank;

namespace Infrastructure.Apis.Hackerrank
{
    public interface IHackerrank
    {
        Task<TransportResult<FootballMatches>> GetFootballMatches();
        Task<TransportResult<FootballMatchesByTeam>> GetFootballMatchesByTeam(GetHackerrankSettings settings);
    }
}
