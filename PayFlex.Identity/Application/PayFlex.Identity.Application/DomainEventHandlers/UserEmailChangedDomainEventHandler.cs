using MediatR;
using PayFlex.Identity.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.DomainEventHandlers
{
    public class UserEmailChangedDomainEventHandler : INotificationHandler<UserEmailChangedDomainEvent>
    {
        public async Task Handle(UserEmailChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            //UserEmail Changed post commit action !!!
        }
    }
}
