using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Services
{
    public class UserAppService : CrudAppServiceAsync<UserDto, int, User>, IUserAppService
    { 
        private readonly IUserRepository _userRep;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public UserAppService(IRepositoryAsync<User, int> repositoryAsync
            , IObjectMapper objectMapper
            , IUserRepository userRep
            , IPasswordHasher<User> passwordHasher
            , UserManager<User> userManager
            , SignInManager<User> signInManager) : base(repositoryAsync, objectMapper)
        {
            _userRep = userRep;
            _userManager = userManager;
            _signInManager = signInManager;
            _passwordHasher = passwordHasher;
        }
         
        public async Task<UserDto> AddUserAsync(UserDto userDto)
        {
            var user = User.Create(userDto.UserName, userDto.Email, userDto.TenantId.Value);
            var hashedPassword = _passwordHasher.HashPassword(user, "123456.");
            user.ChangePassword(hashedPassword);
            var createdUser = await this._userRep.CreateUserAsync(user);
            return this._objectMapper.MapTo<UserDto>(createdUser);
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

        public async Task DeleteUserAsync(int userId)
        {
            await DeleteAsync(userId);
        }

        public async Task<UserDto> FindByUsernameAsync(string userName)
        {
           return  await QueryableNoTrack() 
                .Where(u=> u.UserName == userName)
                .SingleOrDefaultAsync();
        }  

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await QueryableNoTrack().ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            return await FindAsync(userId);
        }

        public  async Task<IEnumerable<UserAssignedToTenantDto>>  GetUserTenantsByUserId(int userId) =>
          await _objectMapper.MapTo<UserAssignedToTenantDto>(
                 _userRep.GetUserTenantsByUserId(userId)
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

        public async Task<IEnumerable<UserAssignedToRoleDto>> UserAssignedToRolesByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidateCredentials(UserDto userDto, string password)
        {
            var user = await this._userManager.FindByIdAsync(userDto.Id.ToString());
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<bool> ValidateCredentialsByUserName(UserCredantialsDto credentials)
        {
             var user = await this._userRep.FindByUsername(credentials.Username);
             if (user == null)
                throw new ArgumentNullException(nameof(user));
             var result = await _userManager.CheckPasswordAsync(user,credentials.Password);
             return result;
        }
    }
}
