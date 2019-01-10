using Autofac;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayFlex.Identity.Application.AutoFacModules;
using PayFlex.Identity.Application.DomainEventHandlers;
using PayFlex.Identity.Application.Services;
using PayFlex.Identity.Application.Validations;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.EFRepositories.UserAggregate;
using PayFlex.Identity.Infrastructure;
using PayFlex.Identity.TestBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Application.Tests
{
    public class PayFlexIdentityTestApplication : ApplicationTestBase
    {
        public PayFlexIdentityTestApplication()
        {
            var services = new ServiceCollection();
            
            services
                .AddOptions()
                .AddLogging();

            Build(builder =>
            {
                builder.UseGalaxyCore(typeof(ApplicationModule).Assembly, b =>
                              {
                                  b.UseConventionalCustomRepositories(typeof(UserRepository).Assembly);
                                  b.UseConventionalDomainEventHandlers(typeof(RoleAssignedToUserDomainEventHandler).Assembly);
                                
                                  b.RegisterAssemblyTypes(typeof(UserAppService).Assembly)
                                        .AssignableTo<IApplicationService>()
                                        .AsImplementedInterfaces()
                                        .EnableInterfaceInterceptors()
                                        .InterceptedBy(typeof(ValidatorInterceptor)) 
                                        .InstancePerLifetimeScope();
                              })
                        .UseGalaxyEntityFrameworkCore<PayFlexIdentityContext>(conf =>
                        {
                            conf.UseInMemoryDatabase(databaseName: "test_database");
                        })
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

            }).Initialize(services);
        }
    }
}
