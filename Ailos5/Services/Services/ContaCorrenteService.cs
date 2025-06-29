using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Commands;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Services.Domain;
using Services.Filters.ContaCorrenteService;
using Services.Interfaces;
using Services.Profiles.ContaCorrenteService;
using Entitie = Domain.Entities.Sql;

namespace Services.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private IContaCorrenteComand _Comand;
        private IMapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter> _MapperFilter;
        private IMapperSpecific<ContaCorrente, Entitie.ContaCorrente> _MapperResult;
        private IList<Profile> _Profiles;

        public ContaCorrenteService(
            IContaCorrenteComand comand,
            IMapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter> mapperFilter,
            IMapperSpecific<ContaCorrente, Entitie.ContaCorrente> mapperResult,
            IList<Profile> profiles)
        {
            _Comand = comand;
            _MapperFilter = mapperFilter;
            _MapperResult = mapperResult;
            _Profiles = profiles;
        }

        public async Task<TransportResult<ContaCorrente>> CreateAsync(CreateFilter item)
        {
            _Profiles.Add(new MapperFilterProfile());
            var mapper = await _MapperFilter.Create(_Profiles);
            var parameter = await mapper.MapperAsync(item);

            var result = await _Comand.CreateAsync(parameter);

            if (result.Success)
            {
                var response = await _MapperResult.MapperAsync(result.Item);
                return TransportResult<ContaCorrente>.Create(response);
            }

            return TransportResult<ContaCorrente>.Create(null);
        }
    }
}
