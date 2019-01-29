using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.ApplicationServices
{
    public class PermissionAppServiceTests : PayFlexIdentityTestApplication
    {
        private readonly IPermissionAppService _permissionServ;
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        public PermissionAppServiceTests()
        {
            _unitofWorkAsync = TheObject<IUnitOfWorkAsync>();
            _permissionServ = TheObject<IPermissionAppService>();
        }


        [Fact]
        public async Task Get_all_existing_permissions_success()
        {
            List<PermissionDto> fakepermissionList = new List<PermissionDto>
            {
                new PermissionDto { Name = "test-permission-1", ServiceName = "test-service-1", MethodExecutionName = "test-method-1"  },
                new PermissionDto { Name = "test-permission-2", ServiceName = "test-service-2", MethodExecutionName = "test-method-2" },
                new PermissionDto { Name = "test-permission-3", ServiceName = "test-service-3", MethodExecutionName = "test-method-3" }
            };

            foreach (var fakePermission in fakepermissionList)
            {
                await _permissionServ.AddPermission(fakePermission);
            }

            var permissionResult = await _permissionServ.GetAllPermissionsAsync();

            permissionResult.Should().NotBeNull();

            permissionResult.Should().HaveCountGreaterOrEqualTo(fakepermissionList.Count);

        }

        [Fact]
        public async Task Get_existing_permission_by_id_should_return_permission_success()
        {
            var fakePermission = new PermissionDto
            {
                Name = "edit user permission",
                ServiceName = "test-edit-1",
                MethodExecutionName = "test-edit-1"
            };

            var result = await _permissionServ.AddPermission(fakePermission);
            
            result.Id.Should().BeGreaterThan(0);

            var userResult = await _permissionServ.GetPermissionByIdAsync(result.Id);

            result.Should().NotBeNull();
            result.Id.Should().BeGreaterThan(0);
            result.Name.Should().NotBeEmpty();
        }



        [Fact]
        public async Task Update_permission_returns_updated_permission_success()
        {
            var fakePermission = new PermissionDto
            {
                Name = "Test-1 Permission",
                ServiceName = "test-service-1",
                MethodExecutionName = "test-method-1"
            };

            await _permissionServ.AddPermission(fakePermission);

            var existingPermission = (await _permissionServ.GetAllPermissionsAsync())
                .FirstOrDefault();

            existingPermission.Name = "fake-permission";

            await _permissionServ.UpdatePermissionAsync(existingPermission);

            existingPermission.Name.Should().Be("fake-permission");
            
        }

        [Fact]
        public async Task Add_permission_returns_created_permission_success()
        {
            var fakePermission = new PermissionDto
            {
                Name = "Test-1 Permission",
                ServiceName = "TestService-1",
                MethodExecutionName = "TestMethod-1"
            };

            var result = await _permissionServ.AddPermission(fakePermission);
             
            result.Id.Should().BeGreaterThan(0);
            
        }

        [Fact]
        public async Task Add_permission_returns_created_permission_fail()
        {
            var fakeFailPermission = new PermissionDto
            {
                Name = "a"
            };

            Func<Task> act = async () => await _permissionServ.AddPermission(fakeFailPermission);

            act.Should().Throw<Exception>();
        }

        [Fact]
        public async Task Delete_existing_permission_returns_success()
        {
            var fakePermission = new PermissionDto
            {
                Name = "Test-1 Permission",
                ServiceName = "TestService-1",
                MethodExecutionName = "TestMethod-1"
            };

            var resut = await _permissionServ.AddPermission(fakePermission);
            
            resut.Id.Should().BeGreaterThan(0);

            var permission = (await _permissionServ.GetPermissionByIdAsync(resut.Id));

            await _permissionServ.DeletePermissionAsync(permission.Id); 
            
        }
    }
}
