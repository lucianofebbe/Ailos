using AilosInfra.DataBases.Dapper.UnitOfWorkFactory;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using Application.Handlers.ValidNewAccount;
using Application.Map.NewDeposit;
using Application.Map.ValidNewAccount;
using Application.Requests.Customers;
using Application.Requests.NewDeposit;
using Application.Requests.ValidNewAccount;
using Application.Responses.Customers;
using Application.Responses.ValidNewAccount;
using AutoMapper;
using Domain.Abstracts.Withdraw.Base;
using Domain.Abstracts.Withdraw;
using Domain.EntitiesDomains.Joins;
using Domain.EntitiesDomains.Sigles;
using Domain.Filters.AccountsService;
using Domain.Filters.BankAccountsService;
using Domain.Filters.CustomerBankAccountsService;
using Domain.Filters.CustomerService;
using Domain.Interfaces;
using Domain.Map.CustomerBankAccountsService;
using Domain.Services;
using Infrastructure.Data.Commands.Create;
using Infrastructure.Data.Interfaces.Commands.Create;
using Infrastructure.Data.Interfaces.Readers.Get;
using Infrastructure.Data.Interfaces.Readers.GetAll;
using Infrastructure.Data.Parameters.Commands.Create;
using Infrastructure.Data.Parameters.Readers.Get;
using Infrastructure.Data.Parameters.Readers.GetAll;
using Infrastructure.Data.Readers.Get;
using Infrastructure.Data.Readers.GetAll;
using Infrastructure.EntitiesDataBases;
using Infrastructure.EntitiesDataBases.Joins;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Application.Handlers.Transactions;
using Application.Requests.Transactions;
using Application.Map.Transactions;
using Application.Responses.Transactions;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Método          Ciclo de Vida       Criado Quando               Compartilhado          Ideal Para
        /// AddSingleton	Aplicação inteira	Primeira chamada ou start	Sim	                   Cache, log, serviços globais
        /// AddScoped       Por requisição      Início do request           Apenas no request      DbContext, Unit of Work
        /// AddTransient    Cada chamada        Toda injeção                Não                    Validadores, serviços sem estado
        /// 
        /// A ordem de carregamento das dependencias importa, sempre começar pela mais baixa,
        /// no caso AddInfrastructureToolKit, AddInfrastructure, AddServices e por AddApplication
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ValidNewAccountHandler>(); // ou qualquer classe que esteja no mesmo projeto dos handlers
            });

            AddInfrastructureToolKit(services, configuration);
            AddInfrastructure(services, configuration);
            AddInfrastructureDependeces(services, configuration);
            AddDomain(services, configuration);
            AddDomainDependeces(services, configuration);
            AddApplication(services, configuration);
            AddApi(services, configuration);
            return services;
        }

        public static void AddInfrastructureToolKit(IServiceCollection services, IConfiguration configuration)
        {
        }

        public static void AddInfrastructure(IServiceCollection services, IConfiguration configuration)
        {
            //utilizado pela lib Dapper
            services.AddTransient(provider => new ConnectionSettings
                {
                    Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")),
                });

            services.AddTransient<ICreateAccountCommand, CreateAccountCommand>();
            services.AddTransient<ICreateBankAccountCommand, CreateBankAccountCommand>();
            services.AddTransient<ICreateCustomerCommand, CreateCustomerCommand>();
            services.AddTransient<ICreateCustomerBankAccountsCommand, CreateCustomerBankAccountsCommand>();
            services.AddTransient<IGetAccountReader, GetAccountReader>();
            services.AddTransient<IGetBankAccountReader, GetBankAccountReader>();
            services.AddTransient<IGetCustomerReader, GetCustomerReader>();
            services.AddTransient<IGetCustomerBankAccountsReader, GetCustomerBankAccountsReader>();
            services.AddTransient<IGetAllCustomerBankAccountsReader, GetAllCustomerBankAccountsReader>();
        }

        public static void AddInfrastructureDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //CreateAccountCommand
            //GetAccountReader
            services.AddScoped<IUnitOfWorkFactory<Accounts>, UnitOfWorkFactory<Accounts>>();

            //CreateBankAccountCommand
            //GetBankAccountReader
            services.AddScoped<IUnitOfWorkFactory<BankAccounts>, UnitOfWorkFactory<BankAccounts>>();

            //CreateCustomerCommand
            //GetCustomerReader
            services.AddScoped<IUnitOfWorkFactory<Customers>, UnitOfWorkFactory<Customers>>();

            //CreateCustomersBankAccountsCommand
            //GetCustomersBankAccountsReader
            services.AddScoped<IUnitOfWorkFactory<CustomerBankAccounts>, UnitOfWorkFactory<CustomerBankAccounts>>();

            //GetAllCustomersBankAccountsReader
            services.AddScoped<IUnitOfWorkFactory<CustomersBankAccountsAndBankAccounts>, UnitOfWorkFactory<CustomersBankAccountsAndBankAccounts>>();
        }

        public static void AddDomain(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<ICustomerService, CustomerService>();
        }

        public static void AddDomainDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //AccountService
            services.AddScoped<IGetAccountReader, GetAccountReader>();
            services.AddScoped<ICreateAccountCommand, CreateAccountCommand>();
            services.AddScoped<IMapperSpecificFactory<GetAccountFilter, GetAccountParameter>, MapperSpecificFactory<GetAccountFilter, GetAccountParameter>>();
            services.AddScoped<IMapperSpecific<AccountsDomain, Accounts>, Domain.Map.AccountsService.ResponseMapper>();
            services.AddScoped<IMapperSpecificFactory<CreateAccountFilter, CreateAccountParameter>, MapperSpecificFactory<CreateAccountFilter, CreateAccountParameter>>();
            services.AddScoped<ABaseWithdraw, Withdraw35>();

            //BankAccountService
            services.AddScoped<IGetBankAccountReader, GetBankAccountReader>();
            services.AddScoped<ICreateBankAccountCommand, CreateBankAccountCommand>();
            services.AddScoped<IMapperSpecificFactory<GetBankAccountFilter, GetBankAccountParameter>, MapperSpecificFactory<GetBankAccountFilter, GetBankAccountParameter>>();
            services.AddScoped<IMapperSpecific<BankAccountsDomain, BankAccounts>, Domain.Map.BankAccountsService.ResponseMapper>();
            services.AddScoped<IMapperSpecificFactory<CreateBankAccountFilter, CreateBankAccountParameter>, MapperSpecificFactory<CreateBankAccountFilter, CreateBankAccountParameter>>();

            //CustomersBankAccountsService
            services.AddScoped<IGetCustomerBankAccountsReader, GetCustomerBankAccountsReader>();
            services.AddScoped<IGetAllCustomerBankAccountsReader, GetAllCustomerBankAccountsReader>();
            services.AddScoped<ICreateCustomerBankAccountsCommand, CreateCustomerBankAccountsCommand>();
            services.AddScoped<IMapperSpecificFactory<GetCustomerBankAccountsFilter, GetCustomerBankAccountsParameter>, MapperSpecificFactory<GetCustomerBankAccountsFilter, GetCustomerBankAccountsParameter>>();
            services.AddScoped<IMapperSpecificFactory<GetAllCustomerBankAccountsFilter, GetAllCustomerParameter>, MapperSpecificFactory<GetAllCustomerBankAccountsFilter, GetAllCustomerParameter>>();
            services.AddScoped<IMapperSpecific<CustomerBankAccountsDomain, CustomerBankAccounts>, Domain.Map.CustomerBankAccountsService.ResponseMapper>();
            services.AddScoped<IMapperSpecific<CustomerBankAccountsAndBankAccountsDomain, CustomersBankAccountsAndBankAccounts>, ResponseJoinMapper>();
            services.AddScoped<IMapperSpecificFactory<CreateCustomerBankAccountsFilter, CreateCustomerBankAccountsParameter>, MapperSpecificFactory<CreateCustomerBankAccountsFilter, CreateCustomerBankAccountsParameter>>();

            //CustomerService
            services.AddScoped<IGetCustomerReader, GetCustomerReader>();
            services.AddScoped<ICreateCustomerCommand, CreateCustomerCommand>();
            services.AddScoped<IMapperSpecificFactory<GetCustomerFilter, GetCustomerParameter>, MapperSpecificFactory<GetCustomerFilter, GetCustomerParameter>>();
            services.AddScoped<IMapperSpecific<CustomerDomain, Customers>, Domain.Map.CustomerService.ResponseMapper>();
            services.AddScoped<IMapperSpecificFactory<CreateCustomerFilter, CreateCustomerParameter>, MapperSpecificFactory<CreateCustomerFilter, CreateCustomerParameter>>();
        }

        public static void AddApplication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IList<Profile>>(provider => new List<Profile>());

            //ValidNewAccountHandler
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMapperSpecificFactory<GetCustomerFilter, ValidNewAccountRequest>, MapperSpecificFactory<GetCustomerFilter, ValidNewAccountRequest>>();
            services.AddScoped<IMapperSpecific<CustomersBankAccountsResponse, List<CustomerBankAccountsAndBankAccountsDomain>>, ValidNewAccountMap>();
            services.AddScoped<ICustomerBankAccountsService, CustomerBankAccountsService>();

            //AccountCreateHandler
            services.AddScoped<IMapperSpecificFactory<GetCustomerFilter, AccountCreateRequest>, MapperSpecificFactory<GetCustomerFilter, AccountCreateRequest>>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMapperSpecificFactory<CreateBankAccountFilter, AccountCreateRequest>, MapperSpecificFactory<CreateBankAccountFilter, AccountCreateRequest>>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IMapperSpecificFactory<CreateAccountFilter, AccountCreateRequest>, MapperSpecificFactory<CreateAccountFilter, AccountCreateRequest>>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IMapperSpecificFactory<CreateCustomerBankAccountsFilter, AccountCreateRequest>, MapperSpecificFactory<CreateCustomerBankAccountsFilter, AccountCreateRequest>>();
            services.AddScoped<ICustomerBankAccountsService, CustomerBankAccountsService>();

            //CustomerCreateHandler
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IMapperSpecificFactory<CreateCustomerFilter, CustomerCreateRequest>, MapperSpecificFactory<CreateCustomerFilter, CustomerCreateRequest>>();
            services.AddScoped<IMapperSpecificFactory<CustomerDomain, CustomerCreateResponse>, MapperSpecificFactory<CustomerDomain, CustomerCreateResponse>>();

            //NewDepositHandler
            services.AddScoped<IMapperSpecificFactory<GetBankAccountFilter, NewDepositRequest>, MapperSpecificFactory<GetBankAccountFilter, NewDepositRequest>>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IMapperSpecific<GetAccountFilter, BankAccountsDomain>, CreateAccountMap>();
            services.AddScoped<IMapperSpecificFactory<CreateAccountFilter, GetAccountFilter>, MapperSpecificFactory<CreateAccountFilter, GetAccountFilter>>();
            services.AddScoped<IAccountService, AccountService>();

            //WithdrawHandler
            services.AddScoped<IMapperSpecificFactory<GetBankAccountFilter, WithdrawRequest>, MapperSpecificFactory<GetBankAccountFilter, WithdrawRequest>>();
            services.AddScoped<IBankAccountService, BankAccountService>();
            services.AddScoped<IMapperSpecific<GetAccountFilter, BankAccountsDomain>, CreateAccountMap>();
            services.AddScoped<IMapperSpecificFactory<CreateAccountFilter, GetAccountFilter>, MapperSpecificFactory<CreateAccountFilter, GetAccountFilter>>();
            services.AddScoped<IMapperSpecific<WithdrawResponse, AccountsDomain>, WithdrawMap>();
            services.AddScoped<IAccountService, AccountService>();

        }

        public static void AddApi(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
