using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using PayFlex.Identity.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PayFlex.Identity.API.Filters
{ 
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private readonly IHostingEnvironment _env;

        public HttpGlobalExceptionFilter(IHostingEnvironment env)
        {
            this._env = env ?? throw new ArgumentNullException(nameof(env));
        }

        public void OnException(ExceptionContext context)
        {
            PerformErrorResult(context);
        }

        private void PerformErrorResult(ExceptionContext context)
        {
            var errorResponse = new HttpApiResultResponse
            {
                Error = true,
                Result = context.Exception.Message + "\r\n" + context.Exception.StackTrace
            };

            context.Result = new OkObjectResult(errorResponse);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            context.ExceptionHandled = true;
        }
    }
}
