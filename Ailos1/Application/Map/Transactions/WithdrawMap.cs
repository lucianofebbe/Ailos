using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using Application.Responses.Transactions;
using Domain.EntitiesDomains.Sigles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Map.Transactions
{
    internal class WithdrawMap : IMapperSpecific<WithdrawResponse, AccountsDomain>
    {
        public async Task<WithdrawResponse> MapperAsync(AccountsDomain? item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            return new WithdrawResponse() { CurrentBalance = item.CurrentBalance };
        }

        public Task<List<WithdrawResponse>> MapperAsync(List<AccountsDomain>? item)
        {
            throw new NotImplementedException();
        }

        public Task<AccountsDomain> MapperAsync(WithdrawResponse? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountsDomain>> MapperAsync(List<WithdrawResponse>? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<AccountsDomain>> MapperItemToListAsync(WithdrawResponse? item)
        {
            throw new NotImplementedException();
        }

        public Task<List<WithdrawResponse>> MapperItemToListAsync(AccountsDomain? item)
        {
            throw new NotImplementedException();
        }
    }
}
