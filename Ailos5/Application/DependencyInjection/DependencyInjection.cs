using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using Application.Handlers.ContaCorrente;
using Application.Middleware;
using Application.Requests.ContaCorrente;
using Application.Requests.Movimento;
using Application.Responses.ContaCorrente;
using Application.Responses.Movimento;
using AutoMapper;
using Domain.Data.SqlServer.ContaCorrente.Commands;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Commands;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Readers;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Readers;
using Domain.Data.SqlServer.ContaCorrente.Readers;
using Domain.Data.SqlServer.Movimento.Commands;
using Domain.Data.SqlServer.Movimento.Interfaces.Commands;
using Domain.Data.SqlServer.Movimento.Interfaces.Readers;
using Domain.Data.SqlServer.Movimento.Parameters.Readers;
using Domain.Data.SqlServer.Movimento.Readers;
using Domain.Entities.Sql;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NReJSON;
using Services.Domain;
using Services.Filters.ContaCorrenteService;
using Services.Filters.MovimentoService;
using Services.Interfaces.ContaCorrenteService;
using Services.Interfaces.MovimentoService;
using Services.Maps.ContaCorrenteService;
using Services.Maps.MovimentoService;
using Services.Services;
using System.Data.SqlClient;
using DapperIUnit = AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using DapperSettings = AilosInfra.Settings.DataBases.Dapper.Settings;
using DapperUnit = AilosInfra.DataBases.Dapper.UnitOfWorkFactory;
using EntitieDomain = Domain.Entities.Sql;
using EntitieServices = Services.Domain;
using RedisDbUnit = AilosInfra.DataBases.RedisDb.UnitOfWorkFactory;
using RedisIDbUnit = AilosInfra.Interfaces.DataBase.RedisDb.UnitOfWorkFactory;
using RedisSettings = AilosInfra.Settings.DataBases.RedisDb.Settings;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ContaCorrenteCreateHandler>(); // ou qualquer classe que esteja no mesmo projeto dos handlers
            });

            //Injeta Lista de Profiles vazia para cada requisicao para o MapperFactory utilizar
            services.AddScoped<IList<Profile>>(provider => new List<Profile>());

            //utilizado pela lib Dapper
            services.AddTransient(provider =>
            {
                var connectionString = configuration.GetConnectionString("SqlServerConnection");
                return new ConnectionSettings
                {
                    Connection = new SqlConnection(connectionString),
                    ConnectionString = connectionString,
                    EnableTransaction = false
                };
            });

            //utilizado pelo Redis
            var endPoints = new List<string>();
            endPoints.Add(configuration.GetConnectionString("RedisConnection1"));
            services.AddTransient(provider => new RedisSettings.ConnectionSettings
            {
                EndPoints = endPoints,
                Password = "Admin"
            });


            AddInfrastructureToolKit(services, configuration);
            AddInfrastructure(services, configuration);
            AddInfrastructureDependeces(services, configuration);
            AddDomain(services, configuration);
            AddDomainDependeces(services, configuration);
            AddApplication(services, configuration);
            AddApplicationDependeces(services, configuration);
            AddServices(services, configuration);
            AddServicesDependeces(services, configuration);
            AddApi(services, configuration);
            return services;
        }

        public static void AddInfrastructureToolKit(IServiceCollection services, IConfiguration configuration)
        {
        }

        public static void AddInfrastructure(IServiceCollection services, IConfiguration configuration)
        {

        }

        public static void AddInfrastructureDependeces(IServiceCollection services, IConfiguration configuration)
        {

        }

        public static void AddDomain(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContaCorrenteCreate, ContaCorrenteCreate>();
            services.AddScoped<IMovimentoByIdContaCorrente, MovimentoByIdContaCorrente>();
            services.AddScoped<IContaCorrenteByNumeroDaConta, ContaCorrenteByNumeroDaConta>();
        }

        public static void AddDomainDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //ContaCorrenteComand
            services.AddScoped<DapperIUnit.IUnitOfWorkFactory<EntitieDomain.ContaCorrente>, DapperUnit.UnitOfWorkFactory<EntitieDomain.ContaCorrente>>();

            //MovimentoComand
            //MovimentoReader
            services.AddScoped<DapperIUnit.IUnitOfWorkFactory<EntitieDomain.Movimento>, DapperUnit.UnitOfWorkFactory<EntitieDomain.Movimento>>();

            //services.AddScoped<RedisIDbUnit.IUnitOfWorkFactory<Idempotence>, RedisDbUnit.UnitOfWorkFactory<Idempotence>>();
            services.AddScoped<DapperIUnit.IUnitOfWorkFactory<EntitieDomain.Idempotence>,DapperUnit.UnitOfWorkFactory<EntitieDomain.Idempotence>>();
        }

        public static void AddApplication(IServiceCollection services, IConfiguration configuration)
        {

        }

        public static void AddApplicationDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //ContaCorrenteHandler
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
            services.AddScoped<IMapperSpecificFactory<CreateFilter, ContaCorrenteCreateRequest>, MapperSpecificFactory<CreateFilter, ContaCorrenteCreateRequest>>();
            services.AddScoped<IMapperSpecificFactory<ContaCorrenteCreateResponse, EntitieServices.ContaCorrente>, MapperSpecificFactory<ContaCorrenteCreateResponse, EntitieServices.ContaCorrente>>();

            //GetSaldoAtualHandler
            services.AddScoped<IMovimentoService, MovimentoService>();
            services.AddScoped<IMapperSpecificFactory<GetSaldoAtualRequest, GetSaldoAtualFilter>, MapperSpecificFactory<GetSaldoAtualRequest, GetSaldoAtualFilter>>();
            services.AddScoped<IMapperSpecificFactory<EntitieServices.Movimento, GetSaldoAtualResponse>, MapperSpecificFactory<EntitieServices.Movimento, GetSaldoAtualResponse>>();

            //InitMovimentoHandler
            services.AddScoped<IMovimentoService, MovimentoService>();
            services.AddScoped<IMapperSpecificFactory<InitMovimentoRequest, InitMovimentoFilter>, MapperSpecificFactory<InitMovimentoRequest, InitMovimentoFilter>>();
            services.AddScoped<IMapperSpecificFactory<EntitieServices.Movimento, InitMovimentoResponse>, MapperSpecificFactory<EntitieServices.Movimento, InitMovimentoResponse>>();
        }

        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
            services.AddScoped<IMovimentoService, MovimentoService>();
        }

        public static void AddServicesDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //ContaCorrenteService
            services.AddScoped<IContaCorrenteCreate, ContaCorrenteCreate>();
            services.AddScoped<IMapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter>, MapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter>>();
            services.AddScoped<IMapperSpecific<EntitieServices.ContaCorrente, EntitieDomain.ContaCorrente>, ContaCorrenteResultMap>();
            services.AddScoped<IMapperSpecificFactory<ContaCorrenteByNumeroDaContaParameter, GetContaCorrenteFilter>, MapperSpecificFactory<ContaCorrenteByNumeroDaContaParameter, GetContaCorrenteFilter>>();
            services.AddScoped<IMapperSpecific<EntitieServices.ContaCorrente, EntitieDomain.ContaCorrente>, ContaCorrenteResultMap>();
            services.AddScoped<IMapperSpecificFactory<GetSaldoAtualFilter, InitMovimentoFilter>, MapperSpecificFactory<GetSaldoAtualFilter, InitMovimentoFilter>>();

            //MovimentoService
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
            services.AddScoped<IMapperSpecificFactory<GetSaldoAtualFilter, GetContaCorrenteFilter>, MapperSpecificFactory<GetSaldoAtualFilter, GetContaCorrenteFilter>>();
            services.AddScoped<IMovimentoByIdContaCorrente, MovimentoByIdContaCorrente>();
            services.AddScoped<IMapperSpecificFactory<MovimentoGetByIdContaCorrenteParameter, EntitieServices.ContaCorrente>, MapperSpecificFactory<MovimentoGetByIdContaCorrenteParameter, EntitieServices.ContaCorrente>>();
            services.AddScoped<IMapperSpecificFactory<InitMovimentoFilter, GetContaCorrenteFilter>, MapperSpecificFactory<InitMovimentoFilter, GetContaCorrenteFilter>>();
            services.AddScoped<IMapperSpecificFactory<EntitieServices.ContaCorrente, UltimoMovimentoGetByIdContaCorrenteParameter>, MapperSpecificFactory<EntitieServices.ContaCorrente, UltimoMovimentoGetByIdContaCorrenteParameter>>();
            services.AddScoped<IUltimoMovimentoByIdContaCorrente, UltimoMovimentoByIdContaCorrente>();
            services.AddScoped<IMovimentoCreate, MovimentoCreate>();
            services.AddScoped<IMapperSpecific<EntitieServices.Movimento, EntitieDomain.Movimento>, InitMovimentoResultMap>();

        }

        public static void AddApi(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
