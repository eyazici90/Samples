using MediatR;
using PayFlex.Identity.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.DomainEventHandlers
{ 
    public class UserNameChangedDomainEventHandler : INotificationHandler<UserNameChangedDomainEvent>
    {
        public async Task Handle(UserNameChangedDomainEvent notification, CancellationToken cancellationToken)
        {
            //User Name Changed post commit action !!!
        }
    }
}
