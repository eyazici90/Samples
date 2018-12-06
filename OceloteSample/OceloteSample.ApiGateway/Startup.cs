using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using OceloteSample.ApiGateway.Middlewares;

namespace OceloteSample.ApiGateway
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
            services.AddCors();

            services.AddMvc()
            .AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            })
           .AddControllersAsServices();

            services.AddOcelot(Configuration);
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
            
            app.UseMiddleware<CorrelationIdMiddleware>();


            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action}/{id?}");
            });

                conf.PreQueryStringBuilderMiddleware = async (ctx, next) =>
                {
                    Console.WriteLine($"{DateTime.Now} : Requesting URL is : {ctx.HttpContext.Request.Path.ToString()}");
                    await next.Invoke();
                };
                conf.PreErrorResponderMiddleware = async (ctx, next) =>
                {
                    await next.Invoke();
                    Console.WriteLine($"{DateTime.Now} : ErrorMsj : {string.Join(",", ctx.Errors.Select(e => e.Message)) }");
                };
            })
            .ConfigureAwait(false)
            .GetAwaiter()
            .GetResult();
           
        }
    }
}
