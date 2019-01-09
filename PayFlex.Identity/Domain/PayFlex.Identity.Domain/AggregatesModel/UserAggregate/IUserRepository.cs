using Galaxy.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : ICustomRepository
    { 
        Task<User> FindByUsername(string user);
        Task<User> GetUserById(int userId);
        Task<User> GetUserAggregateById(int userId);

        Task<IEnumerable<UserAssignedToRole>> UserAssignedToRolesByUserId(int userId);
        Task<IList<UserAssignedToPermission>> GetAllUserPermissions(); 

        IQueryable<UserAssignedToTenant> GetUserTenantsByUserId(int userId);
         
        Task<User> CreateUserAsync(User user);  
        Task<List<User>> GetAllUsers();
        IQueryable<User> GetAllUsersQueryable();
        Task<bool> DeleteAsync(User user);
        Task<bool> Update(User user);
    }
}
