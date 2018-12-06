using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Galaxy.Bootstrapping;
using Galaxy.Cache;
using Galaxy.Serialization;
using Galaxy.Serilog.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using PayFlexGateway_v3.Gateway.Middlewares;
using PayFlexGateway_v3.Gateway.Persistance.CommandHandlers;
using Serilog;

namespace PayFlexGateway_v3.Gateway.API
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = new ConfigurationBuilder()
                        .AddJsonFile(Path.Combine(string.Empty, "appsettings.json"), optional: true, reloadOnChange: true)
                        .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
               .AddControllersAsServices();

            services.AddOcelot(Configuration);

            var container = this.ConfigureGalaxy(services);
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
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            ConfigureMiddlewares(app);
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });

            app.UseOcelot(conf =>
            {
                conf.PreQueryStringBuilderMiddleware = async (ctx, next) =>
                {
                    await next.Invoke();
                };
                conf.PreErrorResponderMiddleware = async (ctx, next) =>
                {
                    await next.Invoke();
                };
            })
           .ConfigureAwait(false)
           .GetAwaiter().GetResult();
        }

        private IContainer ConfigureGalaxy(IServiceCollection services)
        {
            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore(b=>
                     {
                         b.UseConventionalCommandHandlers(typeof(LogRequestCommandHandler).Assembly); 
                     })
                     .UseGalaxySerilogger(configs => {
                         configs.WriteTo.File($"log.txt",
                            rollingInterval: RollingInterval.Day,
                            rollOnFileSizeLimit: true);
                     });

            containerBuilder.Populate(services);

            return containerBuilder.InitializeGalaxy();
        }

        private void ConfigureMiddlewares(IApplicationBuilder app)
        {
            app.UseMiddleware<IdempotencyMiddleware>();
            app.UseMiddleware<LogMiddleware>();
            app.UseMiddleware<CorrelationIdMiddleware>();
        }
    }
}
