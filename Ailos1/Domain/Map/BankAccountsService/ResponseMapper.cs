using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Domain.EntitiesDomains.Sigles;
using Infrastructure.EntitiesDataBases;

namespace Domain.Map.BankAccountsService
{
    public class ResponseMapper : IMapperSpecific<BankAccountsDomain, BankAccounts>
    {
        public async Task<BankAccountsDomain> MapperAsync(BankAccounts? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            return new BankAccountsDomain(item.Id, item.Guid, item.JointAccount, item.AccountNumber);
        }

        public async Task<List<BankAccountsDomain>> MapperAsync(List<BankAccounts>? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var result = new List<BankAccountsDomain>();
            foreach (var obj in item)
                result.Add(new BankAccountsDomain(obj.Id, obj.Guid, obj.JointAccount, obj.AccountNumber));
            return result;
        }

        public async Task<BankAccounts> MapperAsync(BankAccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BankAccounts>> MapperAsync(List<BankAccountsDomain>? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BankAccounts>> MapperItemToListAsync(BankAccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BankAccountsDomain>> MapperItemToListAsync(BankAccounts? item)
        {
            throw new NotImplementedException();
        }
    }
}
