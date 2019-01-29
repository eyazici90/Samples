using FluentAssertions;
using PayFlex.Identity.Domain.AggregatesModel.PermissionAggregate;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PayFlex.Identity.Domain.Tests.AggregatesTests
{
    public class PermissionAggregateTests
    {

        [Fact]
        public void Created_new_permission_aggregateroot_is_transient()
        {
            var newPermission = Permission.Create(fakePermissionName, fakeServiceName, fakeMethodExecutionName);

            newPermission.IsTransient().Should().BeTrue();
        }


        [Fact]
        public void Create_new_permission_aggregateroot_fail()
        {
            var fakeName = string.Empty;

            Action act = () => Permission.Create(fakeName, fakeServiceName, fakeMethodExecutionName);

            act.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public void Create_new_permission_aggregateroot_success()
        {
            var fakeName = "admin";

            var createdNewPermission = Permission.Create(fakePermissionName, fakeServiceName, fakeMethodExecutionName);

            createdNewPermission.Should().NotBeNull();
        }

        [Fact]
        public void Delete_existing_permission_aggregateroot_success()
        {
            var fakeName = "admin";

            var createdNewPermission = Permission.Create(fakePermissionName, fakeServiceName, fakeMethodExecutionName);

            createdNewPermission.Should().NotBeNull();

            createdNewPermission.DeleteThisPermission();

            createdNewPermission.IsDeleted.Should().BeTrue();
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

        [Fact]
        public void Change_existing_permission_description_success()
        {
            var newFakePermissionDesc = "fasdadasdqwds";

            var permission = fakePermission;

            permission.ChangeOrSetDesciption(newFakePermissionDesc);

            permission.Description.Should().Be(newFakePermissionDesc);
        }


        private Permission fakePermission => Permission.Create(fakePermissionName, fakeServiceName, fakeMethodExecutionName);

        private string fakePermissionName = "admin";

        private string fakeServiceName = "testService";

        private string fakeMethodExecutionName = "testMethod";

        private int fakeTenantId = 1;

        private int fakeUserId = 1;
    }
}
