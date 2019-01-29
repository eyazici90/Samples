using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Optional;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.Shared.Dtos.User;
using PayFlex.Identity.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Services
{
    public class UserAppService : CrudAppServiceAsync<User, UserDto, int>, IUserAppService
    { 
        private readonly IUserRepository _userRep;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserManager<User> _userManager;
        public UserAppService(IRepositoryAsync<User, int> repositoryAsync
            , IUnitOfWorkAsync unitOfWorkAsync
            , IObjectMapper objectMapper
            , IUserRepository userRep
            , IPasswordHasher<User> passwordHasher
            , UserManager<User> userManager): base(repositoryAsync, unitOfWorkAsync, objectMapper)
        {
            _userRep = userRep;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
        }
         
        public async Task<UserDto> AddUserAsync(CreateUserRequest userDto)
        {
           return await AddAsync(async () => {
                var user = User.Create(userDto.UserName, userDto.Email);
                var hashedPassword = _passwordHasher.HashPassword(user, userDto.Password);
                user.ChangePassword(hashedPassword);
                return user;
            });
        }

        public async Task AssignPermissionToUser(UserDto userDto, int permissionId)
        {
            await UpdateAsync(userDto.Id, async user => {
                user.AssignPermission(permissionId);
            });
        }

        public async Task AssignRoleToUser(UserDto userDto, int roleId)
        {
            await UpdateAsync(userDto.Id, async user => {
                user.AssignRole(roleId);
            });
        }
        
        public async Task AssignTenantToUser(UserDto userDto, int tenantId)
        {
            await UpdateAsync(userDto.Id, async user => {
                user.AssignTenant(tenantId);
            });
        }

        public async Task DeleteUserAsync(int userId) =>
           await DeleteAsync(userId);
        

        public async Task<UserDto> FindByUsernameAsync(string userName) =>
           await QueryableNoTrack()  
                .SingleOrDefaultAsync((u => u.UserName == userName));
        

        public async Task<List<UserDto>> GetAllUsersAsync() =>
            await QueryableNoTrack().ToListAsync();
        

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            return await FindAsync(userId);
        }

        public  async Task<IEnumerable<UserAssignedToTenantDto>>  GetUserTenantsByUserId(int userId) =>
          await ObjectMapper.MapTo<UserAssignedToTenantDto>(
                 _userRep.GetUserTenantsByUserId(userId)
             )
            .ToListAsync();

        public async Task<IEnumerable<UserAssignedToTenantDto>> GetUserTenantsByUserName(string userName) =>
        await ObjectMapper.MapTo<UserAssignedToTenantDto>(
               _userRep.GetUserTenantsByUserName(userName)
            )
           .ToListAsync();


        public async Task<IEnumerable<UserAssignedToPermissionDto>> GetUserPermissionsByUserId(int userId) =>
          await ObjectMapper.MapTo<UserAssignedToPermissionDto>(
                 _userRep.GetUserPermissionsByUserId(userId)
            )
           .ToListAsync();

        public async Task UpdateUserAsync(UserDto userDto)
        {
            await UpdateAsync(userDto.Id, async user => { 
                user.ChangeUserName(userDto.UserName);
                user.ChangeEmail(userDto.Email);
                user.ChangePhoneNumber(string.Empty); 
            });
        }

        public async Task<IEnumerable<UserAssignedToRoleDto>> GetUserRolesByUserId(int userId) =>
             ObjectMapper.MapTo<IEnumerable<UserAssignedToRoleDto>>(
                    await _userRep.GetUserRolesByUserId(userId)
                );
      

        public async Task<bool> ValidateCredentials(ValidateCreadentialsRequest userDto)
        {
            var result = false;
            await (await this._userManager.FindByIdAsync(userDto.UserId.ToString()))
                .SomeNotNull()
                .Match(async user =>
                {
                    result = await _userManager.CheckPasswordAsync(user, userDto.Password);
                }, ()=> throw new ArgumentNullException(userDto.UserId.ToString()));
             
            return result;
        }

        public async Task<bool> ValidateCredentialsByUserName(UserCredantialsDto credentials)
        {
            var result = false;
            await (await this._userRep.FindByUsername(credentials.Username))
              .SomeNotNull()
              .Match(async user =>
              {
                  result = await _userManager.CheckPasswordAsync(user, credentials.Password);

              }, () => throw new ArgumentNullException(credentials.Username));

            return result;
        }
    }
}
