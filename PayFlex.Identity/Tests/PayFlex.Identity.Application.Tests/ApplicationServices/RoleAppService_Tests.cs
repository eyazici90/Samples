using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Role;
using System;
using System.Collections.Generic;
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
