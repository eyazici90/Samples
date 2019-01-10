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
    public class PermissionAppService_Tests : PayFlexIdentityTestApplication
    {
        private readonly IPermissionAppService _permissionServ;
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        public PermissionAppService_Tests()
        {
            _unitofWorkAsync = TheObject<IUnitOfWorkAsync>();
            _permissionServ = TheObject<IPermissionAppService>();
        }


        [Fact]
        public async Task Get_all_existing_permissions_success()
        {
            List<PermissionDto> fakepermissionList = new List<PermissionDto>
            {
                new PermissionDto { Name = "test-permission-1" },
                new PermissionDto { Name = "test-permission-2" },
                new PermissionDto { Name = "test-permission-3" }
            };

            foreach (var fakePermission in fakepermissionList)
            {
                await _permissionServ.AddPermission(fakePermission);
            }

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(fakepermissionList.Count);

            var permissionResult = await _permissionServ.GetAllPermissionsAsync();

            permissionResult.Should().NotBeNull();

            permissionResult.Should().HaveCountGreaterOrEqualTo(fakepermissionList.Count);
        }



        [Fact]
        public async Task Update_permission_returns_updated_permission_success()
        {
            var fakePermission = new PermissionDto
            {
                Name = "Test-1 Permission",
            };

            await _permissionServ.AddPermission(fakePermission);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);

            var existingPermission = (await _permissionServ.GetAllPermissionsAsync())
                .FirstOrDefault();

            existingPermission.Name = "fake-permission";

            await _permissionServ.UpdatePermissionAsync(existingPermission);

            var permissionResult = await _unitofWorkAsync.SaveChangesAsync();

            permissionResult.Should().BeGreaterThan(0);
            
        }

        [Fact]
        public async Task Add_permission_returns_created_permission_success()
        {
            var fakePermission = new PermissionDto
            {
                Name = "Test-1 Permission",
            };

            await _permissionServ.AddPermission(fakePermission);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);
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
            };

            await _permissionServ.AddPermission(fakePermission);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);

            var permission = (await _permissionServ.GetAllPermissionsAsync())
                .FirstOrDefault();

            await _permissionServ.DeletePermissionAsync(permission.Id); 
            
        }
    }
}
