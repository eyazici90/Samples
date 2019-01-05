using MediatR;
using PayFlexGateway_v3.Gateway.Services.Contracts;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using PayFlexGateway_v3.Gateway.Shared.Queries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Application.Services
{
    public class LogService : ILogService
    {
        private readonly IMediator _mediatr;
        public LogService(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr)) ;
        }

        public async Task<bool> LogRequest(LogRequestCommand command) =>
            await _mediatr.Send(command);

        public async Task<bool> LogResponse(LogResponseCommand command) =>
           await _mediatr.Send(command);

        public async Task<bool> LogException(LogExceptionByRequestCommand command) =>
           await _mediatr.Send(command);

        public async Task<IList<object>> GetAllLogs(GetAllLogsQuery query) =>
            await _mediatr.Send(query);

        public async Task<object> GetLogById(GetLogByIdQuery query) =>
            await _mediatr.Send(query);
    }
}
