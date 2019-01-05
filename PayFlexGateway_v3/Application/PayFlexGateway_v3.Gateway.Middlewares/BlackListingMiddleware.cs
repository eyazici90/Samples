using Galaxy.Serialization;
using Microsoft.AspNetCore.Http;
using PayFlexGateway_v3.Gateway.Application.Contracts;
using PayFlexGateway_v3.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{ 
    public class BlackListingMiddleware
    {
        private readonly IBlackListService _blackListService;
        private readonly ISerializer _serializer;
        private readonly RequestDelegate _next;

        public BlackListingMiddleware(RequestDelegate next
            , IBlackListService blackListService
            , ISerializer serializer)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _blackListService = blackListService ?? throw new ArgumentNullException(nameof(blackListService));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }

        public async Task Invoke(HttpContext context)
        {
            var host = context.Request.HttpContext?.Connection?.RemoteIpAddress?.ToString();

            var isIpInblackList = await _blackListService.IsIpInBlackList(new BlackListByIpQuery(host));

            if (isIpInblackList)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync( this._serializer.Serialize(new
                {
                    Result = $"IP : {host} is in BlackList!"
                }));
            }
            else
                await _next(context);
        }
    }
}
