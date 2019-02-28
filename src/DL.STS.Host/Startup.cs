using DL.STS.Data.ConfigurationStore.EFCore.Extensions;
using DL.STS.Data.Identity.Entities;
using DL.STS.Data.Identity.Extensions;
using DL.STS.Data.OperationalStore.EFCore.Extensions;
using DL.STS.Host.App.Account.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Cryptography.X509Certificates;

namespace DL.STS.Host
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            string dbConnectionString = _configuration.GetConnectionString("appDbConnection");

            services.AddIdentityService(dbConnectionString);

            services
                .AddRouting(options => options.LowercaseUrls = true)
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // Read thumbprint from appsettings
            string thumbPrint = _configuration.GetValue<string>("ThumbPrint");
            services
                .AddIdentityServer()
                .AddEFConfigurationStore(dbConnectionString)
                .AddEFOperationalStore(dbConnectionString)
                .AddAspNetIdentity<AppUser>()
                .AddSigningCredential(LoadCertificate(thumbPrint));

            // Register MediatR
            services
                .AddAccountMediatR();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                // TODO: setup exception handler
                //app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseIdentityServer();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=dashboard}/{action=index}/{id?}");
            });
        }

        private X509Certificate2 LoadCertificate(string thumbPrint)
        {
            using (var store = new X509Store(StoreName.My, StoreLocation.LocalMachine))
            {
                store.Open(OpenFlags.ReadOnly);
                var certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, thumbPrint, validOnly: false);
                if (certCollection.Count == 0)
                {
                    throw new Exception("No certificate found containing the specified thumbprint.");
                }

                return certCollection[0];
            }
        }
    }
}
