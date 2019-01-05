using System;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using Galaxy.Application;
using Galaxy.Bootstrapping;
using Galaxy.Cache.Bootstrapper;
using Galaxy.EntityFrameworkCore.Bootstrapper;
using Galaxy.FluentValidation;
using Galaxy.FluentValidation.Bootstrapper;
using Galaxy.Identity.Bootstrapper;
using Galaxy.Mapster.Bootstrapper;
using Galaxy.NewtonSoftJson.Bootstrapper;
using Galaxy.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayFlex.Identity.Application.DomainEventHandlers;
using PayFlex.Identity.Application.Services;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.EFRepositories.UserAggregate;
using PayFlex.Identity.Infrastructure;
using Swashbuckle.AspNetCore.Swagger;

namespace PayFlex.Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
         
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<PayFlexIdentityContext>(options =>
                     options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Identity API", Version = "v1" });
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddOptions();

            services.AddMvc()
             .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             })
            .AddControllersAsServices();

            var container = this.ConfigureGalaxy(services);

            return new AutofacServiceProvider(container);
        }
         
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
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity API V1");
             });

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });
        }

        private IContainer ConfigureGalaxy(IServiceCollection services)
        {
            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore(b =>
                     {
                         b.UseConventionalCustomRepositories(typeof(UserRepository).Assembly);
                         b.UseConventionalDomainEventHandlers(typeof(RoleAssignedToUserDomainEventHandler).Assembly);

                         b.RegisterAssemblyTypes(typeof(UserAppService).Assembly)
                              .AssignableTo<IApplicationService>()
                              .AsImplementedInterfaces()
                              .EnableInterfaceInterceptors()
                              .InterceptedBy(typeof(ValidatorInterceptor))
                              .InterceptedBy(typeof(UnitOfWorkInterceptor))
                              .InstancePerLifetimeScope();
                     })
                     .UseGalaxyEntityFrameworkCore<PayFlexIdentityContext>(conf =>
                     {
                         conf.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
                     })
                     .UseGalaxyNewtonSoftJsonSerialization()
                     .UseGalaxyMapster()
                     .UseGalaxyFluentValidation()
                     .UseGalaxyInMemoryCache(services)
                     .UseGalaxyIdentity<PayFlexIdentityContext, User, Role, int>(services, opt =>
                     {
                         opt.Lockout = new LockoutOptions()
                         {
                             AllowedForNewUsers = true,
                             DefaultLockoutTimeSpan = TimeSpan.FromDays(30),
                             MaxFailedAccessAttempts = 3
                         };
                         opt.Password.RequireDigit = false;
                         opt.Password.RequiredLength = 6;
                         opt.Password.RequireNonAlphanumeric = false;
                         opt.Password.RequireUppercase = false;
                         opt.Password.RequireLowercase = false;
                     });

            containerBuilder.Populate(services);

            return containerBuilder.InitializeGalaxy();

        }
    }
}
