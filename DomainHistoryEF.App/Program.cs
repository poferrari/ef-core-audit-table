using DomainHistoryEF.Domain.Catalogs.Services;
using DomainHistoryEF.Infra.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace DomainHistoryEF.App
{
    public static class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = BuildConfiguration();

            var serviceCollection = new ServiceCollection();

            ConfigurationServices(serviceCollection, configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();

            var service = serviceProvider.GetRequiredService<IChangeProductService>();
            service.Send();

            Environment.Exit(0);
        }

        private static void ConfigurationServices(ServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(configuration);

            IoC.ConfigureContainer(services, configuration);
        }

        private static IConfiguration BuildConfiguration()
            => new ConfigurationBuilder()
                            .SetBasePath(Directory.GetCurrentDirectory())
                            .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                            .AddEnvironmentVariables()
                            .Build();
    }
}
