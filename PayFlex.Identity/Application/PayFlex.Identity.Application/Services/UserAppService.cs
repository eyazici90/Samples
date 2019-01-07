using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
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
        public UserAppService(IRepositoryAsync<User, int> repositoryAsync
            , IObjectMapper objectMapper
            , IUserRepository userRep) : base(repositoryAsync, objectMapper)
        {
            _userRep = userRep; 
        }

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            var user = User.Create(userDto.UserName, userDto.Email, userDto.TenantId.Value);
            var createdUser = await this._userRep.CreateUserAsync(user, "123456.");
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

        public async Task<UserDto> FindByUsername(string userName)
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

        public async Task ValidateCredentials(UserDto user, string password)
        {
            
        }

        public async Task<UserDto> ValidateCredentialsByUserName(UserCredantialsDto credentials)
        {
            return new UserDto() ;
        }
    }
}
