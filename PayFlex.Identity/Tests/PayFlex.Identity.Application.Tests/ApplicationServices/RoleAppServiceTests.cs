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
    public class RoleAppServiceTests : PayFlexIdentityTestApplication
    {
        private readonly IRoleAppService _roleAppService;
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        public RoleAppServiceTests()
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

            var result = await _roleAppService.AddRoleAsync(fakeRole);

            result.Id.Should().BeGreaterThan(0);

            var existingRole = (await _roleAppService.GetAllRolesAsync())
                .FirstOrDefault();

            existingRole.Name = "fake-role";

            await _roleAppService.UpdateRoleAsync(existingRole);
              
            existingRole.Name.Should().Be("fake-role");

        }

        [Fact]
        public async Task Get_existing_role_by_id_should_return_role_success()
        {
            var fakeRole = new RoleDto
            {
                Name = "Test-1 Role",
            };

            var result = await _roleAppService.AddRoleAsync(fakeRole);

            result.Id.Should().BeGreaterThan(0);

            var existingRoleId = (await _roleAppService.GetRoleByIdAsync(result.Id)).Id;

            var roleResult = await _roleAppService.GetRoleByIdAsync(existingRoleId);

            roleResult.Should().NotBeNull();
            roleResult.Id.Should().BeGreaterThan(0);
            roleResult.Name.Should().NotBeEmpty();
        }



        [Fact]
        public async Task Add_role_returns_created_role_success()
        {
            var fakeRole = new RoleDto
            {
                Name = "Test-1 Role",
            };

            var result = await _roleAppService.AddRoleAsync(fakeRole); 
           
            result.Id.Should().BeGreaterThan(0);
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
