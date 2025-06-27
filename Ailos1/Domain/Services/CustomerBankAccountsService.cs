using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.EntitiesDomains.Joins;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.CustomerBankAccountsService;
using Domain.Interfaces;
using Domain.Profiles.CustomerBankAccountsService;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Interfaces.Readers.Get;
using Infrastructure.Data.Interfaces.Readers.GetAll;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.Data.Parameters.Readers.GetAll;
using Infrastructure.EntitiesDataBases;
using Infrastructure.EntitiesDataBases.Joins;

namespace Domain.Services
{
    public class CustomerBankAccountsService : ICustomerBankAccountsService
    {
        private IGetCustomerBankAccountsReader _IGetCustomersBankAccountsReader;
        private IGetAllCustomerBankAccountsReader _IGetAllCustomersBankAccountsReader;
        private ICreateCustomerBankAccountsCommand _ICreateCustomersBankAccountsCommand;
        private IMapperSpecificFactory<GetCustomerBankAccountsFilter, GetCustomerBankAccountsParameter> _MapperGetFilter;
        private IMapperSpecificFactory<GetAllCustomerBankAccountsFilter, GetAllCustomerParameter> _MapperGetAllFilter;
        private IMapperSpecific<CustomerBankAccountsDomain, CustomerBankAccounts> _MapperGetResponse;
        private IMapperSpecific<CustomerBankAccountsAndBankAccountsDomain, CustomersBankAccountsAndBankAccounts> _MapperGetResponseJoin;
        private IMapperSpecificFactory<CreateCustomerBankAccountsFilter, CreateCustomerBankAccountsParameter> _MapperCreateFilter;
        private IList<Profile> _IProfiles;

        public CustomerBankAccountsService(
            IGetCustomerBankAccountsReader iGetCustomersBankAccountsReader,
            ICreateCustomerBankAccountsCommand iCreateCustomersBankAccountsCommand,
            IGetAllCustomerBankAccountsReader iGetAllCustomersBankAccountsReader,
            IMapperSpecificFactory<GetCustomerBankAccountsFilter, GetCustomerBankAccountsParameter> mapperGetFilter,
            IMapperSpecificFactory<GetAllCustomerBankAccountsFilter, GetAllCustomerParameter> mapperGetAllFilter,
            IMapperSpecific<CustomerBankAccountsDomain, CustomerBankAccounts> mapperGetResponse,
            IMapperSpecific<CustomerBankAccountsAndBankAccountsDomain, CustomersBankAccountsAndBankAccounts> mapperGetResponseJoin,
            IMapperSpecificFactory<CreateCustomerBankAccountsFilter, CreateCustomerBankAccountsParameter> mapperCreateFilter,
            IList<Profile> iProfiles)
        {
            _IGetCustomersBankAccountsReader = iGetCustomersBankAccountsReader;
            _IGetAllCustomersBankAccountsReader = iGetAllCustomersBankAccountsReader;
            _ICreateCustomersBankAccountsCommand = iCreateCustomersBankAccountsCommand;
            _MapperGetFilter = mapperGetFilter;
            _MapperGetAllFilter = mapperGetAllFilter;
            _MapperGetResponse = mapperGetResponse;
            _MapperGetResponseJoin = mapperGetResponseJoin;
            _MapperCreateFilter = mapperCreateFilter;
            _IProfiles = iProfiles;
        }

        public async Task<TransportResult<CustomerBankAccountsDomain>> CreateAsync(CreateCustomerBankAccountsFilter createCustomersBankAccountsFilter)
        {
            _IProfiles.Add(new CreateProfile());
            var mapCreate = await _MapperCreateFilter.Create(_IProfiles);
            var parameterCreate = await mapCreate.MapperAsync(createCustomersBankAccountsFilter);
            var resultCreate = await _ICreateCustomersBankAccountsCommand.CreateAsync(parameterCreate);
            if (resultCreate.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultCreate.Item);
                return TransportResult<CustomerBankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerBankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<CustomerBankAccountsDomain>> GetAsync(GetCustomerBankAccountsFilter getCustomersBankAccountsFilter)
        {
            _IProfiles.Add(new GetProfile());
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(getCustomersBankAccountsFilter);
            var resultGet = await _IGetCustomersBankAccountsReader.GetAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<CustomerBankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerBankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar buscar");
        }

        public async Task<TransportResult<CustomerBankAccountsDomain>> GetByIdBankAccountAsync(GetCustomerBankAccountsFilter getCustomersBankAccountsFilter)
        {
            _IProfiles.Add(new GetProfile());
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(getCustomersBankAccountsFilter);

            var resultGet = await _IGetCustomersBankAccountsReader.GetByIdBankAccountAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<CustomerBankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerBankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar buscar");
        }

        public async Task<TransportResult<CustomerBankAccountsDomain>> GetByIdCustomerAsync(GetCustomerBankAccountsFilter getCustomersBankAccountsFilter)
        {
            _IProfiles.Add(new GetProfile());
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(getCustomersBankAccountsFilter);

            var resultGet = await _IGetCustomersBankAccountsReader.GetByIdCustomerAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<CustomerBankAccountsDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerBankAccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar buscar");
        }

        public async Task<TransportResult<List<CustomerBankAccountsAndBankAccountsDomain>>> GetAllByIdCustomerAsync(GetAllCustomerBankAccountsFilter getCustomersBankAccountsFilter) 
        {
            _IProfiles.Add(new GetProfile());
            var mapFilter = await _MapperGetAllFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(getCustomersBankAccountsFilter);

            var resultGet = await _IGetAllCustomersBankAccountsReader.GetAllByIdCustomerAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponseJoin.MapperAsync(resultGet.Item);
                return TransportResult<List<CustomerBankAccountsAndBankAccountsDomain>>.Create(mapResponse);
            }

            return TransportResult<List<CustomerBankAccountsAndBankAccountsDomain>>.Create(null, notFoundMessage: "Erro ao tentar buscar");
        }
    }
}
