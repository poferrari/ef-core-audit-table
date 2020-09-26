using DomainHistoryEF.Infra.CrossCutting.IoC;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

namespace DomainHistoryEF.AppWorker
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config
                         .SetBasePath(Directory.GetCurrentDirectory())
                         .AddJsonFile($"appsettings.json", optional: true, reloadOnChange: true)
                         .AddEnvironmentVariables();
                 })
                .ConfigureServices((hostContext, services) =>
                {
                    services.ConfigureContainer(hostContext.Configuration);
                    services.AddHostedService<Worker>();
                });
    }
}
