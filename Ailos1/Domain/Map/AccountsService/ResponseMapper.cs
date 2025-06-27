using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Domain.EntitiesDomains.Sigles;
using Infrastructure.EntitiesDataBases;

namespace Domain.Map.AccountsService
{
    public class ResponseMapper : IMapperSpecific<AccountsDomain, Accounts>
    {
        public async Task<AccountsDomain> MapperAsync(Accounts? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new AccountsDomain(item.Id, item.Guid, item.CurrentBalance);
        }

        public Task<List<AccountsDomain>> MapperAsync(List<Accounts>? item)
        {
            throw new NotImplementedException();
        }

        public Task<Accounts> MapperAsync(AccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Accounts>> MapperAsync(List<AccountsDomain>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<Accounts>> MapperItemToListAsync(AccountsDomain? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountsDomain>> MapperItemToListAsync(Accounts? item)
        {
            throw new NotImplementedException();
        }
    }
}
