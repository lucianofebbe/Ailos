using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Application.Responses.ValidNewAccount;
using Domain.EntitiesDomains.Joins;
using Domain.EntitiesDomains.Sigles;

namespace Application.Map.ValidNewAccount
{
    public class ValidNewAccountMap : IMapperSpecific<CustomersBankAccountsResponse, List<CustomerBankAccountsAndBankAccountsDomain>>
    {
        public async Task<CustomersBankAccountsResponse> MapperAsync(List<CustomerBankAccountsAndBankAccountsDomain>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomersBankAccountsResponse>> MapperAsync(List<List<CustomerBankAccountsAndBankAccountsDomain>>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerBankAccountsAndBankAccountsDomain>> MapperAsync(CustomersBankAccountsResponse? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<List<CustomerBankAccountsAndBankAccountsDomain>>> MapperAsync(List<CustomersBankAccountsResponse>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<List<CustomerBankAccountsAndBankAccountsDomain>>> MapperItemToListAsync(CustomersBankAccountsResponse? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomersBankAccountsResponse>> MapperItemToListAsync(List<CustomerBankAccountsAndBankAccountsDomain>? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = new List<CustomersBankAccountsResponse>();
            foreach (var obj in item)
                result.Add(new CustomersBankAccountsResponse(obj.GuidBankAccounts, obj.AccountNumber, obj.JointAccount, obj.GuidCustomerBankAccounts, obj.AccountHolder));
            return result;
        }
    }
}
