using MediatR;
using PayFlexGateway_v3.Gateway.Commands;
using PayFlexGateway_v3.Gateway.Commands.QueryCommands;
using PayFlexGateway_v3.Gateway.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Services
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

        public async Task<IList<object>> GetAllLogs() =>
            await _mediatr.Send(new GetAllLogsQuery());

        public async Task<object> GetLogById(string id) =>
            await _mediatr.Send(new GetLogByIdQuery(id));
    }
}
