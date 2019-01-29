using Galaxy.Application;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Shared.Dtos.User;
using PayFlex.Identity.Shared.Requests;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Contracts.Services
{
    public interface IUserAppService : IApplicationService
    {
        [EnableUnitOfWork]
        Task AssignRoleToUser(UserDto userDto, int roleId);

        Task<UserDto> FindByUsernameAsync(string userName);

        [EnableUnitOfWork]
        Task AssignPermissionToUser(UserDto userDto, int permissionId);

        [EnableUnitOfWork]
        Task AssignTenantToUser(UserDto userDto, int tenantId);

        Task<IEnumerable<UserAssignedToRoleDto>> GetUserRolesByUserId(int userId);

        Task<UserDto> GetUserByIdAsync(int userId);

        Task<List<UserDto>> GetAllUsersAsync();

        Task<IEnumerable<UserAssignedToTenantDto>> GetUserTenantsByUserId(int userId);

        Task<IEnumerable<UserAssignedToTenantDto>> GetUserTenantsByUserName(string userName);

        Task<IEnumerable<UserAssignedToPermissionDto>> GetUserPermissionsByUserId(int userId);

        [EnableUnitOfWork]
        Task<UserDto> AddUserAsync(CreateUserRequest user);

        [EnableUnitOfWork]
        Task DeleteUserAsync(int userId);

        [EnableUnitOfWork]
        Task UpdateUserAsync(UserDto user);

        Task<bool> ValidateCredentials(ValidateCreadentialsRequest userDto); 

        Task<bool> ValidateCredentialsByUserName(UserCredantialsDto credentials);
    }
}
