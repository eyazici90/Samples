using FluentAssertions;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.Domain.Events;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PayFlex.Identity.Domain.Tests.AggregatesTests
{
    public class UserAggregate_Tests
    {

        [Fact]
        public void Created_new_user_aggregateroot_is_transient()
        {
            var newUser = User.Create(fakeUserName, fakeTenantId);

            newUser.IsTransient().Should().BeTrue();
        }


        [Fact]
        public void Create_new_user_aggregateroot_fail()
        {
            var fakeName = string.Empty; 

            Action act = () => User.Create(fakeName, fakeTenantId);

            act.Should().Throw<ArgumentNullException>();
           
        }

        [Fact]
        public void Create_new_user_aggregateroot_success()
        {
            var fakeName = "emre.yazici ";

            var createdNewUser = User.Create(fakeName, fakeTenantId);

            createdNewUser.Should().NotBeNull();
        }

        [Fact]
        public void Assign_role_to_user_fail()
        {
            var fakeName = "emre.yazici ";
            var fakeRole_Id = -1;

            var createdNewUser = User.Create(fakeName, fakeTenantId);

            Action act = () => createdNewUser.AssignRole(fakeRole_Id);

            act.Should().Throw<IdentityDomainException>();

        }

        [Fact]
        public void Assign_role_to_user_success()
        {
            var fakeName = "emre.yazici ";

            var createdNewUser = User.Create(fakeName, fakeTenantId);
            
            createdNewUser.AssignRole(fakeRoleId);

            createdNewUser.UserRoles.Should().HaveCount(1);

        }
        
        [Fact]
        public void Change_existing_user_userName_success()
        {
            var newUsername = "berkay.yazici";

            var user = fakeUser;

            user.ChangeUserName(newUsername);

            user.UserName.Should().Be(newUsername);
        }

        [Fact]
        public void Change_existing_user_userName_fail()
        {
            var newUsername = string.Empty;

            Action act = () => fakeUser.ChangeUserName(newUsername);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Change_existing_user_email_success()
        {
            var newEmail = "emre.yazici@innova.com.tr";

            var user = fakeUser;

            user.ChangeEmail(newEmail);

            user.Email.Should().Be(newEmail);
        }


        [Fact]
        public void Change_existing_user_userName_trigger_userNameChangedDomainEvent_success()
        {
            var newUsername = "berkay.yazici";

            var user = fakeUser;

            user.ChangeUserName(newUsername);

            user.DomainEvents.Should().ContainItemsAssignableTo<UserNameChangedDomainEvent>();
        }

        [Fact]
        public void Change_existing_user_email_trigger_userEmailChangedDomainEvent_success()
        {
            var newEmail = "emre.yazici@innova.com.tr";

            var user = fakeUser;

            user.ChangeEmail(newEmail);

            user.DomainEvents.Should().ContainItemsAssignableTo<UserEmailChangedDomainEvent>();
        }

        [Fact]
        public void Assign_role_to_user_trigger_roleAssignedToUserDomainEvent_success()
        {
            var user = fakeUser;

            user.AssignRole(fakeRoleId);

            user.DomainEvents.Should().ContainItemsAssignableTo<RoleAssignedToUserDomainEvent>();
        }

        [Fact]
        public void Assign_permission_to_user_trigger_permissionAssignedToUserDomainEvent_success()
        {
            var user = fakeUser;

            user.AssignPermission(fakeRoleId);

            user.DomainEvents.Should().ContainItemsAssignableTo<PermissionAssignedToUserDomainEvent>();
        }

        [Fact]
        public void Assign_tenant_to_user_trigger_tenantAssignedToUserDomainEvent_success()
        {
            var user = fakeUser;

            user.AssignPermission(fakeRoleId);

            user.DomainEvents.Any().Should().BeTrue();
        }



        private User fakeUser => User.Create(fakeUserName,fakeTenantId);

        private string fakeUserName = "emre.yazici"; 

        private int fakeTenantId = 1;

        private int fakeRoleId = 1;
         
    }
}
