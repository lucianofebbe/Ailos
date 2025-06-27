using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.ValidNewAccount;
using Application.Requests.ValidNewAccount;
using Application.Responses.ValidNewAccount;
using AutoMapper;
using Domain.Filters.AccountsService;
using Domain.Filters.BankAccountsService;
using Domain.Filters.CustomerBankAccountsService;
using Domain.Filters.CustomerService;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.ValidNewAccount
{
    public class AccountCreateHandler : IRequestHandler<AccountCreateRequest, AccountCreateResponse>
    {
        private IMapperSpecificFactory<GetCustomerFilter, AccountCreateRequest> _FacGetCustomerMapper;
        private ICustomerService _CustomerService;
        private IMapperSpecificFactory<CreateBankAccountFilter, AccountCreateRequest> _FacCreateBankAccountMapper;
        private IBankAccountService _BankAccountService;
        private IMapperSpecificFactory<CreateAccountFilter, AccountCreateRequest> _FacCreateAccountMapper;
        private IAccountService _AccountService;
        private IMapperSpecificFactory<CreateCustomerBankAccountsFilter, AccountCreateRequest> _FacCreateCustomersBankAccountsMapper;
        private ICustomerBankAccountsService _CustomersBankAccountsService;
        private IList<Profile> _Profiles;

        public AccountCreateHandler(
            IMapperSpecificFactory<GetCustomerFilter, AccountCreateRequest> facGetCustomerMapper,
            ICustomerService customerService,
            IMapperSpecificFactory<CreateBankAccountFilter, AccountCreateRequest> facCreateBankAccountMapper,
            IBankAccountService bankAccountService,
            IMapperSpecificFactory<CreateAccountFilter, AccountCreateRequest> facCreateAccountMapper,
            IAccountService accountService,
            IMapperSpecificFactory<CreateCustomerBankAccountsFilter, AccountCreateRequest> facCreateCustomersBankAccountsMapper,
            ICustomerBankAccountsService customersBankAccountsService,
            IList<Profile> profiles)
        {
            _FacGetCustomerMapper = facGetCustomerMapper;
            _CustomerService = customerService;
            _FacCreateBankAccountMapper = facCreateBankAccountMapper;
            _BankAccountService = bankAccountService;
            _FacCreateAccountMapper = facCreateAccountMapper;
            _AccountService = accountService;
            _FacCreateCustomersBankAccountsMapper = facCreateCustomersBankAccountsMapper;
            _CustomersBankAccountsService = customersBankAccountsService;
            _Profiles = profiles;
            _Profiles.Add(new GetCustomerMapperProfile());
            _Profiles.Add(new CreateBankAccountProfile());
            _Profiles.Add(new CreateAccountProfile());
            _Profiles.Add(new CreateCustomersBankAccountsProfile());
        }

        public async Task<AccountCreateResponse> Handle(AccountCreateRequest request, CancellationToken cancellationToken)
        {
            var mapGetCustomer = await _FacGetCustomerMapper.Create(_Profiles);
            var mapCustomer = await mapGetCustomer.MapperAsync(request);
            var resultCustomer = await _CustomerService.GetAsync(mapCustomer);

            var mapCreateBankAccount = await _FacCreateBankAccountMapper.Create(_Profiles);
            var mapBankAccount = await mapCreateBankAccount.MapperAsync(request);
            var resultBankAccount = await _BankAccountService.CreateAsync(mapBankAccount);

            var mapCreateAccount = await _FacCreateAccountMapper.Create(_Profiles);
            var mapAccount = await mapCreateAccount.MapperAsync(request);
            mapAccount.IdBankAccount = resultBankAccount.Item.Id;
            var resultAccount = await _AccountService.CreateAsync(mapAccount);

            var mapCreateCustomersBankAccounts = await _FacCreateCustomersBankAccountsMapper.Create(_Profiles);
            var mapCustomersBankAccounts = await mapCreateCustomersBankAccounts.MapperAsync(request);
            mapCustomersBankAccounts.IdCustomer = resultCustomer.Item.Id;
            mapCustomersBankAccounts.IdBankAccount = resultBankAccount.Item.Id;
            mapCustomersBankAccounts.AccountHolder = request.AccountHolder;
            var resultCustomersBankAccounts = await _CustomersBankAccountsService.CreateAsync(mapCustomersBankAccounts);

            if (resultCustomer.Success && resultBankAccount.Success && resultAccount.Success && resultCustomersBankAccounts.Success)
                return new AccountCreateResponse() { AccountNumber = resultBankAccount.Item.AccountNumber };
            return new AccountCreateResponse();
        }
    }
}
