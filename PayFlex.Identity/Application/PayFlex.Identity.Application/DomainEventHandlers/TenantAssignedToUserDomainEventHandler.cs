using MediatR;
using PayFlex.Identity.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.DomainEventHandlers
{
    public class TenantAssignedToUserDomainEventHandler : INotificationHandler<TenantAssignedToUserDomainEvent>
    {

        public async Task Handle(TenantAssignedToUserDomainEvent notification, CancellationToken cancellationToken)
        {
             
        }
    }
}
