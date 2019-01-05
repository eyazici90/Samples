using Microsoft.AspNetCore.Http;
using PayFlexGateway_v3.Gateway.Shared;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = Guid.NewGuid().ToString();

            context.Response.Headers.Add(Settings.PGW_CORRELATION_ID , correlationId);

            await _next(context);
        }
    }
}
