using Galaxy.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Domain.AggregatesModel.RoleAggregate;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.EFRepositories.RoleAggregate
{
    public class RoleRepository : IRoleRepository
    { 
        private readonly IRepositoryAsync<Role> _roleRep;
        private readonly IRepositoryAsync<User> _userRep;
        public RoleRepository(IRepositoryAsync<Role> roleRep
            , IRepositoryAsync<User> userRep)
        { 
            this._roleRep = roleRep ?? throw new ArgumentNullException(nameof(roleRep));
            this._userRep = userRep ?? throw new ArgumentNullException(nameof(userRep));
        }

        public Task<Role> FindRoleById(int roleId) =>
            this._roleRep.FindAsync(roleId);
 

        public IQueryable<Role> GetAllRoles() =>
             this._roleRep.Queryable();

        public async Task<Role> GetRoleAggregateById(int roleId)
        {
            var roleAggregate = await this._roleRep.Queryable()
               .SingleOrDefaultAsync(r => r.Id == roleId);
            return roleAggregate;
        }

        public async Task<IEnumerable<UserAssignedToRole>> GetUserAssignedToRoleByRoleId(int roleId) =>
             await this._userRep.Queryable()
                .Include(u => u.UserRoles)
                .SelectMany(u => u.UserRoles.Where(r => r.RoleId == roleId))
                .ToListAsync();

        
        public async Task<Role> CreateAsync(Role role)
        {
            await _roleRep.InsertAsync(role);
            return role;
        }

        public async Task<bool> UpdateAsync(Role role)
        {
            this._roleRep.Update(role);
            return true;
        }

        public async Task<bool> DeleteAsync(Role role)
        {
            return (await _roleRep.DeleteAsync(role.Id));
        }

        
    }
}
