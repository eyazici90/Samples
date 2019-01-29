using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.ApplicationServices
{

    public class TenantAppServiceTests : PayFlexIdentityTestApplication
    {
        private readonly ITenanAppService _tenantAppService;
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        public TenantAppServiceTests()
        {
            _unitofWorkAsync = TheObject<IUnitOfWorkAsync>();
            _tenantAppService = TheObject<ITenanAppService>();
        }


        [Fact]
        public async Task Get_all_existing_tenants_should_return_all_tenants()
        {
            List<TenantDto> faketenantList = new List<TenantDto>
            {
                new TenantDto { Name = "test-tenant-1" },
                new TenantDto { Name = "test-tenant-2" },
                new TenantDto { Name = "test-tenant-3" }
            };

            foreach (var fakePermission in faketenantList)
            {
                await _tenantAppService.AddTenantAsync(fakePermission);
            }
             
            var tenantResult = await _tenantAppService.GetAllTenantsAsync();

            tenantResult.Should().NotBeNull();

            tenantResult.Should().HaveCountGreaterOrEqualTo(faketenantList.Count);

        }

        [Fact]
        public async Task Get_all_existing_tenants_should_return_the_tenant()
        {
            var fakeTenant = new TenantDto
            {
                Name = "Test-1 Tenant",
            };

            var result = await _tenantAppService.AddTenantAsync(fakeTenant); 

            result.Id.Should().BeGreaterThan(0);

            var existingTenant = (await _tenantAppService.GetAllTenantsAsync())
                .FirstOrDefault();

            var tenantById = await _tenantAppService.GetTenantByIdAsync(existingTenant.Id);

            tenantById.Should().NotBeNull();
            tenantById.Id.Should().BeGreaterThan(0);
            tenantById.Name.Should().NotBeEmpty();
        }



        [Fact]
        public async Task Update_tenant_should_return_updated_tenant()
        {
            var fakeTenant = new TenantDto
            {
                Name = "Test-1 Tenant",
            };

            var result = await _tenantAppService.AddTenantAsync(fakeTenant);
            
            result.Id.Should().BeGreaterThan(0);

            var existingTenant = (await _tenantAppService.GetAllTenantsAsync())
                .FirstOrDefault();

            existingTenant.Name = "fake-tenant";

            existingTenant.Description = "fake_desc";

            await _tenantAppService.UpdateTenantAsync(existingTenant);

            existingTenant.Name.Should().Be("fake-tenant");

        }



        [Fact]
        public async Task Add_tenant_returns_created_tenant_success()
        {
            var fakeTenant = new TenantDto
            {
                Name = "Test-1 Tenant",
                Description = "aassd"
            };

            var result =  await _tenantAppService.AddTenantAsync(fakeTenant);
             
            result.Id.Should().BeGreaterThan(0);
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
