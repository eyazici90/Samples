using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.Exceptions;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.ApplicationServices
{
    public class UserAppService_Tests : PayFlexIdentityTestApplication
    {
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        private readonly IUserAppService _userAppService;
        public UserAppService_Tests()
        { 
            _unitofWorkAsync = TheObject<IUnitOfWorkAsync>();
            _userAppService = TheObject<IUserAppService>();
        }
 

        [Fact]
        public async Task Get_existing_user_by_name_returns_user_success()
        {
            var fakeUser = new UserDto
            {
                UserName = "leman.yazici",
                Email = "emre.yazici@gmail.com",
                TenantId = 1
            };

            await _userAppService.AddUserAsync(fakeUser);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);

            var userResult = await _userAppService.FindByUsernameAsync(fakeUser.UserName);

            userResult.Should().NotBeNull();

            userResult.UserName.Should().BeSameAs(userResult.UserName);

        }

        [Fact]
        public async Task Get_all_existing_users_success()
        {
            List<UserDto> fakeUserList = new List<UserDto>
            {
                new UserDto {UserName = "test-user-1", Email = "testuser1@gmail.com", TenantId = 1},
                new UserDto {UserName = "test-user-2", Email = "testuser2@gmail.com", TenantId = 1},
                new UserDto {UserName = "test-user-3", Email = "testuser3@gmail.com", TenantId = 1}
            };

            foreach (var fakeUser in fakeUserList)
            {
                await _userAppService.AddUserAsync(fakeUser);
            }
            
            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(fakeUserList.Count);

            var userResult = await _userAppService.GetAllUsersAsync();

            userResult.Should().NotBeNull();

            userResult.Should().HaveCountGreaterOrEqualTo(fakeUserList.Count);
        }
        
        [Fact]
        public async Task Add_user_returns_created_user_success()
        { 
            var fakeUser = new UserDto
            {
                UserName = "test-user",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            await _userAppService.AddUserAsync(fakeUser);

            var result = await _unitofWorkAsync.SaveChangesAsync();

            result.Should().Be(1);
        }

        [Fact]
        public async Task Add_user_returns_created_user_fail()
        {
            var fakeFailUser = new UserDto
            {
                UserName = "fail",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            Func<Task> act = async () => await _userAppService.AddUserAsync(fakeFailUser);

            act.Should().Throw<Exception>();
        }

        [Fact]
        public async Task Assign_permission_to_user_success()
        {
            var fakeUser = new UserDto
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            var fakePermissionId = 1;

            var user = await _userAppService.AddUserAsync(fakeUser);

            var userResult = await _unitofWorkAsync.SaveChangesAsync();

            userResult.Should().Be(1);

            await _userAppService.AssignPermissionToUser(user, fakePermissionId);

            var permissionResult = await _unitofWorkAsync.SaveChangesAsync();

            permissionResult.Should().BeGreaterThan(0); 
        }

        [Fact]
        public async Task Assign_permission_to_user_fail()
        {
            var fakeUser = new UserDto
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            var fakePermissionId = -1;

            var user = await _userAppService.AddUserAsync(fakeUser);

            var userResult = await _unitofWorkAsync.SaveChangesAsync();

            userResult.Should().Be(1);
            
            Func<Task> act = async () => await _userAppService.AssignPermissionToUser(user, fakePermissionId); ;
            
             act.Should().Throw<IdentityDomainException>();
        }

        [Fact]
        public async Task Assign_role_to_user_success()
        {
            var fakeUser = new UserDto
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            var fakeroleId = 1;

            var user = await _userAppService.AddUserAsync(fakeUser);

            var userResult = await _unitofWorkAsync.SaveChangesAsync();

            userResult.Should().Be(1);

            await _userAppService.AssignRoleToUser(user, fakeroleId);

            var roleResult = await _unitofWorkAsync.SaveChangesAsync();

            roleResult.Should().BeGreaterThan(0); 
        }

        [Fact]
        public async Task Assign_role_to_user_fail()
        {
            var fakeUser = new UserDto
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            var fakeRoleId = -1;

            var user = await _userAppService.AddUserAsync(fakeUser);

            var userResult = await _unitofWorkAsync.SaveChangesAsync();

            userResult.Should().Be(1);
            
            Func<Task> act = async () => await _userAppService.AssignRoleToUser(user, fakeRoleId); 
            
             act.Should().Throw<IdentityDomainException>();
        }

        [Fact]
        public async Task Assign_tenant_to_user_success()
        {
            var fakeUser = new UserDto
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            var faketenantId = 2;

            var user = await _userAppService.AddUserAsync(fakeUser);

            var userResult = await _unitofWorkAsync.SaveChangesAsync();

            userResult.Should().Be(1);

            await _userAppService.AssignTenantToUser(user, faketenantId);

            var tenantResult = await _unitofWorkAsync.SaveChangesAsync();

            tenantResult.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Assign_tenant_to_user_fail()
        {
            var fakeUser = new UserDto
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                TenantId = 1
            };

            var fakeTenantId = -1;

            var user = await _userAppService.AddUserAsync(fakeUser);

            var userResult = await _unitofWorkAsync.SaveChangesAsync();

            userResult.Should().Be(1);

            Func<Task> act = async () => await _userAppService.AssignTenantToUser(user, fakeTenantId);

            act.Should().Throw<IdentityDomainException>();
        }

        //[Fact]
        //public async void Delete_existing_user_success()
        //{
        //    var fakeUser = new UserDto
        //    {
        //        UserName = "test-1 user",
        //        Email = "testuser@gmail.com",
        //        TenantId = 1
        //    };

        //    var user = await _userAppService.AddUserAsync(fakeUser);

        //    var userResult = await _unitofWorkAsync.SaveChangesAsync();

        //    userResult.Should().Be(1);

        //    await _userAppService.DeleteUserAsync(1);

        //    var deleteResult = await _unitofWorkAsync.SaveChangesAsync();

        //    deleteResult.Should().BeGreaterThan(0);
        //}

    }
}
