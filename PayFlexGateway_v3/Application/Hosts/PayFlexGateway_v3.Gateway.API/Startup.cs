using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Galaxy.Bootstrapping;
using Galaxy.Cache;
using Galaxy.Cache.Bootstrapper;
using Galaxy.Serilog.Bootstrapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using PayFlexGateway_v3.Gateway.Application.Services;
using PayFlexGateway_v3.Gateway.CommandHandlers;
using PayFlexGateway_v3.Gateway.Middlewares;
using PayFlexGateway_v3.Gateway.QueryHandlers;
using Serilog;

namespace PayFlexGateway_v3.Gateway.API
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        { 
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddOptions();

            services.AddOcelot(Configuration);

            var container = this.ConfigureGalaxy(services);
            return new AutofacServiceProvider(container);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {  
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            ConfigureSwagger(app);

            ConfigureMiddlewares(app);
            
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
                         b.UseConventionalCommandHandlers(typeof(LogCommandHandler).Assembly,typeof(LogQueryHandler).Assembly);
                         b.UseConventionalApplicationService(typeof(LogService).Assembly);
                     })
                     .UseGalaxyInMemoryCache(services)
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
            app.UseMiddleware<HttpGlobalExceptionMiddleware>();
            app.UseMiddleware<HealthCheckMiddleware>();
            app.UseMiddleware<IdempotencyMiddleware>();
            app.UseMiddleware<LogMiddleware>();
            app.UseMiddleware<HttpValidationMiddleware>();
            app.UseMiddleware<CorrelationIdMiddleware>();
        }

        private void ConfigureSwagger(IApplicationBuilder app)
        {
            var swaggerUrls = Configuration.GetSection("SwaggerUrls").GetChildren();

            app.UseSwaggerUI(c =>
            {
                foreach (var swaggerConf in swaggerUrls)
                {
                    c.SwaggerEndpoint(swaggerConf.Value, swaggerConf.Key);     
                }
            });
        }
    }
}
