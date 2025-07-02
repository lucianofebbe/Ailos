using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWork;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using Domain.Data.SqlServer.Movimento.Commands;
using Domain.Data.SqlServer.Movimento.Parameters.Commands;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Domain.Data.SqlServer.Movimento.Readers;
using Domain.Entities.Sql;
using NSubstitute;
using System.Data;

namespace DomainTests
{
    public class MovimentoTest
    {
        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var param = new MovimentoCreateParameter
            {
                IdFather = 1,
                IdContaCorrente = 99,
                DataMovimento = DateTime.UtcNow,
                TipoMovimento = 'c',
                Valor = 1500.00m,
                Deleted = false
            };

            var mockResult = new Movimento
            {
                Id = 1,
                IdFather = param.IdFather,
                IdContaCorrente = param.IdContaCorrente,
                DataMovimento = param.DataMovimento,
                TipoMovimento = param.TipoMovimento,
                Valor = param.Valor,
                Deleted = param.Deleted
            };

            var unitOfWork = Substitute.For<IUnitOfWork<Movimento>>();
            unitOfWork.InsertAsync(Arg.Any<CommandSettings<Movimento>>()).Returns(mockResult);

            var factory = Substitute.For<IUnitOfWorkFactory<Movimento>>();
            factory.Create(Arg.Any<ConnectionSettings>()).Returns(Task.FromResult(unitOfWork));

            var connectionSettings = new ConnectionSettings
            {
                Connection = Substitute.For<IDbConnection>(),
                EnableTransaction = false
            };

            var service = new MovimentoCreate(factory, connectionSettings);

            // Act
            var result = await service.CreateAsync(param);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(param.Valor, result.Item.Valor);
            Assert.Equal(param.IdContaCorrente, result.Item.IdContaCorrente);
            Assert.True(result.Item.Guid != Guid.Empty);
        }

        [Fact]
        public async Task GetByIdUltimoMovimentoContaCorrente()
        {
            // Arrange
            var param = new UltimoMovimentoGetByIdContaCorrenteParameter
            {
                IdContaCorrente = 10
            };

            var mockResult = new Movimento
            {
                Id = 123,
                IdContaCorrente = param.IdContaCorrente,
                TipoMovimento = 'c',
                Valor = 200,
                Deleted = false
            };

            var unitOfWork = Substitute.For<IUnitOfWork<Movimento>>();
            unitOfWork.GetAsync(Arg.Any<CommandSettings<Movimento>>()).Returns(mockResult);

            var factory = Substitute.For<IUnitOfWorkFactory<Movimento>>();
            factory.Create(Arg.Any<ConnectionSettings>()).Returns(Task.FromResult(unitOfWork));

            var connectionSettings = new ConnectionSettings
            {
                Connection = Substitute.For<IDbConnection>(),
                EnableTransaction = false
            };

            var service = new UltimoMovimentoByIdContaCorrente(factory, connectionSettings);

            // Act
            var result = await service.GetByIdUltimoMovimentoContaCorrente(param);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(param.IdContaCorrente, result.Item.IdContaCorrente);
            Assert.Equal(mockResult.Id, result.Item.Id);
        }
    }
}
