using System.Linq;
using IntegratedIocContainerApi.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services;

namespace IntegratedIocContainerApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services = RegisterDiInterfacesWithScrutor(services);

            services = ConfigureSettings(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private IServiceCollection RegisterDiInterfacesWithScrutor(IServiceCollection services)
        {
            //Jose: Use of Scrutor to not having to add every single interface
            services.Scan(scan => scan
                .FromAssemblyOf<IAssemblyMarkerServices>().AddClasses().AsImplementedInterfaces().WithTransientLifetime()
                .FromAssemblyOf<IAssemblyMarkerApi>().AddClasses().AsImplementedInterfaces().WithTransientLifetime()
            );

            return services;
        }

        private IServiceCollection ConfigureSettings(IServiceCollection services)
        {
            services.Configure<OperationsSettings>(Configuration.GetSection("OperationsSettings"));

            return services;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
