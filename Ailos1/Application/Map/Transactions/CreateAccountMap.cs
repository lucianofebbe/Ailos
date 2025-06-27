using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Application.Responses.ValidNewAccount;
using Domain.EntitiesDomains.Joins;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.AccountsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Map.Transactions
{
    internal class CreateAccountMap : IMapperSpecific<GetAccountFilter, BankAccountsDomain>
    {
        public async Task<GetAccountFilter> MapperAsync(BankAccountsDomain? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new GetAccountFilter() { IdAccount = item.Id };
        }

        public Task<List<GetAccountFilter>> MapperAsync(List<BankAccountsDomain>? item)
        {
            throw new NotImplementedException();
        }

        public Task<BankAccountsDomain> MapperAsync(GetAccountFilter? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<BankAccountsDomain>> MapperAsync(List<GetAccountFilter>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<BankAccountsDomain>> MapperItemToListAsync(GetAccountFilter? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<GetAccountFilter>> MapperItemToListAsync(BankAccountsDomain? item)
        {
            throw new NotImplementedException();
        }
    }
}
