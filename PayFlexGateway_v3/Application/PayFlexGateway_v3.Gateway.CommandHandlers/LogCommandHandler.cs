using Galaxy.Log;
using MediatR;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.CommandHandlers
{
    public class LogCommandHandler : IRequestHandler<LogRequestCommand, bool>
         , IRequestHandler<LogResponseCommand, bool>
    {
        private readonly ILog _log;
        public LogCommandHandler(ILog log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<bool> Handle(LogRequestCommand request, CancellationToken cancellationToken)
        {
            _log.Information(request.Body);
            return true;
        }

        public async Task<bool> Handle(LogResponseCommand request, CancellationToken cancellationToken)
        {
            _log.Information(request.Body);
            return true;
        }
    }
}
