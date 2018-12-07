using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{
    
    public class HealthCheckMiddleware
    {
        private const string HEALTHCHECK_URL = "/healthcheck";
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public HealthCheckMiddleware(RequestDelegate next
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            if (!context.Request.Path.Equals(HEALTHCHECK_URL, StringComparison.Ordinal))
            {
                await _next(context);
                return;
            }

            await context.Response.WriteAsync(this._serializer.Serialize(new { Status = "Healty" }));
         
        }
    }
}
