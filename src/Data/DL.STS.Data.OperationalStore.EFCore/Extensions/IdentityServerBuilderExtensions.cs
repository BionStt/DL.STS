using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace DL.STS.Data.OperationalStore.EFCore.Extensions
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddEFOperationalStore(this IIdentityServerBuilder builder,
            string connectionString)
        {
            string assemblyNamespace = typeof(IdentityServerBuilderExtensions)
                .GetTypeInfo()
                .Assembly
                .GetName()
                .Name;

            builder.AddOperationalStore(options =>
                options.ConfigureDbContext = b =>
                    b.UseSqlServer(connectionString, optionsBuilder =>
                        optionsBuilder.MigrationsAssembly(assemblyNamespace)
                    )
            );

            // TODO: caching

            return builder;
        }
    }
}
