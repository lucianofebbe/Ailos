using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Domain.EntitiesDomain.Hackerrank;
using Infrastructure.Entities.Hackerrank;

namespace Domain.Maps.Hackerrank
{
    public class GetFootballMatchesMap : IMapperSpecific<FootballMatches, List<HackerrankDomain>>
    {
        public Task<FootballMatches> MapperAsync(List<HackerrankDomain>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<FootballMatches>> MapperAsync(List<List<HackerrankDomain>>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HackerrankDomain>> MapperAsync(FootballMatches? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = new List<HackerrankDomain>();
            foreach (var data in item.data)
                result.Add(new HackerrankDomain(data.competition, data.year, data.round, data.team1, data.team2,
                    data.team1goals.ToString(), data.team2goals.ToString()));

            return result;
        }

        public Task<List<List<HackerrankDomain>>> MapperAsync(List<FootballMatches>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<List<HackerrankDomain>>> MapperItemToListAsync(FootballMatches? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<FootballMatches>> MapperItemToListAsync(List<HackerrankDomain>? item)
        {
            throw new NotImplementedException();
        }
    }
}
