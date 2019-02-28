using DL.STS.Host.App.Account.Queries.Handlers;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DL.STS.Host.App.Account.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAccountMediatR(this IServiceCollection services)
        {
            services.AddMediatR(typeof(GetLoginViewModelHandler).Assembly);

            return services;
        }
    }
}
