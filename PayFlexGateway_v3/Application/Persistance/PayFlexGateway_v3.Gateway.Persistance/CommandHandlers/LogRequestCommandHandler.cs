using Galaxy.Log;
using MediatR;
using PayFlexGateway_v3.Gateway.Persistance.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Persistance.CommandHandlers
{
    public class LogRequestCommandHandler : IRequestHandler<LogRequestCommand, bool>
    {
        private readonly ILog _log;
        public LogRequestCommandHandler(ILog log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }
        public async Task<bool> Handle(LogRequestCommand request, CancellationToken cancellationToken)
        {
            _log.Information(request.Body);
            return await Task.FromResult(true);
        }
    }
}
