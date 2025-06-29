using AilosInfra.DataBases.Dapper.UnitOfWorkFactory;
using AilosInfra.Interfaces.DataBase.Dapper.UnitOfWorkFactory;
using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Settings.DataBases.Dapper.Settings;
using Application.Handlers;
using Application.Requests.ContaCorrente;
using Application.Responses.ContaCorrente;
using AutoMapper;
using Domain.Data.SqlServer.ContaCorrente.Commands;
using Domain.Data.SqlServer.ContaCorrente.Interfaces.Commands;
using Domain.Data.SqlServer.ContaCorrente.Parameters.Commands;
using Domain.Data.SqlServer.Movimento.Interfaces.Commands;
using Domain.Data.SqlServer.Movimento.Interfaces.Readers;
using Domain.Data.SqlServer.Movimento.Readers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services.Domain;
using Services.Filters.ContaCorrenteService;
using Services.Interfaces;
using Services.Maps.ContaCorrenteService;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<ContaCorrenteHandler>(); // ou qualquer classe que esteja no mesmo projeto dos handlers
            });

            //Injeta Lista de Profiles vazia para cada requisicao para o MapperFactory utilizar
            services.AddScoped<IList<Profile>>(provider => new List<Profile>());

            //utilizado pela lib Dapper
            services.AddTransient(provider => new ConnectionSettings
            {
                Connection = new SqlConnection(configuration.GetConnectionString("DefaultConnection")),
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
            services.AddScoped<IContaCorrenteComand, ContaCorrenteComand>();
            services.AddScoped<IMovimentoReader, MovimentoReader>();
        }

        public static void AddDomainDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //ContaCorrenteComand
            services.AddScoped<IUnitOfWorkFactory<Domain.Entities.Sql.ContaCorrente>, UnitOfWorkFactory<Domain.Entities.Sql.ContaCorrente>>();

            //MovimentoComand
            //MovimentoReader
            services.AddScoped<IUnitOfWorkFactory<Domain.Entities.Sql.Movimento>, UnitOfWorkFactory<Domain.Entities.Sql.Movimento>>();
        }

        public static void AddApplication(IServiceCollection services, IConfiguration configuration)
        {

        }

        public static void AddApplicationDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //ContaCorrenteHandler
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
            services.AddScoped<IMapperSpecificFactory<CreateFilter, ContaCorrenteRequest>, MapperSpecificFactory<CreateFilter, ContaCorrenteRequest>>();
            services.AddScoped<IMapperSpecificFactory<ContaCorrenteResponse, Services.Domain.ContaCorrente>, MapperSpecificFactory<ContaCorrenteResponse, Services.Domain.ContaCorrente>>();
        }

        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContaCorrenteService, ContaCorrenteService>();
        }

        public static void AddServicesDependeces(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IContaCorrenteComand, ContaCorrenteComand>();
            services.AddScoped<IMapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter>, MapperSpecificFactory<ContaCorrenteCreateParameter, CreateFilter>>();
            services.AddScoped<IMapperSpecific<ContaCorrente, Domain.Entities.Sql.ContaCorrente>, CreateAsyncMap>();
        }

        public static void AddApi(IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
