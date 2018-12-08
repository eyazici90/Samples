using Galaxy.Log;
using MediatR;
using PayFlexGateway_v3.Gateway.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Persistances.CommandHandlers
{
    public class LogResponseCommandHandler : IRequestHandler<LogResponseCommand, bool>
    {
        private readonly ILog _log;
        public LogResponseCommandHandler(ILog log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public async Task<bool> Handle(LogResponseCommand request, CancellationToken cancellationToken)
        {
            _log.Information(request.Body);
            return await Task.FromResult(true);
        }
    }
}
