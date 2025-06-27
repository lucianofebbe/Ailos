using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.BankAccountsService;
using Domain.Interfaces;
using Domain.Profiles.BankAccountsService;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Interfaces.Readers.Get;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.EntitiesDataBases;

namespace Domain.Services
{
    public class BankAccountService : IBankAccountService
    {
        private IGetBankAccountReader _IGetBankAccountReader;
        private ICreateBankAccountCommand _ICreateBankAccountCommand;
        private IMapperSpecificFactory<GetBankAccountFilter, GetBankAccountParameter> _MapperGetFilter;
        private IMapperSpecific<BankAccountsDomain, BankAccounts> _MapperGetResponse;
        private IMapperSpecificFactory<CreateBankAccountFilter, CreateBankAccountParameter> _MapperCreateFilter;
        private IList<Profile> _IProfiles;
        public BankAccountService(IGetBankAccountReader iGetBankAccountReader,
            ICreateBankAccountCommand iCreateBankAccountCommand,
            IMapperSpecificFactory<GetBankAccountFilter, GetBankAccountParameter> mapperGetFilter,
            IMapperSpecific<BankAccountsDomain, BankAccounts> mapperGetResponse,
            IMapperSpecificFactory<CreateBankAccountFilter, CreateBankAccountParameter> mapperCreateFilter,
            IList<Profile> iProfiles)
        {
            _IGetBankAccountReader = iGetBankAccountReader;
            _ICreateBankAccountCommand = iCreateBankAccountCommand;
            _MapperGetFilter = mapperGetFilter;
            _MapperGetResponse = mapperGetResponse;
            _MapperCreateFilter = mapperCreateFilter;
            _IProfiles = iProfiles;
            _IProfiles.Add(new CreateProfile());
            _IProfiles.Add(new GetProfile());
        }

        public async Task<TransportResult<BankAccountsDomain>> CreateAsync(CreateBankAccountFilter createBankAccountFilter)
        {
            var mapCreate = await _MapperCreateFilter.Create(_IProfiles);
            var parameterCreate = await mapCreate.MapperAsync(createBankAccountFilter);
            var resultCreate = await _ICreateBankAccountCommand.CreateAsync(parameterCreate);
            if (resultCreate.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultCreate.Item);
                return TransportResult<BankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<BankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<BankAccountsDomain>> GetAsync(GetBankAccountFilter createBankAccountFilter)
        {
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createBankAccountFilter);

            var resultGet = await _IGetBankAccountReader.GetAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<BankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<BankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<BankAccountsDomain>> GetByIdAsync(GetBankAccountFilter createBankAccountFilter)
        {
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createBankAccountFilter);

            var resultGet = await _IGetBankAccountReader.GetByIdAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<BankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<BankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<BankAccountsDomain>> GetByGuidAsync(GetBankAccountFilter createBankAccountFilter)
        {
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createBankAccountFilter);

            var resultGet = await _IGetBankAccountReader.GetByGuidAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<BankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<BankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<BankAccountsDomain>> GetByAccountNumberAsync(GetBankAccountFilter createBankAccountFilter)
        {
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createBankAccountFilter);

            var resultGet = await _IGetBankAccountReader.GetByAccountNumberAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<BankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<BankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }
    }
}
