using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using PayFlexGateway_v3.Gateway.Application.Contracts;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{ 
    public class CircuitBreakerMiddleware
    {
        private readonly IResilenceService _resilenceService;
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public CircuitBreakerMiddleware(RequestDelegate next
            , IResilenceService resilenceService
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _resilenceService = resilenceService ?? throw new ArgumentNullException(nameof(resilenceService));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer)) ;
        }

        public async Task Invoke(HttpContext context)
        {
            if (this._resilenceService.CheckIfBreakerStateOpened())
            {
                context.Response.OnStarting(async () => {

                    context.Response.Clear();
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";

                    var errorResult = this._serializer.Serialize(
                            new { Message = $"CircuitBreaker Is Opened !!! LastHandled Exception by CircuitBreaker is: {_resilenceService.LastHandledExceptionByCircuitBreaker.Message}"}
                            );

                    await context.Response.WriteAsync(errorResult);
                });
                return;
            }
            var command = new ExecuteWithCircuitBreakerCommand();

            command.Execution = async () => await _next(context);

            await this._resilenceService
                .ExecuteWithCircuitBreakerAsync( command);
        }
    }
}
