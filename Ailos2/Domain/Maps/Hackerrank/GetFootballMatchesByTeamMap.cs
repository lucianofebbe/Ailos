using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Domain.EntitiesDomain.Hackerrank;
using Infrastructure.Entities.Hackerrank;

namespace Domain.Maps.Hackerrank
{
    public class GetFootballMatchesByTeamMap : IMapperSpecific<FootballMatchesByTeam, HackerrankDomainByTeam>
    {
        public Task<FootballMatchesByTeam> MapperAsync(HackerrankDomainByTeam? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<FootballMatchesByTeam>> MapperAsync(List<HackerrankDomainByTeam>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<HackerrankDomainByTeam> MapperAsync(FootballMatchesByTeam? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            int goalsTryParse = 0;
            int goalsResult = 0;
            foreach (var obj in item.data)
                if (int.TryParse(obj.team1goals, out goalsTryParse))
                    goalsResult += goalsTryParse;

            if (item.data.Any())
            {
                string team = item.data.FirstOrDefault().team1;
                int year = item.data.FirstOrDefault().year;
                string result = $"Team {team} scored {goalsResult.ToString()} goals in {year}";
                return new HackerrankDomainByTeam(year, team, result);
            }
            return new HackerrankDomainByTeam(0, string.Empty);
        }

        public Task<List<HackerrankDomainByTeam>> MapperAsync(List<FootballMatchesByTeam>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<HackerrankDomainByTeam>> MapperItemToListAsync(FootballMatchesByTeam? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<FootballMatchesByTeam>> MapperItemToListAsync(HackerrankDomainByTeam? item)
        {
            throw new NotImplementedException();
        }
    }
}
