using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.User;
using PayFlex.Identity.Shared.Requests;

namespace PayFlex.Identity.API.Controllers
{
    [ApiController] 
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppServ;
        public UserController(IUserAppService userAppServ)
        {
            _userAppServ = userAppServ ?? throw new ArgumentNullException(nameof(userAppServ));
        }

        [Route("api/v1/Identity/User/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<UserDto> GetUserById(int id) =>
            await this._userAppServ.GetUserByIdAsync(id);

        [Route("api/v1/Identity/User/{id}/Roles")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserAssignedToRoleDto>> GetUserRolesByUserId(int id) =>
            await this._userAppServ.GetUserRolesByUserId(id);

        [Route("api/v1/Identity/User/{id}/Permissions")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserAssignedToPermissionDto>> GetUserPermissionsByUserId(int id) =>
            await this._userAppServ.GetUserPermissionsByUserId(id);

        [Route("api/v1/Identity/User/{id}/Tenants")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserAssignedToTenantDto>> GetUserTenantsByUserId(int id) =>
            await this._userAppServ.GetUserTenantsByUserId(id);

        [Route("api/v1/Identity/User/Tenants")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserAssignedToTenantDto>> GetUserTenantsByUserName([FromQuery] string username) =>
            await this._userAppServ.GetUserTenantsByUserName(username);

        [Route("api/v1/Identity/Users")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserDto>> GetAllUsers() =>
            await this._userAppServ.GetAllUsersAsync();
         
        [Route("api/v1/Identity/User")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<UserDto> Create([FromBody] CreateUserRequest user) =>
            await this._userAppServ.AddUserAsync(user);

        [Route("api/v1/Identity/User")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Update([FromBody] UserDto user) =>
            await this._userAppServ.UpdateUserAsync(user);
               
        [Route("api/v1/Identity/User/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Delete([FromRoute]int id) =>
            await this._userAppServ.DeleteUserAsync(id);

        [Route("api/v1/Identity/User/ValidateCredentials")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<bool> ValidateCredentials([FromBody] ValidateCreadentialsRequest user) =>
            await this._userAppServ.ValidateCredentials(user);

        [Route("api/v1/Identity/User/Assign/Role")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task AssignRoleToUser([FromBody] UserDto user, [FromQuery]int roleId) =>
            await this._userAppServ.AssignRoleToUser(user, roleId);
           
        [Route("api/v1/Identity/User/Assign/Permission")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task AssignToPermission([FromBody] UserDto user, [FromQuery]int permissionId) =>
            await this._userAppServ.AssignPermissionToUser(user, permissionId);
         
        [Route("api/v1/Identity/User/Assign/Tenant")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task AssignToTenant([FromBody] UserDto user, [FromQuery]int tenantId) =>
            await this._userAppServ.AssignTenantToUser(user, tenantId);

        [Route("api/v1/Identity/User/ValidateCredentialsByUserName")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<bool> ValidateCredentialsByUserName([FromBody] UserCredantialsDto credantialsDto) =>
           await this._userAppServ.ValidateCredentialsByUserName(credantialsDto);
        
    }
}
