using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Permission;
using System;
using System.Collections.Generic;
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
    }
}
