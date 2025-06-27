using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Domain.EntitiesDomains.Joins;
using Infrastructure.EntitiesDataBases.Joins;

namespace Domain.Map.CustomerBankAccountsService
{
    public class ResponseJoinMapper : IMapperSpecific<CustomerBankAccountsAndBankAccountsDomain, CustomersBankAccountsAndBankAccounts>
    {
        public async Task<CustomerBankAccountsAndBankAccountsDomain> MapperAsync(CustomersBankAccountsAndBankAccounts? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new CustomerBankAccountsAndBankAccountsDomain(
                item.GuidBankAccounts, item.AccountNumber, item.JointAccount, item.GuidCustomerBankAccounts, item.AccountHolder);
        }

        public async Task<List<CustomerBankAccountsAndBankAccountsDomain>> MapperAsync(List<CustomersBankAccountsAndBankAccounts>? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = new List<CustomerBankAccountsAndBankAccountsDomain>();
            foreach (var obj in item)
                result.Add(new CustomerBankAccountsAndBankAccountsDomain(
                    obj.GuidBankAccounts, obj.AccountNumber, obj.JointAccount, obj.GuidCustomerBankAccounts, obj.AccountHolder));
            return result;
        }

        public async Task<CustomersBankAccountsAndBankAccounts> MapperAsync(CustomerBankAccountsAndBankAccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomersBankAccountsAndBankAccounts>> MapperAsync(List<CustomerBankAccountsAndBankAccountsDomain>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomersBankAccountsAndBankAccounts>> MapperItemToListAsync(CustomerBankAccountsAndBankAccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerBankAccountsAndBankAccountsDomain>> MapperItemToListAsync(CustomersBankAccountsAndBankAccounts? item)
        {
            throw new NotImplementedException();
        }
    }
}
