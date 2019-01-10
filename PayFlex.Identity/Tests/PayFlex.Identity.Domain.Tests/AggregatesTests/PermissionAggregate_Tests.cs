using FluentAssertions;
using PayFlex.Identity.Domain.AggregatesModel.PermissionAggregate;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PayFlex.Identity.Domain.Tests.AggregatesTests
{
    public class PermissionAggregate_Tests
    {

        [Fact]
        public void Created_new_permission_aggregateroot_is_transient()
        {
            var newPermission = Permission.Create(fakePermissionName);

            newPermission.IsTransient().Should().BeTrue();
        }


        [Fact]
        public void Create_new_permission_aggregateroot_fail()
        {
            var fakeName = string.Empty;

            Action act = () => Permission.Create(fakeName);

            act.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public void Create_new_permission_aggregateroot_success()
        {
            var fakeName = "admin";

            var createdNewPermission = Permission.Create(fakeName);

            createdNewPermission.Should().NotBeNull();
        }


        [Fact]
        public void Change_existing_permission_name_success()
        {
            var newPermissionName = "test-11";

            var permission = fakePermission;

            permission.ChangeName(newPermissionName);

            permission.Name.Should().Be(newPermissionName);
        }

        [Fact]
        public void Change_existing_permission_name_fail()
        {
            var newPermissionName = string.Empty;

            Action act = () => fakePermission.ChangeName(newPermissionName);

            act.Should().Throw<IdentityDomainException>();
        }
         

        private Permission fakePermission => Permission.Create(fakePermissionName);

        private string fakePermissionName = "admin"; 

        private int fakeTenantId = 1;

        private int fakeUserId = 1;
    }
}
