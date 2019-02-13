using DL.STS.Data.ConfigurationStore.EFCore.Extensions;
using DL.STS.Data.Identity.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services
                .AddIdentityServer()
                .AddEFConfigurationStore(dbConnectionString);
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=dashboard}/{action=index}/{id?}");
            });
        }
    }
}
