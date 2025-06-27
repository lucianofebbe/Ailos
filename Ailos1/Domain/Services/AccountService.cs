using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Util.TransportsResults;
using AutoMapper;
using Domain.Abstracts.Withdraw.Base;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.AccountsService;
using Domain.Interfaces;
using Domain.Profiles.AccountsService;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Interfaces.Readers.Get;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.EntitiesDataBases;

namespace Domain.Services
{
    public class AccountService : IAccountService
    {
        private IGetAccountReader _IGetAccountReader;
        private ICreateAccountCommand _ICreateAccountCommand;
        private ABaseWithdraw _ABaseWithdraw;
        private IMapperSpecificFactory<GetAccountFilter, GetAccountParameter> _MapperGetFilter;
        private IMapperSpecific<AccountsDomain, Accounts> _MapperGetResponse;
        private IMapperSpecificFactory<CreateAccountFilter, CreateAccountParameter> _MapperCreateFilter;
        private IList<Profile> _IProfiles;

        public AccountService(IGetAccountReader iGetAccountReader,
            ICreateAccountCommand iCreateAccountCommand,
            ABaseWithdraw aBaseWithdraw,
            IMapperSpecificFactory<GetAccountFilter, GetAccountParameter> mapperGetFilter,
            IMapperSpecific<AccountsDomain, Accounts> mapperGetResponse,
            IMapperSpecificFactory<CreateAccountFilter, CreateAccountParameter> mapperCreateFilter,
            IList<Profile> iProfiles)
        {
            _IGetAccountReader = iGetAccountReader;
            _ICreateAccountCommand = iCreateAccountCommand;
            _ABaseWithdraw = aBaseWithdraw;
            _MapperGetFilter = mapperGetFilter;
            _MapperGetResponse = mapperGetResponse;
            _MapperCreateFilter = mapperCreateFilter;
            _IProfiles = iProfiles;
            _IProfiles.Add(new CreateProfile());
            _IProfiles.Add(new GetProfile());
        }

        public async Task<TransportResult<AccountsDomain>> CreateAsync(CreateAccountFilter createAccountFilter)
        {
            var mapCreate = await _MapperCreateFilter.Create(_IProfiles);
            var parameterCreate = await mapCreate.MapperAsync(createAccountFilter);
            var resultCreate = await _ICreateAccountCommand.CreateAsync(parameterCreate);
            if (resultCreate.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultCreate.Item);
                return TransportResult<AccountsDomain>.Create(mapResponse);
            }

            return TransportResult<AccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<AccountsDomain>> DepositAsync(AccountsDomain account, CreateAccountFilter createAccountFilter)
        {
            var CreateAccount = new CreateAccountFilter()
            {
                CurrentBalance = createAccountFilter.CurrentBalance + account.CurrentBalance,
                IdFather = account.Id,
                IdBankAccount = createAccountFilter.IdBankAccount
            };

            var resultCreateAsync = await CreateAsync(CreateAccount);

            if (resultCreateAsync.Success)
                return TransportResult<AccountsDomain>.Create(resultCreateAsync.Item);

            return TransportResult<AccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<AccountsDomain>> WithdrawAsync(AccountsDomain account, CreateAccountFilter createAccountFilter)
        {
            var CreateAccount = new CreateAccountParameter()
            {
                CurrentBalance = createAccountFilter.CurrentBalance,
                IdFather = account.Id,
                IdBankAccount = createAccountFilter.IdBankAccount
            };

            var resultWithdraw = await _ABaseWithdraw.Calc(CreateAccount);

            if (resultWithdraw.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultWithdraw.Item);
                return TransportResult<AccountsDomain>.Create(mapResponse);
            }

            return TransportResult<AccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar sacar");
        }

        public async Task<TransportResult<AccountsDomain>> GetAsync(GetAccountFilter createAccountFilter)
        {
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createAccountFilter);
            var resultGet = await _IGetAccountReader.GetAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<AccountsDomain>.Create(mapResponse);
            }

            return TransportResult<AccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }

        public async Task<TransportResult<AccountsDomain>> GetByIdBankAccountAsync(GetAccountFilter createAccountFilter)
        {
            var mapFilter = await _MapperGetFilter.Create(_IProfiles);
            var parameterGet = await mapFilter.MapperAsync(createAccountFilter);
            var resultGet = await _IGetAccountReader.GetByIdBankAccountAsync(parameterGet);
            if (resultGet.Success)
            {
                var mapResponse = await _MapperGetResponse.MapperAsync(resultGet.Item);
                return TransportResult<AccountsDomain>.Create(mapResponse);
            }

            return TransportResult<AccountsDomain>.Create(null, notFoundMessage: "Erro ao tentar cadastrar");
        }
    }
}
