using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using Application.Profiles.ValidNewAccount;
using Application.Requests.ValidNewAccount;
using Application.Responses.ValidNewAccount;
using AutoMapper;
using Domain.EntitiesDomains.Joins;
using Domain.Filters.CustomerBankAccountsService;
using Domain.Filters.CustomerService;
using Domain.Interfaces;
using MediatR;

namespace Application.Handlers.ValidNewAccount
{
    public class ValidNewAccountHandler : IRequestHandler<ValidNewAccountRequest, ValidNewAccountResponse>
    {
        private ICustomerService _ICustomerService;
        private IMapperSpecificFactory<GetCustomerFilter, ValidNewAccountRequest> _MapperGetCustomerFilter;
        private IMapperSpecific<CustomersBankAccountsResponse, List<CustomerBankAccountsAndBankAccountsDomain>> _MapperBankAccountsFilter;
        private ICustomerBankAccountsService _ICustomersBankAccountsService;
        private IList<Profile> _Profiles;
        public ValidNewAccountHandler(
            ICustomerService iCustomerService,
            ICustomerBankAccountsService iCustomersBankAccountsService,
            IMapperSpecificFactory<GetCustomerFilter, ValidNewAccountRequest> mapperGetCustomerFilter,
            IMapperSpecific<CustomersBankAccountsResponse, List<CustomerBankAccountsAndBankAccountsDomain>> mapperBankAccountsFilter,
            IList<Profile> profiles)
        {
            _Profiles = profiles;
            _ICustomerService = iCustomerService;
            _MapperGetCustomerFilter = mapperGetCustomerFilter;
            _MapperBankAccountsFilter = mapperBankAccountsFilter;
            _ICustomersBankAccountsService = iCustomersBankAccountsService;
        }

        public async Task<ValidNewAccountResponse> Handle(ValidNewAccountRequest request, CancellationToken cancellationToken)
        {
            _Profiles.Add(new ValidNewAccountProfile());
            var map = await _MapperGetCustomerFilter.Create(_Profiles);
            var filterCustomer = await map.MapperAsync(request);
            var customerResult = await _ICustomerService.GetByCPFAsync(filterCustomer);
            if (customerResult.Success)
            {
                var filterGetAll = new GetAllCustomerBankAccountsFilter() { IdCustomer = customerResult.Item.Id };
                var accounts = await _ICustomersBankAccountsService.GetAllByIdCustomerAsync(filterGetAll);
                var resultMap = await _MapperBankAccountsFilter.MapperItemToListAsync(accounts.Item);
                var customer = customerResult.Item;
                var result = new ValidNewAccountResponse(customer.Guid, customer.NameCustomer, customer.CPF, resultMap);
                return result;
            }
            return null;
        }
    }
}
