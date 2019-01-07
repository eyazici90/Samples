using MediatR;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Text;

namespace PayFlex.Identity.Domain.Events
{
    public class PermissionAssignedToUserDomainEvent : INotification
    {
        public User User { get; private set; }

        public PermissionAssignedToUserDomainEvent(User user)
        {
            this.User = user;
        }
    }
}
