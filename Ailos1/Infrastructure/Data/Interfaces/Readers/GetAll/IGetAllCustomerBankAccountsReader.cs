using AilosInfra.Util.TransportsResults;
using Infrastructure.Data.Parameters.Readers.GetAll;
using Infrastructure.EntitiesDataBases.Joins;

namespace Infrastructure.Data.Interfaces.Readers.GetAll
{
    public interface IGetAllCustomerBankAccountsReader
    {
        Task<TransportResult<List<CustomersBankAccountsAndBankAccounts>>> GetAllByIdCustomerAsync(GetAllCustomerParameter getCustomersBankAccountsParameter);
    }
}
