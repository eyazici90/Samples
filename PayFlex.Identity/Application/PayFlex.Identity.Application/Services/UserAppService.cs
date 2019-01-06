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
        private readonly IRepositoryAsync<User, int> _userRep;
        public UserAppService(IRepositoryAsync<User, int> repositoryAsync
            , IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {
            _userRep = repositoryAsync;
        }

        public async Task<UserDto> AddUser(UserDto userDto)
        {
            return await AddAsync(async () => {
                var user = User.Create(userDto.UserName, userDto.TenantId);
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

        public async Task DeleteUserAsync(int userId)
        {
            await DeleteAsync(userId);
        }

        public async Task<UserDto> FindByUsername(string userName)
        {
           return  await QueryableNoTrack()
                .Where(u=> u.UserName == userName).FirstOrDefaultAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
        {
            return await QueryableNoTrack().ToListAsync();
        }

        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            return await FindAsync(userId);
        }

        public async Task UpdateUserAsync(UserDto userDto)
        {
            await UpdateAsync(userDto.Id, async user => {
               // User updates 
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
