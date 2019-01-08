using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.AggregatesModel.PermissionAggregate;
using PayFlex.Identity.Shared.Dtos.Permission;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Services
{
    public class PermissionAppService : CrudAppServiceAsync<PermissionDto, int, Permission>, IPermissionAppService
    {
        public PermissionAppService(IRepositoryAsync<Permission, int> repositoryAsync
            , IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {
        }

        public async Task<PermissionDto> AddPermission(PermissionDto permissionDto)
        {
          return  await AddAsync(async () => {
                var permission = Permission.Create(permissionDto.Name);
                return permission;
            });
        }

        public async Task DeletePermissionAsync(int permissionId)
        {
            // Hard Delete
            //await DeleteAsync(permissionId);

            //Soft Delete
            await UpdateAsync(permissionId, async (permission) => {
                permission.DeleteThisPermission();
            });
        }

        public  async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync()
        {
           return await QueryableNoTrack().ToListAsync();
        }

        public async Task<PermissionDto> GetPermissionByIdAsync(int permissionId)
        {
            return await FindAsync(permissionId);
        }

        public async Task UpdatePermissionAsync(PermissionDto permissionDto)
        {
            await UpdateAsync(permissionDto.Id, async permission => {
                permission.ChangeName(permissionDto.Name);
            });
        }
    }
}
