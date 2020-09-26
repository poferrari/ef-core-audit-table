using DomainHistoryEF.Infra.CrossCutting.IoC.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DomainHistoryEF.Infra.CrossCutting.IoC
{
    public static class IoC
    {
        public static IServiceCollection ConfigureContainer(this IServiceCollection services, IConfiguration configuration)
        {
            DataModule.Register(services, configuration);
            DomainModule.Register(services);
            return services;
        }
    }
}
