using Galaxy.Log;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using PayFlexGateway_v3.Gateway.Persistance.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Middlewares
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILog _log;
        private readonly IMediator _mediatr;
        public LogMiddleware(RequestDelegate next
            , ILog log
            , IMediator mediatr)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }


        public async Task Invoke(HttpContext context)
        {
            // _log.Information(await FormatRequest(context.Request));
            await _mediatr.Send(new LogRequestCommand {
                Body = await FormatRequest(context.Request)
            });

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                await _next(context);

                _log.Information(await FormatResponse(context.Response));
                await responseBody.CopyToAsync(originalBodyStream);
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;
            request.EnableRewind();

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return $"Response {text}";
        }
    }
}
