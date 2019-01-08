using Galaxy.Application;
using Galaxy.UnitOfWork;
using PayFlex.Identity.Shared.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Contracts.Services
{
    public interface IPermissionAppService : IApplicationService
    {
        [EnableUnitOfWork]
        Task<PermissionDto> AddPermission(PermissionDto permissionDto);

        Task<PermissionDto> GetPermissionByIdAsync(int permissionId);

        Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync();

        [EnableUnitOfWork]
        Task DeletePermissionAsync(int permissionId);

        [EnableUnitOfWork]
        Task UpdatePermissionAsync(PermissionDto permission);
    }
}
