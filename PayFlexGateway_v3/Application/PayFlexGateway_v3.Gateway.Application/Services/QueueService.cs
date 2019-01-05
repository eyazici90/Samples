using MediatR;
using PayFlexGateway_v3.Gateway.Application.Contracts;
using PayFlexGateway_v3.Gateway.Shared.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlexGateway_v3.Gateway.Application.Services
{
    public class QueueService : IQueueService
    {
        private readonly IMediator _mediatr;
        public QueueService(IMediator mediatr)
        {
            _mediatr = mediatr ?? throw new ArgumentNullException(nameof(mediatr));
        }

        public async Task<bool> PublishCommandThroughEventBus(PublishToQueueThroughEventBusCommand command) =>
            await _mediatr.Send(command);
        

    }
}
