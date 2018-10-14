﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using CustomerSample.Application;
using CustomerSample.Application.Abstractions;
using CustomerSample.Application.Validators;
using CustomerSample.Customer.Domain.AggregatesModel.BrandAggregate;
using CustomerSample.Customer.Domain.EFRepositories.BrandAggregate;
using CustomerSample.Infrastructure;
using Galaxy.Bootstrapping;
using Galaxy.EntityFrameworkCore.Bootstrapper;
using Galaxy.FluentValidation;
using Galaxy.FluentValidation.Bootstrapper;
using Galaxy.Mapster.Bootstrapper;
using Galaxy.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace CustomerSample.API.Host
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CustomerSampleDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Customer API", Version = "v1" });
            });
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();
            services.AddMvc()
            .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
            .AddControllersAsServices(); ;
           

            var container = ConfigureAutofacModules(services);

            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Customer API V1");
             });

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });
        }

        private IContainer ConfigureAutofacModules(IServiceCollection services)
        {

            var containerBuilder = GalaxyMainBootsrapper.Create()
                 .RegisterContainerBuilder()
                     .UseGalaxyCore(builder =>
                     {
                         builder.RegisterType<CustomerAppService>()
                              .As<ICustomerAppService>()
                               .AsImplementedInterfaces()
                               .EnableInterfaceInterceptors()
                               .InterceptedBy(typeof(ValidatorInterceptor))
                               .InterceptedBy(typeof(UnitOfWorkInterceptor))
                              .InstancePerLifetimeScope();
                     })
                     .UseConventinalCustomRepositories(typeof(BrandRepository).Assembly)
                     .UseConventinalPolicies(typeof(BrandPolicy).Assembly)
                     .UseConventinalDomainService(typeof(Brand).Assembly)
                     .UseConventinalApplicationService(typeof(CustomerAppService).Assembly)
                     .UseGalaxyEntityFrameworkCore(
                                new DbContextOptionsBuilder<CustomerSampleDbContext>()
                                     .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")))
                     .UseGalaxyMapster()
                     .UseGalaxyFluentValidation(typeof(BrandValidation).Assembly);

            containerBuilder.Populate(services);

           return containerBuilder.InitializeGalaxy();

        }
    }
}
