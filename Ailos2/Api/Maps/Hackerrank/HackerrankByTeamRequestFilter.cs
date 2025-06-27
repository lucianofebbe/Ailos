using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Api.Requests.Hackerrank;
using Domain.Services.Hackerrank.Settings;

namespace Api.Maps.Hackerrank
{
    public class HackerrankByTeamRequestFilter : IMapperSpecific<HackerrankSettings, HackerrankByTeamRequest>
    {
        public Task<HackerrankSettings> MapperAsync(HackerrankByTeamRequest? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<HackerrankSettings>> MapperAsync(List<HackerrankByTeamRequest>? item)
        {
            throw new NotImplementedException();
        }

        public Task<HackerrankByTeamRequest> MapperAsync(HackerrankSettings? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<HackerrankByTeamRequest>> MapperAsync(List<HackerrankSettings>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<HackerrankByTeamRequest>> MapperItemToListAsync(HackerrankSettings? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<HackerrankSettings>> MapperItemToListAsync(HackerrankByTeamRequest? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = new List<HackerrankSettings>();
            foreach (var obj in item.Teams)
                result.Add(new HackerrankSettings() { Team = obj.Team, Year = obj.Year });
            return result;
        }
    }
}
