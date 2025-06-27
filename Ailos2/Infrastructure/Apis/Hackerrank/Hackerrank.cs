using AilosInfra.Interfaces.Apis.ApiExternal.ApiExternalFactory;
using AilosInfra.Util.TransportsResults;
using Infrastructure.Apis.Hackerrank.Settings;
using Infrastructure.Entities.Hackerrank;
using System.Text;

namespace Infrastructure.Apis.Hackerrank
{
    public class Hackerrank : IHackerrank
    {
        private IApiExternalFactory<FootballMatches> _ApiFootballMatches;
        private IApiExternalFactory<FootballMatchesByTeam> _ApiFootballMatchesByTeam;
        private readonly string _BaseUrl = "https://jsonmock.hackerrank.com/api/";

        public Hackerrank(
            IApiExternalFactory<FootballMatches> apiFootballMatches,
            IApiExternalFactory<FootballMatchesByTeam> apiFootballMatchesByTeam)
        {
            _ApiFootballMatches = apiFootballMatches;
            _ApiFootballMatchesByTeam = apiFootballMatchesByTeam;
        }

        public async Task<TransportResult<FootballMatches>> GetFootballMatches()
        {
            string address = "football_matches";
            var fac = await _ApiFootballMatches.Create(_BaseUrl);
            var result = await fac.GetAsync(address);
            return TransportResult<FootballMatches>.Create(result);
        }

        public async Task<TransportResult<FootballMatchesByTeam>> GetFootballMatchesByTeam(GetHackerrankSettings settings)
        {
            StringBuilder address = new StringBuilder($"football_matches");
            var conditions = new List<string>();

            if (settings.Year > 0)
                conditions.Add($"year={settings.Year}");

            if (!string.IsNullOrEmpty(settings.Team1))
                conditions.Add($"team1={settings.Team1}");

            if (!string.IsNullOrEmpty(settings.Team2))
                conditions.Add($"team2={settings.Team2}");

            if (settings.Page > 0)
                conditions.Add($"page={settings.Page}");

            if (conditions.Any())
            {
                address.Append("?");
                address.Append(string.Join("&", conditions));
            }
            else
                address.Append("/");

            var fac = await _ApiFootballMatchesByTeam.Create(_BaseUrl);

            var relativeUrl = address.ToString();
            var result = await fac.GetAsync(relativeUrl);

            return TransportResult<FootballMatchesByTeam>.Create(result);
        }
    }
}
