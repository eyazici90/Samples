using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PayFlex.Identity.API.Attributes;
using PayFlex.Identity.API.Extensions;
using PayFlex.Identity.API.Filters; 
using PayFlex.Identity.API.Session;
using PayFlex.Identity.Application.AutoFacModules;
using PayFlex.Identity.Application.DomainEventHandlers;
using PayFlex.Identity.Application.Services;
using PayFlex.Identity.Application.Validations;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.Infrastructure;
using PayFlex.Identity.Persistance.EF.UserAggregate;
using PayFlex.Identity.Shared;
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
        
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddOptions();

            ConfigureSwagger(services);
            ConfigureAuthorization(services);

            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
                options.Filters.Add(typeof(HttpGlobalResponseFilter));
            })
            .AddJsonOptions(options =>
             {
                 options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                 options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
             })
            .AddControllersAsServices();

            var container = this.ConfigureGalaxy(services);

            return new AutofacServiceProvider(container);
        }
         
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime lifetime)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
            });

            app.UseAuthentication();

            app.UseSwagger()
             .UseSwaggerUI(c =>
             {
                 c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityApi v1");
             });
            

            app.UseMvc(routes =>
            {

                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });

            lifetime.ApplicationStarted.Register(() =>
            {
                
            });

            lifetime.ApplicationStopped.Register(() =>
            {
                 
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "IdentityApi", Version = "v1" });
                c.OperationFilter<AuthorizationHeaderParameterOperationFilter>();
            });
        }

        private void ConfigureAuthorization(IServiceCollection services)
        {

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(opt =>
                    {
                        opt.TokenValidationParameters =
                             new TokenValidationParameters
                             {
                                 ValidateIssuer = false,
                                 ValidateAudience = false,
                                 ValidateLifetime = true,
                                 ValidateIssuerSigningKey = true,
                                 ValidIssuer = string.Empty,
                                 ValidAudience = "",
                                 IssuerSigningKey = SecurityKeyExtension.GetSigningKey(Settings.API_SECRET)
                             };
                    });

        }

        private IContainer ConfigureGalaxy(IServiceCollection services)
        {
            var containerBuilder = GalaxyCoreModule.New
                 .RegisterGalaxyContainerBuilder()
                     .UseGalaxyCore(typeof(ApplicationModule).Assembly, b =>
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
                         
                     }, typeof(PayFlexIdentityAppSession))
                     .UseGalaxyNewtonSoftJsonSerialization()
                     .UseGalaxyMapster()
                     .UseGalaxyFluentValidation(typeof(UserDtoValidation).Assembly)
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
