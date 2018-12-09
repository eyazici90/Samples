using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{
    public class QueueMiddleware
    {
        private readonly RequestDelegate _next;

        public QueueMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);
        }
    }
}
