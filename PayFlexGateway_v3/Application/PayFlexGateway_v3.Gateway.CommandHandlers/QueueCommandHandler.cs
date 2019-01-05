using Galaxy.Events;
using MediatR;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.CommandHandlers
{
    public class QueueCommandHandler : IRequestHandler<PublishToQueueThroughEventBusCommand, bool>
    {
        private readonly IEventBus _eventBus;
        public QueueCommandHandler(IEventBus eventBus)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public async Task<bool> Handle(PublishToQueueThroughEventBusCommand request, CancellationToken cancellationToken)
        {
            //ToDo gonna be changed !!!
            await _eventBus.Publish(request);
            return true;
        }
    }
}
