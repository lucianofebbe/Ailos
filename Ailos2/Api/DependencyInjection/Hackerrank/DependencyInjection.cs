using AilosInfra.Apis.ApiExternal.ApiExternalFactory;
using AilosInfra.Interfaces.Apis.ApiExternal.ApiExternalFactory;
using AilosInfra.Interfaces.Mappers.AutoMapper.Mapper;
using AilosInfra.Interfaces.Mappers.AutoMapper.MapperFactory;
using AilosInfra.Mappers.AutoMapper.Mapper;
using AilosInfra.Mappers.AutoMapper.MapperFactory;
using Api.Handlers.Hackerrank;
using Api.Maps.Hackerrank;
using Api.Requests.Hackerrank;
using Api.Responses.Hackerrank;
using AutoMapper;
using Domain.EntitiesDomain.Hackerrank;
using Domain.Maps.Hackerrank;
using Domain.Services.Hackerrank;
using Domain.Services.Hackerrank.Settings;
using Infrastructure.Apis.Hackerrank;
using Infrastructure.Apis.Hackerrank.Settings;
using Infrastructure.Entities.Hackerrank;

namespace Api.DependencyInjection.Hackerrank
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<HackerrankByTeamHandler>(); // ou qualquer classe que esteja no mesmo projeto dos handlers
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
            services.AddScoped<IHackerrank, Infrastructure.Apis.Hackerrank.Hackerrank>();
        }

        public static void AddInfrastructureDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //Hackerrank
            services.AddScoped<IApiExternalFactory<FootballMatches>, ApiExternalFactory<FootballMatches>>();
            services.AddScoped<IApiExternalFactory<FootballMatchesByTeam>, ApiExternalFactory<FootballMatchesByTeam>>();
        }

        public static void AddDomain(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IHackerrankService, HackerrankService>();
        }

        public static void AddDomainDependeces(IServiceCollection services, IConfiguration configuration)
        {
            //HackerrankService
            services.AddScoped<IMapperSpecific<FootballMatches, List<HackerrankDomain>>, GetFootballMatchesMap>();
            services.AddScoped<IMapperSpecific<FootballMatchesByTeam, HackerrankDomainByTeam>, GetFootballMatchesByTeamMap>();
            services.AddScoped<IMapperSpecificFactory<GetHackerrankSettings, HackerrankSettings>, MapperSpecificFactory<GetHackerrankSettings, HackerrankSettings>>();
        }

        public static void AddApplication(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IList<Profile>>(provider => new List<Profile>());
        }

        public static void AddApi(IServiceCollection services, IConfiguration configuration)
        {
            //HackerrankHandler
            services.AddScoped<IHackerrankService, HackerrankService>();
            services.AddScoped<IMapperSpecificFactory<List<HackerrankDomain>, List<HackerrankResponse>>, MapperSpecificFactory<List<HackerrankDomain>, List<HackerrankResponse>>>();

            //HackerrankByTeamHandler
            services.AddScoped<IHackerrankService, HackerrankService>();
            services.AddScoped<IMapperSpecific<HackerrankSettings, HackerrankByTeamRequest>, HackerrankByTeamRequestFilter>();
            services.AddScoped<IMapperSpecificFactory<HackerrankDomainByTeam, HackerrankByTeamResponse>, MapperSpecificFactory<HackerrankDomainByTeam, HackerrankByTeamResponse>>();
        }
    }
}
