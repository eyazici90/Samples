using FluentAssertions;
using PayFlex.Identity.Application.DomainEventHandlers;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.Domain.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.EventHandlers
{
    public class EventHandler_Tests : PayFlexIdentityTestApplication
    {
        
        [Fact]
        public async Task Permission_assigned_to_user_domain_event_handle_after_triggered_success()
        {
            var handler = TheObject<PermissionAssignedToUserDomainEventHandler>();

            var fakeTenantId = 1;

            var fakeUser = User.Create("emre.yazici", fakeTenantId);

            var fakePermissionAssignedToUserDomainEvent = new PermissionAssignedToUserDomainEvent(fakeUser);

            Func<Task> act = async () => await handler.Handle(fakePermissionAssignedToUserDomainEvent, CancellationToken.None);

            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task Role_assigned_to_user_domain_event_handle_after_triggered_success()
        {
            var handler = TheObject<RoleAssignedToUserDomainEventHandler>();

            var fakeTenantId = 1;

            var fakeUser = User.Create("emre.yazici", fakeTenantId);

            var fakeRoleAssignedToUserDomainEvent = new RoleAssignedToUserDomainEvent(fakeUser);

            Func<Task> act = async () => await handler.Handle(fakeRoleAssignedToUserDomainEvent, CancellationToken.None);

            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task Tenant_assigned_to_user_domain_event_handle_after_triggered_success()
        {
            var handler = TheObject<TenantAssignedToUserDomainEventHandler>();

            var fakeTenantId = 1;

            var fakeUser = User.Create("emre.yazici", fakeTenantId);

            var fakeTenantAssignedToUserDomainEvent = new TenantAssignedToUserDomainEvent(fakeUser);

            Func<Task> act = async () => await handler.Handle(fakeTenantAssignedToUserDomainEvent, CancellationToken.None);

            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task User_email_changed_domain_event_handle_after_triggered_success()
        {
            var handler = TheObject<UserEmailChangedDomainEventHandler>();

            var fakeTenantId = 1;

            var fakeUser = User.Create("emre.yazici", fakeTenantId);

            var fakeUserEmailChangedDomainEvent = new UserEmailChangedDomainEvent(fakeUser);

            Func<Task> act = async () => await handler.Handle(fakeUserEmailChangedDomainEvent, CancellationToken.None);

            await act.Should().NotThrowAsync<Exception>();
        }

        [Fact]
        public async Task User_name_changed_domain_event_handle_after_triggered_success()
        {
            var handler = TheObject<UserNameChangedDomainEventHandler>();

            var fakeTenantId = 1;

            var fakeUser = User.Create("emre.yazici", fakeTenantId);

            var fakeUserNameChangedDomainEvent = new UserNameChangedDomainEvent(fakeUser);

            Func<Task> act = async () => await handler.Handle(fakeUserNameChangedDomainEvent, CancellationToken.None);

            await act.Should().NotThrowAsync<Exception>();
        }

    }
}
