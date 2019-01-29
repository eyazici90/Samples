using Galaxy.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PayFlex.Identity.Domain.AggregatesModel.UserAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PayFlex.Identity.Persistance.EF.UserAggregate
{
    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryAsync<User> _userRep; 
        public UserRepository(IRepositoryAsync<User> userRep )
        { 
            this._userRep = userRep ?? throw new ArgumentNullException(nameof(userRep)); 
        }

        public Task<List<User>> GetAllUsers() =>
            this._userRep.Queryable().ToListAsync();


        public Task<User> GetUserById(int userId) =>
            this._userRep.FindAsync(userId);


        public IQueryable<User> GetAllUsersQueryable() =>
            this._userRep.Queryable();


        public async Task<IEnumerable<UserAssignedToRole>> GetUserRolesByUserId(int userId) =>
            await this._userRep.Queryable()
                .Include(u => u.UserRoles)
                .SelectMany(u => u.UserRoles.Where(r => r.UserId == userId))
                .ToListAsync();

        public IQueryable<UserAssignedToTenant> GetUserTenantsByUserId(int userId) => 
            _userRep.Queryable()
                   .SelectMany(t => t.UserTenants)
                   .Where(u => u.UserId == userId);

        public IQueryable<UserAssignedToTenant> GetUserTenantsByUserName(string username) =>
            _userRep.Queryable()
            .Where(u=>u.UserName == username)
               .SelectMany(t => t.UserTenants);

        public IQueryable<UserAssignedToPermission> GetUserPermissionsByUserId(int userId) =>
          _userRep.Queryable()
          .Where(u => u.Id == userId)
             .SelectMany(t => t.UserPermissions);

        public async Task<User> FindByUsername(string user) =>
            await this._userRep.Queryable()
                .SingleOrDefaultAsync(u => u.UserName == user.Trim());

        
        public async Task<User> CreateUserAsync(User user)
        { 
            await this._userRep.InsertAsync(user);
            return user;
        } 

        public async Task<bool> DeleteAsync(User user)
        {
            return (await this._userRep.DeleteAsync(user));
        }

        public async Task<bool> Update(User user)
        {
            this._userRep.Update(user);
            return await Task.FromResult(true);
        }

        public async Task<User> GetUserAggregateById(int userId) =>
            await this._userRep.Queryable()
                .Include(r => r.UserRoles)
                .SingleOrDefaultAsync(u => u.Id == userId);

        public async Task<IList<UserAssignedToPermission>> GetAllUserPermissions() =>
            await this._userRep.QueryableNoTrack()
                .SelectMany(p => p.UserPermissions)
                .ToListAsync();

      
    }
}
