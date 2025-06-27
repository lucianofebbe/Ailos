using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Infrastructure.Data.Interfaces.Readers.Get;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.Data.Querys;
using Infrastructure.EntitiesDataBases;
using System.Text;

namespace Infrastructure.Data.Readers.Get
{
    public class GetBankAccountReader : IGetBankAccountReader
    {
        private IUnitOfWorkFactory<BankAccounts> _Factory;
        private ConnectionSettings _Settings;

        public GetBankAccountReader(
            IUnitOfWorkFactory<BankAccounts> factory,
            ConnectionSettings settings)
        {
            this._Factory = factory;
            this._Settings = settings;
        }

        public async Task<TransportResult<BankAccounts>> GetAsync(GetBankAccountParameter getBankAccountParameter)
        {
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<BankAccounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(getBankAccountParameter, parameters),
                Parameters = parameters
            });
            return TransportResult<BankAccounts>.Create(result);
        }

        public async Task<TransportResult<BankAccounts>> GetByIdAsync(GetBankAccountParameter getBankAccountParameter)
        {
            var parameter = new GetBankAccountParameter() { Id = getBankAccountParameter.Id };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<BankAccounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<BankAccounts>.Create(result);
        }

        public async Task<TransportResult<BankAccounts>> GetByGuidAsync(GetBankAccountParameter getBankAccountParameter)
        {
            var parameter = new GetBankAccountParameter() { Guid = getBankAccountParameter.Guid };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<BankAccounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<BankAccounts>.Create(result);
        }

        public async Task<TransportResult<BankAccounts>> GetByAccountNumberAsync(GetBankAccountParameter getBankAccountParameter)
        {
            var parameter = new GetBankAccountParameter() { AccountNumber = getBankAccountParameter.AccountNumber };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<BankAccounts>()
            {
                Entity = new BankAccounts(),
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<BankAccounts>.Create(result);
        }

        private string GetQueryBuilder(GetBankAccountParameter getBankAccountParameter, DynamicParameters parameters)
        {
            var queryBuilder = new StringBuilder(BankAccountQuery.Get());
            var conditions = new List<string>();

            if (getBankAccountParameter.Id > 0)
            {
                conditions.Add("Id = @Id");
                parameters.Add("@Id", getBankAccountParameter.Id);
            }
            if (getBankAccountParameter.Guid != Guid.Empty)
            {
                conditions.Add("Guid = @Guid");
                parameters.Add("@Guid", getBankAccountParameter.Guid);
            }
            if (getBankAccountParameter.AccountNumber != Guid.Empty)
            {
                conditions.Add("AccountNumber = @AccountNumber");
                parameters.Add("@AccountNumber", getBankAccountParameter.AccountNumber);
            }

            if (conditions.Any())
            {
                queryBuilder.Append(" WHERE ");
                queryBuilder.Append(string.Join(" AND ", conditions));
            }

            return queryBuilder.ToString();
        }
    }
}
