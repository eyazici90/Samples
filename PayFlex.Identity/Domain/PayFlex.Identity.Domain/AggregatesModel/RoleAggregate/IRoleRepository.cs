using Galaxy.Repositories;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Domain.AggregatesModel.RoleAggregate
{
    public interface IRoleRepository : ICustomRepository
    {
        Task<Role> GetRoleAggregateById(int roleId);

        Task<Role> FindRoleById(int roleId);
        IQueryable<Role> GetAllRoles(); 
        Task<IEnumerable<UserAssignedToRole>> GetUserAssignedToRoleByRoleId(int roleId);
        Task<Role> CreateAsync(Role role);
        Task<bool> UpdateAsync(Role role);
        Task<bool> DeleteAsync(Role role);
    }
}
