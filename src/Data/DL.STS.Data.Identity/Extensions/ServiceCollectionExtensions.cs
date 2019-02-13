using DL.STS.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DL.STS.Data.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityService(this IServiceCollection services, IConfiguration configuration)
        {
            string dbConnectionString = configuration.GetConnectionString("appDbConnection");

            string assemblyNamespace = typeof(AppIdentityDbContext).Namespace;

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(dbConnectionString, optionsBuilder =>
                    optionsBuilder.MigrationsAssembly(assemblyNamespace)
                )
            );

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
