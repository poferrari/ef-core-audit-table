using DomainHistoryEF.Domain.Catalogs.Repositories;
using DomainHistoryEF.Infra.Data.Contexts;
using DomainHistoryEF.Infra.Data.Repositories.Catalogs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DomainHistoryEF.Infra.CrossCutting.IoC.Modules
{
    public static class DataModule
    {
        private const string ConnectionStringName = "DefaultConnection";
        public static readonly ILoggerFactory EFLoggerFactory = LoggerFactory.Create(builder => { builder.AddDebug(); });

        public static void Register(IServiceCollection services, IConfiguration configuration)
        {
            var conexao = GetConnectionString(configuration);

            services.AddDbContext<DataContext>(optionsBuilder =>
                optionsBuilder
                       .UseSqlServer(conexao)
                       .UseLoggerFactory(EFLoggerFactory)
            );

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private static string GetConnectionString(IConfiguration configuration)
            => configuration.GetConnectionString(ConnectionStringName);
    }
}
