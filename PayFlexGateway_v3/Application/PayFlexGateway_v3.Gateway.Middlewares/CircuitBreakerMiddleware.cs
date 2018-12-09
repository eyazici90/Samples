using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{ 
    public class CircuitBreakerMiddleware
    {
        private readonly RequestDelegate _next;

        public CircuitBreakerMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}
