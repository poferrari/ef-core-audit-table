using DomainHistoryEF.Domain.Catalogs.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DomainHistoryEF.Infra.CrossCutting.IoC.Modules
{
    public static class DomainModule
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IChangeProductService, ChangeProductService>();
        }
    }
}
