using DL.STS.Data.Identity.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DL.STS.Data.Identity.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddIdentityService(this IServiceCollection services, string connectionString)
        {
            string assemblyNamespace = typeof(AppIdentityDbContext).Namespace;

            services.AddDbContext<AppIdentityDbContext>(options =>
                options.UseSqlServer(connectionString, optionsBuilder =>
                    optionsBuilder.MigrationsAssembly(assemblyNamespace)
                )
            );

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>()
                .AddDefaultTokenProviders();
        }
    }
}
