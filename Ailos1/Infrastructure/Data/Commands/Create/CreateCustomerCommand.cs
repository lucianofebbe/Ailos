using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using AilosInfra.Util.TransportsResults;
using Dapper;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Querys;
using Infrastructure.EntitiesDataBases;
using System.Text;

namespace Infrastructure.Data.Commands.Create
{
    public class CreateCustomerCommand : ICreateCustomerCommand
    {
        private IUnitOfWorkFactory<Customers> _Factory;
        private ConnectionSettings _Settings;

        public CreateCustomerCommand(
            IUnitOfWorkFactory<Customers> factory,
            ConnectionSettings settings)
        {
            _Factory = factory;
            _Settings = settings;
        }

        public async Task<TransportResult<Customers>> CreateAsync(CreateCustomerParameter createCustomerParameter)
        {
            var guid = Guid.NewGuid();
            var fac = await _Factory.Create(_Settings);
            var parameters = new DynamicParameters();
            parameters.Add("@NameCustomer", createCustomerParameter.NameCustomer);
            parameters.Add("@CPF", createCustomerParameter.CPF);
            parameters.Add("@Guid", guid);
            parameters.Add("@IdFather", 0);
            parameters.Add("@Created", DateTime.UtcNow);
            parameters.Add("@Updated", DateTime.UtcNow);
            parameters.Add("@Deleted", true);

            var result = await fac.InsertAsync(new CommandSettings<Customers>()
            {
                Entity = new Customers(),
                CommandType = TypeCommand.Query,
                Query = CustomerQuerys.Create(),
                Parameters = parameters,
                ExecuteScalar = true
            });
            if (result.Id > 0)
                result.Guid = guid;

            return TransportResult<Customers>.Create(result);
        }
    }
}
