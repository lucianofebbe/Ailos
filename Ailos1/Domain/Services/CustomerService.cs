using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.CustomerService;
using Domain.Interfaces;
using Domain.Profiles.CustomerService;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Interfaces.Readers.Get;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.EntitiesDataBases;

namespace Domain.Services
{
    public class CustomerService : ICustomerService
    {
        private IGetCustomerReader _IGetCustomerReader;
        private ICreateCustomerCommand _ICreateCustomerCommand;
        private IMapperSpecificFactory<GetCustomerFilter, GetCustomerParameter> _MapperGetFilter;
        private IMapperSpecific<CustomerDomain, Customers> _MapperGetResponse;
        private IMapperSpecificFactory<CreateCustomerFilter, CreateCustomerParameter> _MapperCreateFilter;
        private IList<Profile> _IProfiles;
        public CustomerService(
            IGetCustomerReader iGetCustomerReader,
            ICreateCustomerCommand iCreateCustomerCommand,
            IMapperSpecificFactory<GetCustomerFilter, GetCustomerParameter> mapperGetFilter,
            IMapperSpecific<CustomerDomain, Customers> mapperGetResponse,
            IMapperSpecificFactory<CreateCustomerFilter, CreateCustomerParameter> mapperCreateFilter,
            IList<Profile> iProfiles)
        {
            _IGetCustomerReader = iGetCustomerReader;
            _ICreateCustomerCommand = iCreateCustomerCommand;
            _MapperGetFilter = mapperGetFilter;
            _MapperGetResponse = mapperGetResponse;
            _MapperCreateFilter = mapperCreateFilter;
            _IProfiles = iProfiles;
        }

        public async Task<TransportResult<CustomerDomain>> CreateAsync(CreateCustomerFilter createAccountFilter)
        {
            _IProfiles.Add(new CreateProfile());
            var mapCreate = await _MapperCreateFilter.Create(_IProfiles);
            var parameterCreate = await mapCreate.MapperAsync(createAccountFilter);
            var resultCreate = await _ICreateCustomerCommand.CreateAsync(parameterCreate);
            if (resultCreate.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultCreate.Item);
                return TransportResult<CustomerDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<CustomerDomain>> GetAsync(GetCustomerFilter createAccountFilter)
        {
            _IProfiles.Add(new GetProfile());
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createAccountFilter);
            var resultGet = await _IGetCustomerReader.GetAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<CustomerDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerDomain>.Create(null, notFoundMessage: "Erro ao tentar buscar");
        }

        public async Task<TransportResult<CustomerDomain>> GetByGuidAsync(GetCustomerFilter createAccountFilter)
        {
            _IProfiles.Add(new GetProfile());
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createAccountFilter);

            var resultGet = await _IGetCustomerReader.GetByGuidAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<CustomerDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerDomain>.Create(null, notFoundMessage: "Erro ao tentar buscar");
        }

        public async Task<TransportResult<CustomerDomain>> GetByCPFAsync(GetCustomerFilter createAccountFilter)
        {
            _IProfiles.Add(new GetProfile());
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createAccountFilter);

            var resultGet = await _IGetCustomerReader.GetByCPFAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<CustomerDomain>.Create(mapResponse);
            }

            return TransportResult<CustomerDomain>.Create(null, notFoundMessage: "Cpf Nao cadastrado");
        }
    }
}
