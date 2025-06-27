using AilosInfra.Util.TransportsResults;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.EntitiesDataBases;

namespace Infrastructure.Data.Interfaces.Commands.Create
{
    public interface ICreateCustomerBankAccountsCommand
    {
        Task<TransportResult<CustomerBankAccounts>> CreateAsync(CreateCustomerBankAccountsParameter createCustomersBankAccountsParameter);
    }
}
