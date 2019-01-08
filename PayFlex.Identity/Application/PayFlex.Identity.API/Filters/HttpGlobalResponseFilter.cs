using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using PayFlex.Identity.API.Attributes;
using PayFlex.Identity.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PayFlex.Identity.API.Filters
{
    public class HttpGlobalResponseFilter :  IAsyncResultFilter
    { 
        public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
        {
            var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;

            var shouldDisableResponseFormatting = false;

            if (controllerActionDescriptor != null)
            {
                var actionAttributes = controllerActionDescriptor.MethodInfo.GetCustomAttributes(inherit: true);
                shouldDisableResponseFormatting = actionAttributes.Any(a => a is DisableResponseFormatterAttribute);
            }

            if (shouldDisableResponseFormatting)
            {
                await next();
                return;
            } 

            if ((context.Result as ObjectResult).StatusCode.HasValue 
                && (context.Result as ObjectResult).StatusCode.Value.ToString().StartsWith("2") == false )
                PerformErrorResult(context);
            
            else
                PerformSuccesfullResult(context);
            

            var executedContext = await next();
        } 

        private void PerformSuccesfullResult(ResultExecutingContext context)
        {
                var successResponse = new HttpApiResultResponse
                {
                    Success = true,
                    Result = (context.Result as ObjectResult).Value
                };

                context.Result = new OkObjectResult(successResponse);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
            
        }

        private void PerformErrorResult(ResultExecutingContext context)
        {
            var errorResponse = new HttpApiResultResponse
            {
                Error = true,
                Result = (context.Result as ObjectResult).Value
            };

            context.Result = new OkObjectResult(errorResponse);
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
        }
    }
}
