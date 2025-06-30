using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Commands;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Readers;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Readers;
using Services.Domain;
using Services.Filters.ContaCorrenteService;
using Services.Interfaces.ContaCorrenteService;
using Services.Profiles.ContaCorrenteService;
using Entitie = Domain.Entities.Sql;

namespace Services.Services
{
    public class ContaCorrenteService : IContaCorrenteService
    {
        private IContaCorrenteCreate _IContaCorrenteCreate;
        private IMapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter> _IMapperCreateFilter;
        private IContaCorrenteByNumeroDaConta _IContaCorrenteContaCorrenteByNumeroDaConta;
        private IMapperSpecificFactory<ContaCorrenteByNumeroDaContaParameter, GetContaCorrenteFilter> _IMapperGetContaCorrenteFilter;

        private IMapperSpecific<ContaCorrente, Entitie.ContaCorrente> _IMapperContaCorrenteResult;
        private IList<Profile> _IProfiles;

        public ContaCorrenteService(
            IContaCorrenteCreate iContaCorrenteCreate,
            IMapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter> iMapperCreateFilter,
            IMapperSpecific<ContaCorrente, Entitie.ContaCorrente> iMapperContaCorrenteResult,
            IContaCorrenteByNumeroDaConta iContaCorrenteContaCorrenteByNumeroDaConta,
            IMapperSpecificFactory<ContaCorrenteByNumeroDaContaParameter, GetContaCorrenteFilter> iMapperGetContaCorrenteFilter,
            IList<Profile> iProfiles)
        {
            _IContaCorrenteCreate = iContaCorrenteCreate;
            _IMapperCreateFilter = iMapperCreateFilter;
            _IMapperContaCorrenteResult = iMapperContaCorrenteResult;
            _IContaCorrenteContaCorrenteByNumeroDaConta = iContaCorrenteContaCorrenteByNumeroDaConta;
            _IMapperGetContaCorrenteFilter = iMapperGetContaCorrenteFilter;
            _IProfiles = iProfiles;
        }

        public async Task<TransportResult<ContaCorrente>> CreateAsync(CreateFilter item)
        {
            _IProfiles.Add(new MapperCreateFilterProfile());
            var mapper = await _IMapperCreateFilter.Create(_IProfiles);
            var parameter = await mapper.MapperAsync(item);

            var result = await _IContaCorrenteCreate.CreateAsync(parameter);

            if (result.Success)
            {
                var response = await _IMapperContaCorrenteResult.MapperAsync(result.Item);
                return TransportResult<ContaCorrente>.Create(response);
            }

            return TransportResult<ContaCorrente>.Create(null);
        }

        public async Task<TransportResult<ContaCorrente>> GetContaCorrenteAsync(GetContaCorrenteFilter item)
        {
            _IProfiles.Add(new GetContaCorrenteFilterProfile());
            var mapper = await _IMapperGetContaCorrenteFilter.Create(_IProfiles);
            var parameter = await mapper.MapperAsync(item);

            if (item.IncluirAtivas == null)
                parameter.IncluirAtivas = true;

            if (item.IncluirDeletadas == null)
                parameter.IncluirDeletadas = false;

            var result = await _IContaCorrenteContaCorrenteByNumeroDaConta.GetByNumeroDaConta(parameter);
            if (result.Success)
            {
                if (result.Item.Ativo && !result.Item.Deleted)
                    return TransportResult<ContaCorrente>.Create(await _IMapperContaCorrenteResult.MapperAsync(result.Item));
                if (!result.Item.Ativo && result.Item.Deleted)
                    return TransportResult<ContaCorrente>.Create(null, notFoundMessage: "Conta Exluida.");
                if(!result.Item.Ativo && !result.Item.Deleted)
                    return TransportResult<ContaCorrente>.Create(null, notFoundMessage: "Conta Inativa.");
            }
            return TransportResult<ContaCorrente>.Create(null, notFoundMessage: "Conta nao encontrada.");
        }
    }
}
