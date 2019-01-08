using Galaxy.Application;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Shared.Dtos.Role;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Contracts.Services
{
    public interface IRoleAppService : IApplicationService
    {
        [EnableUnitOfWork]
        Task<RoleDto> AddRoleAsync(RoleDto roleDto);

        Task<IEnumerable<UserAssignedToRoleDto>> GetUserAssignedToRoleByRoleId(int roleId);

        Task<RoleDto> GetRoleByIdAsync(int roleId);

        Task<IList<RoleDto>> GetAllRolesAsync();

        [EnableUnitOfWork]
        Task UpdateRoleAsync(RoleDto role);

        Task DeleteRoleAsync(int roleId);
    }
}
