using MediatR;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.Events
{
    public class UserNameChangedDomainEvent: INotification
    {
        public User User { get; private set; }

        public UserNameChangedDomainEvent(User user)
        {
            this.User = user;
        }
    }
}
