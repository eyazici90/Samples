using Galaxy.Cache;
using Galaxy.Serialization;
using MediatR;
using PayFlex.Identity.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.DomainEventHandlers
{
    public class PermissionAssignedToUserDomainEventHandler : INotificationHandler<PermissionAssignedToUserDomainEvent>
    {
        private readonly ICache _cache;
        private readonly ISerializer _serializer;
        public PermissionAssignedToUserDomainEventHandler(ICache cache
            , ISerializer serializer)
        {
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _serializer = serializer ?? throw new ArgumentNullException(nameof(serializer));
        }
        public async Task Handle(PermissionAssignedToUserDomainEvent notification, CancellationToken cancellationToken)
        {
            var serializedJsonData = this._serializer.Serialize(notification.User);

            await _cache.SetAsync($"{nameof(notification.User)}-{notification.User.Id.ToString()}", serializedJsonData);
        }
    }
}
