using FluentAssertions;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PayFlex.Identity.Domain.Tests.AggregatesTests
{
    public class RoleAggregate_Tests
    {
        [Fact]
        public void Created_new_role_aggregateroot_is_transient()
        {
            var newRole = Role.Create(fakeRoleName);

            newRole.IsTransient().Should().BeTrue();
        }


        [Fact]
        public void Create_new_role_aggregateroot_fail()
        {
            var fakeName = string.Empty;

            Action act = () => Role.Create(fakeName);

            act.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public void Create_new_role_aggregateroot_success()
        {
            var fakeName = "default";

            var createdNewRole = Role.Create(fakeName);

            createdNewRole.Should().NotBeNull();
        }

        [Fact]
        public void Delete_existing_role_aggregateroot_success()
        {
            var fakeName = "default";

            var createdNewRole = Role.Create(fakeName);

            createdNewRole.DeleteThisRole();

            createdNewRole.IsDeleted.Should().BeTrue();
        }


        [Fact]
        public void Change_existing_role_name_success()
        {
            var newRoleName = "test-11";

            var role = fakeRole;

            role.ChangeName(newRoleName);

            role.Name.Should().Be(newRoleName);
        }

        [Fact]
        public void Change_existing_tenant_name_fail()
        {
            var newRoleName = string.Empty;

            Action act = () => fakeRole.ChangeName(newRoleName);

            act.Should().Throw<IdentityDomainException>();
        }


        private Role fakeRole=> Role.Create(fakeRoleName);

        private string fakeRoleName = "admin";

        private int fakeTenantId = 1;

        private int fakeUserId = 1;
    }
}
