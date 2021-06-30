using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vaquinha.App.Config;
using Vaquinha.App.Interfaces.Repository;
using Vaquinha.App.Interfaces.Service;
using Vaquinha.App.Repository;
using Vaquinha.App.Repository.Context;
using Vaquinha.App.Service;
using Vaquinha.App.Service.AutoMapper;

namespace Vaquinha.App.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddDbContext(this IServiceCollection services)
        {
            services.AddDbContext<CrowfundingOnlineDBContext>(opt => opt.UseInMemoryDatabase("Vakinha"));

            return services;
        }

        public static IServiceCollection AddIocConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ICauseService, CauseService>();
            services.AddScoped<IHomeInfoService, HomeInfoService>();

            services.AddScoped<IDomainNotificationService, DomainNotificationService>();
            services.AddScoped<IDonationService, DonationService>();
            services.AddScoped<IDonationRepository, DonationRepository>();

            services.AddScoped<ICauseRepository, CauseRepository>();
            services.AddScoped<IHomeInfoRepository, HomeInfoRepository>();

            return services;
        }

        public static IServiceCollection AddAutoMapper(this IServiceCollection services, IConfiguration configuration)
        {
            var globalAppSettings = new GlobalAppConfig();
            configuration.Bind("ConfigurationApp", globalAppSettings);

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CrowfundingOnlineMappingProfile>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var config = new GlobalAppConfig();

            configuration.Bind("ConfigurationApp", config);
            services.AddSingleton(config);

            return services;
        }
    }
}