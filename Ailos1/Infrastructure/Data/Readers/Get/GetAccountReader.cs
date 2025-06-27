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
    public class GetAccountReader : IGetAccountReader
    {
        private IUnitOfWorkFactory<Accounts> _Factory;
        private ConnectionSettings _Settings;

        public GetAccountReader(
            IUnitOfWorkFactory<Accounts> factory,
            ConnectionSettings settings)
        {
            _Factory = factory;
            _Settings = settings;
        }

        public async Task<TransportResult<Accounts>> GetAsync(GetAccountParameter getAccountParameter)
        {
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<Accounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(getAccountParameter, parameters),
                Parameters = parameters
            });
            return TransportResult<Accounts>.Create(result);
        }

        public async Task<TransportResult<Accounts>> GetByIdBankAccountAsync(GetAccountParameter getAccountParameter)
        {
            var parameter = new GetAccountParameter() { IdBankAccount = getAccountParameter.IdBankAccount };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<Accounts>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<Accounts>.Create(result);
        }

        private string GetQueryBuilder(GetAccountParameter getAccountParameter, DynamicParameters parameters)
        {
            var queryBuilder = new StringBuilder(AccountsQuerys.Get());
            var conditions = new List<string>();

            if (getAccountParameter.Id > 0)
            {
                conditions.Add("Id = @Id");
                parameters.Add("@Id", getAccountParameter.Id);
            }
            if (getAccountParameter.IdBankAccount > 0)
            {
                conditions.Add("IdBankAccount = @IdBankAccount");
                parameters.Add("@IdBankAccount", getAccountParameter.IdBankAccount);
            }
            if (getAccountParameter.Guid != Guid.Empty)
            {
                conditions.Add("Guid = @Guid");
                parameters.Add("@Guid", getAccountParameter.Guid);
            }

            if (conditions.Any())
            {
                queryBuilder.Append(" WHERE ");
                queryBuilder.Append(string.Join(" AND ", conditions));
            }

            queryBuilder.Append(" ORDER BY Id DESC");

            return queryBuilder.ToString();
        }
    }
}
