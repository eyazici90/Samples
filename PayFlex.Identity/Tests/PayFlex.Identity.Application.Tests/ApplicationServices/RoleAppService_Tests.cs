using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Role;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.ApplicationServices
{
    public class RoleAppService_Tests : PayFlexIdentityTestApplication
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        public RoleAppService_Tests()
        {
            _unitofWorkAsync = TheObject<IUnitOfWorkAsync>();
            _roleAppService = TheObject<IRoleAppService>();
        }

        [Fact]
        public async Task Get_all_existing_roles_success()
        {
            List<RoleDto> fakeRoleList = new List<RoleDto>
            {
                new RoleDto { Name = "test-role-1" },
                new RoleDto { Name = "test-role-2" },
                new RoleDto { Name = "test-role-3" }
            };

            foreach (var fakeRole in fakeRoleList)
            {
                await _roleAppService.AddRoleAsync(fakeRole);
            }

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(fakeRoleList.Count);

            var roleResult = await _roleAppService.GetAllRolesAsync();

            roleResult.Should().NotBeNull();

            roleResult.Should().HaveCountGreaterOrEqualTo(roleResult.Count);
        }

        [Fact]
        public async Task Update_role_returns_updated_role_success()
        {
            var fakeRole = new RoleDto
            {
                Name = "Test-1 Role",
            };

            await _roleAppService.AddRoleAsync(fakeRole);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);

            var existingRole = (await _roleAppService.GetAllRolesAsync())
                .FirstOrDefault();

            existingRole.Name = "fake-role";

            await _roleAppService.UpdateRoleAsync(existingRole);

            var roleResult = await _unitofWorkAsync.SaveChangesAsync();

            roleResult.Should().BeGreaterThan(0);

        }


        [Fact]
        public async Task Add_role_returns_created_role_success()
        {
            var fakeRole = new RoleDto
            {
                Name = "Test-1 Role",
            };

            await _roleAppService.AddRoleAsync(fakeRole);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task Add_role_returns_created_role_fail()
        {
            var fakeFailRole = new RoleDto
            {
                Name = "a"
            };

            Func<Task> act = async () => await _roleAppService.AddRoleAsync(fakeFailRole);

            act.Should().Throw<Exception>();
        }
    }
}
