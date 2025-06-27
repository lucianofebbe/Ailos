using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Domain.EntitiesDomains.Sigles;
using Infrastructure.EntitiesDataBases;

namespace Domain.Map.CustomerBankAccountsService
{
    public class ResponseMapper : IMapperSpecific<CustomerBankAccountsDomain, CustomerBankAccounts>
    {
        public async Task<CustomerBankAccountsDomain> MapperAsync(CustomerBankAccounts? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new CustomerBankAccountsDomain(item.Guid, item.AccountHolder);
        }

        public async Task<List<CustomerBankAccountsDomain>> MapperAsync(List<CustomerBankAccounts>? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            var result = new List<CustomerBankAccountsDomain>();
            foreach (var obj in item)
                result.Add(new CustomerBankAccountsDomain(obj.Guid, obj.AccountHolder));
            return result;
        }

        public async Task<CustomerBankAccounts> MapperAsync(CustomerBankAccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerBankAccounts>> MapperAsync(List<CustomerBankAccountsDomain>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerBankAccounts>> MapperItemToListAsync(CustomerBankAccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CustomerBankAccountsDomain>> MapperItemToListAsync(CustomerBankAccounts? item)
        {
            throw new NotImplementedException();
        }
    }
}
