using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{ 
    public class HttpGlobalExceptionMiddleware
    {
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public HttpGlobalExceptionMiddleware(RequestDelegate next
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await context.Response.WriteAsync(this._serializer.Serialize(
                    new { ErrorMsj = ex.Message, DetailMessage = ex.InnerException?.Message }
                    ));
            }
          
        }
    }
}
