using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Tenant;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.ApplicationServices
{

    public class TenantAppService_Tests : PayFlexIdentityTestApplication
    {
        private readonly ITenanAppService _tenantAppService;
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        public TenantAppService_Tests()
        {
            _unitofWorkAsync = TheObject<IUnitOfWorkAsync>();
            _tenantAppService = TheObject<ITenanAppService>();
        }

        [Fact]
        public async Task Add_tenant_returns_created_tenant_success()
        {
            var fakeTenant = new TenantDto
            {
                Name = "Test-1 Tenant",
                Description = "aassd"
            };

            await _tenantAppService.AddTenantAsync(fakeTenant);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task Add_tenant_returns_created_tenant_fail()
        {
            var fakeTenant = new TenantDto
            {
                Name = "a"
            };

            Func<Task> act = async () => await _tenantAppService.AddTenantAsync(fakeTenant);

            act.Should().Throw<Exception>();
        }
    }
}
