using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWork;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using Domain.Data.SqlServer.ContaCorrente.Commands;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Readers;
using Domain.Data.SqlServer.ContaCorrente.Readers;
using Domain.Entities.Sql;
using NSubstitute;
using System.Data;

namespace DomainTests
{
    public class ContaCorrenteTest
    {
        [Fact]
        public async Task CreateAsync()
        {
            // Arrange
            var parameter = new ContaCorrenteCreateParameter
            {
                NomeDoCliente = "Cliente Teste",
                Ativo = true,
                Deleted = false
            };

            var entityInserida = new ContaCorrente
            {
                Id = 1, // simula sucesso no insert
                NomeDoCliente = parameter.NomeDoCliente,
                Ativo = parameter.Ativo,
                Deleted = parameter.Deleted
            };

            // Simula unit of work e factory
            var unitOfWork = Substitute.For<IUnitOfWork<ContaCorrente>>();
            unitOfWork.InsertAsync(Arg.Any<CommandSettings<ContaCorrente>>())
                .Returns(entityInserida);

            var factory = Substitute.For<IUnitOfWorkFactory<ContaCorrente>>();
            factory.Create(Arg.Any<ConnectionSettings>()).Returns(Task.FromResult(unitOfWork));

            // Mock da conexão do settings
            var connSettings = new ConnectionSettings
            {
                Connection = Substitute.For<IDbConnection>(),
                EnableTransaction = false
            };

            var service = new ContaCorrenteCreate(factory, connSettings);

            // Act
            var result = await service.CreateAsync(parameter);

            // Assert
            Assert.True(result.Success);
            Assert.NotNull(result.Item);
            Assert.Equal(parameter.NomeDoCliente, result.Item.NomeDoCliente);
            Assert.True(result.Item.Ativo);
            Assert.False(result.Item.Deleted);
            Assert.NotEqual(Guid.Empty, result.Item.NumeroDaConta);
        }

        [Fact]
        public async Task GetByNumeroDaConta()
        {
            // Arrange
            var numeroDaConta = Guid.NewGuid();
            var parameter = new ContaCorrenteByNumeroDaContaParameter
            {
                NumeroDaConta = numeroDaConta,
            };

            var contaMock = new ContaCorrente
            {
                Id = 10,
                NumeroDaConta = numeroDaConta,
                NomeDoCliente = "Cliente Teste",
                Ativo = true,
                Deleted = false
            };

            var unitOfWork = Substitute.For<IUnitOfWork<ContaCorrente>>();
            unitOfWork.GetAsync(Arg.Any<CommandSettings<ContaCorrente>>())
                .Returns(contaMock);

            var factory = Substitute.For<IUnitOfWorkFactory<ContaCorrente>>();
            factory.Create(Arg.Any<ConnectionSettings>())
                .Returns(Task.FromResult(unitOfWork));

            var settings = new ConnectionSettings
            {
                Connection = Substitute.For<IDbConnection>(),
                EnableTransaction = false
            };

            var service = new ContaCorrenteByNumeroDaConta(factory, settings);

            var result = await service.GetByNumeroDaConta(parameter);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(numeroDaConta, result.Item.NumeroDaConta);
            Assert.Equal("Cliente Teste", result.Item.NomeDoCliente);
        }
    }
}