using Galaxy.Application;
using Galaxy.ObjectMapping;
using Galaxy.Repositories;
using Galaxy.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Shared.Dtos.Role;
using PayFlex.Identity.Shared.Dtos.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Application.Services
{
    public class RoleAppService : CrudAppServiceAsync<RoleDto, int, Role>, IRoleAppService
    {
        public RoleAppService(IRepositoryAsync<Role, int> repositoryAsync
            , IObjectMapper objectMapper) : base(repositoryAsync, objectMapper)
        {
        }

        public async Task<RoleDto> AddRole(RoleDto roleDto)
        {
            return await AddAsync(async () => {
                var role = Role.Create(roleDto.Name);
                return role;
            });
        }

        public  async Task<IList<RoleDto>> GetAllRolesAsync()
        {
            return await QueryableNoTrack().ToListAsync();
        }

        public async Task<RoleDto> GetRoleByIdAsync(int roleId)
        {
            return await FindAsync(roleId);
        }

        public async Task<IEnumerable<UserAssignedToRoleDto>> GetUserAssignedToRoleByRoleId(int roleId)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateRole(RoleDto roleDto)
        {
            await UpdateAsync(roleDto.Id, async role => {
                role.ChangeName(roleDto.Name);
            });
        }
    }
}
