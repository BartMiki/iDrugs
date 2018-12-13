using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DAL.Interfaces;
using DAL.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using WebApp.Validators;
using static WebApp.AutoMapperProfiels;
using DAL;

namespace WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IContainer ApplicationContainer { get; private set; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ApothecaryViewModelValidator>());

            //Autofac
            var builder = new ContainerBuilder();

            builder.RegisterType<iDrugsEntities>().AsSelf().InstancePerDependency();
            builder.RegisterType<ApothecaryEfRepo>().As<IApothecaryRepo>().InstancePerDependency();
            builder.RegisterType<MedicineEfRepo>().As<IMedicineRepo>().InstancePerDependency();
            builder.RegisterType<OrderEfRepo>().As<IOrderRepo>().InstancePerDependency();
            builder.RegisterType<WarehouseEfRepo>().As<IWarehouseRepo>().InstancePerDependency();
            builder.RegisterType<DrugStoreStockEfRepo>().As<IDrugStoreStockRepo>().InstancePerDependency();
            builder.Populate(services);
            ApplicationContainer = builder.Build();

            CreateMaps();

            return new AutofacServiceProvider(ApplicationContainer);
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
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
