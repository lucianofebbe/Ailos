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
    public class GetCustomerReader : IGetCustomerReader
    {
        private IUnitOfWorkFactory<Customers> _Factory;
        private ConnectionSettings _Settings;

        public GetCustomerReader(
            IUnitOfWorkFactory<Customers> factory,
            ConnectionSettings settings)
        {
            this._Factory = factory;
            this._Settings = settings;
        }

        public async Task<TransportResult<Customers>> GetAsync(GetCustomerParameter getCustomerParameter)
        {
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<Customers>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(getCustomerParameter, parameters),
                Parameters = parameters
            });
            return TransportResult<Customers>.Create(result);
        }

        public async Task<TransportResult<Customers>> GetByGuidAsync(GetCustomerParameter getCustomerParameter)
        {
            var parameter = new GetCustomerParameter() { Guid = getCustomerParameter.Guid };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<Customers>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<Customers>.Create(result);
        }

        public async Task<TransportResult<Customers>> GetByCPFAsync(GetCustomerParameter getCustomerParameter)
        {
            var parameter = new GetCustomerParameter() { CPF = getCustomerParameter.CPF };
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            var result = await fac.GetAsync(new CommandSettings<Customers>()
            {
                CommandType = TypeCommand.Query,
                Query = GetQueryBuilder(parameter, parameters),
                Parameters = parameters
            });
            return TransportResult<Customers>.Create(result);
        }

        private string GetQueryBuilder(GetCustomerParameter getCustomerParameter, DynamicParameters parameters)
        {
            var queryBuilder = new StringBuilder(CustomerQuerys.Get());
            var conditions = new List<string>();

            if (getCustomerParameter.Guid != Guid.Empty)
            {
                conditions.Add("Guid = @Guid");
                parameters.Add("@Guid", getCustomerParameter.Guid);
            }
            if (!string.IsNullOrEmpty(getCustomerParameter.Name))
            {
                conditions.Add("NameCustomer = @NameCustomer");
                parameters.Add("@NameCustomer", getCustomerParameter.Name);
            }
            if (!string.IsNullOrEmpty(getCustomerParameter.CPF))
            {
                conditions.Add("CPF = @CPF");
                parameters.Add("@CPF", getCustomerParameter.CPF);
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
