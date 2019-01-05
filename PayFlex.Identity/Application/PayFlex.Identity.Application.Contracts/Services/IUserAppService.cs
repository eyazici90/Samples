using Galaxy.Application;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Shared.Dtos.User;
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

        Task<UserDto> FindByUsername(string userName);

        [EnableUnitOfWork]
        Task AssignPermissionToUser(UserDto userDto, int permissionId);

        Task<IEnumerable<UserAssignedToRoleDto>> UserAssignedToRolesByUserId(int userId);

        Task<UserDto> GetUserByIdAsync(int userId);

        Task<List<UserDto>> GetAllUsersAsync();

        [EnableUnitOfWork]
        Task<UserDto> AddUser(UserDto user);

        [EnableUnitOfWork]
        Task DeleteUserAsync(int userId);

        [EnableUnitOfWork]
        Task UpdateUserAsync(UserDto user);

        Task ValidateCredentials(UserDto user, string password);

        Task<UserDto> ValidateCredentialsByUserName(UserCredantialsDto credentials);
    }
}
