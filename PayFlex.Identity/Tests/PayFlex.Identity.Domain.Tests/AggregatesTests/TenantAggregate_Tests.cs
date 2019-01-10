using FluentAssertions;
using PayFlex.Identity.Domain.AggregatesModel.TenantAggregate;
using PayFlex.Identity.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace PayFlex.Identity.Domain.Tests.AggregatesTests
{
    public class TenantAggregate_Tests
    {

        [Fact]
        public void Created_new_tenant_aggregateroot_is_transient()
        {
            var newTenant = Tenant.Create(fakeTenantName);

            newTenant.IsTransient().Should().BeTrue();
        }


        [Fact]
        public void Create_new_tenant_aggregateroot_fail()
        {
            var fakeName = string.Empty;

            Action act = () => Tenant.Create(fakeName);

            act.Should().Throw<ArgumentNullException>();

        }

        [Fact]
        public void Create_new_tenant_aggregateroot_success()
        {
            var fakeName = "default";

            var createdNewTenant = Tenant.Create(fakeName);

            createdNewTenant.Should().NotBeNull();
        }


        [Fact]
        public void Change_existing_tenant_name_success()
        {
            var newTenantName = "test-11";

            var tenant = fakeTenant;

            tenant.ChangeName(newTenantName);

            tenant.Name.Should().Be(newTenantName);
        }

        [Fact]
        public void Change_existing_tenant_name_fail()
        {
            var newTenantName = string.Empty;

            Action act = () => fakeTenant.ChangeName(newTenantName);

            act.Should().Throw<ArgumentNullException>();
        }


        private Tenant fakeTenant => Tenant.Create(fakeTenantName);

        private string fakeTenantName = "default";

        private int fakeTenantId = 1;

        private int fakeUserId = 1;
    }
}
