using FluentAssertions;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.Exceptions;
using PayFlex.Identity.Shared.Dtos.User;
using PayFlex.Identity.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PayFlex.Identity.Application.Tests.ApplicationServices
{
    public class UserAppServiceTests : PayFlexIdentityTestApplication
    {
        private readonly IUnitOfWorkAsync _unitofWorkAsync;
        private readonly IUserAppService _userAppService;
        public UserAppServiceTests()
        { 
            _unitofWorkAsync = TheObject<IUnitOfWorkAsync>();
            _userAppService = TheObject<IUserAppService>();
        }
 

        [Fact]
        public async Task Get_existing_user_by_name_should_return_user_success()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "leman.yazici",
                Email = "emre.yazici@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var result = await _userAppService.AddUserAsync(fakeUser);
             
            result.Id.Should().BeGreaterThan(0);

            var userResult = await _userAppService.FindByUsernameAsync(fakeUser.UserName);

            userResult.Should().NotBeNull();

            userResult.UserName.Should().BeSameAs(userResult.UserName);
            
        }

        [Fact]
        public async Task Get_existing_user_by_id_should_return_user_success()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "berkay.yazici",
                Email = "emre.yazici@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var result = await _userAppService.AddUserAsync(fakeUser);
 
            result.Id.Should().BeGreaterThan(0);

            var existingUserId = (await _userAppService.GetUserByIdAsync(result.Id)).Id;
            
            var userResult = await _userAppService.GetUserByIdAsync(existingUserId);

            userResult.Should().NotBeNull();
            userResult.Id.Should().BeGreaterThan(0);
            userResult.UserName.Should().NotBeEmpty();
        }

        [Fact]
        public async Task Get_all_existing_users_success()
        {
            List<CreateUserRequest> fakeUserList = new List<CreateUserRequest>
            {
                new CreateUserRequest {UserName = "test-user-1", Email = "testuser1@gmail.com", Password = "test-1234", TenantId = 1},
                new CreateUserRequest {UserName = "test-user-2", Email = "testuser2@gmail.com", Password = "test-1234", TenantId = 1},
                new CreateUserRequest {UserName = "test-user-3", Email = "testuser3@gmail.com", Password = "test-1234", TenantId = 1}
            };

            foreach (var fakeUser in fakeUserList)
            {
                await _userAppService.AddUserAsync(fakeUser);
            } 

            var userResult = await _userAppService.GetAllUsersAsync();

            userResult.Should().NotBeNull();

            userResult.Should().HaveCountGreaterOrEqualTo(fakeUserList.Count);
        }
        
        [Fact]
        public async Task Add_user_returns_created_user_success()
        { 
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var result = await _userAppService.AddUserAsync(fakeUser);
 
            result.Id.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task Add_user_returns_created_user_fail()
        {
            var fakeFailUser = new CreateUserRequest
            {
                UserName = "fail",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            Func<Task> act = async () => await _userAppService.AddUserAsync(fakeFailUser);

            act.Should().Throw<Exception>();
        }

        [Fact]
        public async Task Assign_permission_to_user_success()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var fakePermissionId = 1;

            var user = await _userAppService.AddUserAsync(fakeUser);
            
            user.Id.Should().BeGreaterThan(0);

            await _userAppService.AssignPermissionToUser(user, fakePermissionId); 
             
            // add GetUserPermission to userAppService 
            // assert for existng user permissions
        }

        [Fact]
        public async Task Assign_permission_to_user_fail()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var fakePermissionId = -1;

            var user = await _userAppService.AddUserAsync(fakeUser);
             
            user.Id.Should().BeGreaterThan(0);
            
            Func<Task> act = async () => await _userAppService.AssignPermissionToUser(user, fakePermissionId); ;
            
             act.Should().Throw<IdentityDomainException>();
        }

        [Fact]
        public async Task Assign_role_to_user_success()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var fakeroleId = 1;

            var user = await _userAppService.AddUserAsync(fakeUser);

            user.Id.Should().BeGreaterThan(0);

            await _userAppService.AssignRoleToUser(user, fakeroleId);

            // add GetUserRoles to userAppService and refactor test

            //var roleResult = await _userAppService.get();

            //roleResult.Should().BeGreaterThan(0); 
        }

        [Fact]
        public async Task Assign_role_to_user_fail()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var fakeRoleId = -1;

            var user = await _userAppService.AddUserAsync(fakeUser);

            user.Id.Should().BeGreaterThan(0);
            
            Func<Task> act = async () => await _userAppService.AssignRoleToUser(user, fakeRoleId); 
            
             act.Should().Throw<IdentityDomainException>();
        }

        [Fact]
        public async Task Assign_tenant_to_user_success()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var faketenantId = 2;

            var user = await _userAppService.AddUserAsync(fakeUser);

            user.Id.Should().BeGreaterThan(0);

            await _userAppService.AssignTenantToUser(user, faketenantId);

            var result = await _userAppService.GetUserTenantsByUserId(user.Id);

            result.Should().Contain(t=>t.TenantId == faketenantId);
        }

        [Fact]
        public async Task Assign_tenant_to_user_fail()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-1 user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var fakeTenantId = -1;

            var user = await _userAppService.AddUserAsync(fakeUser);
 
            user.Id.Should().BeGreaterThan(0);

            Func<Task> act = async () => await _userAppService.AssignTenantToUser(user, fakeTenantId);

            act.Should().Throw<IdentityDomainException>();
        }

        [Fact]
        public async Task Update_user_should_return_updated_user_success()
        {
            var fakeUser = new CreateUserRequest
            {
                UserName = "test-3-user",
                Email = "testuser@gmail.com",
                Password = "test-1234",
                TenantId = 1
            };

            var result = await _userAppService.AddUserAsync(fakeUser);
 
            result.Id.Should().BeGreaterThan(0);

            var existingUser = (await _userAppService.GetUserByIdAsync(result.Id));

            existingUser.UserName = "fake_user_name";

            await _userAppService.UpdateUserAsync(existingUser);

            var userResult = await _unitofWorkAsync.SaveChangesAsync();

            existingUser.UserName.Should().Be("fake_user_name"); 

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
