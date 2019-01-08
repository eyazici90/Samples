﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.User;

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
        public async Task<IEnumerable<UserAssignedToRoleDto>> UserAssignedToRolesByUserId(int id) =>
               await this._userAppServ.UserAssignedToRolesByUserId(id);

        [Route("api/v1/Identity/Users")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserDto>> GetAllUsers() =>
               await this._userAppServ.GetAllUsersAsync();

        [AllowAnonymous]
        [Route("api/v1/Identity/User")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<UserDto> Create([FromBody] UserDto user) =>
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
        public async Task ValidateCredentials([FromBody] UserDto user) =>
            await this._userAppServ.ValidateCredentials(user, "123456.");
           

        [Route("api/v1/Identity/User/AssignToRole")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task AssignRoleToUser([FromBody] UserDto user, [FromQuery]int roleId) =>
            await this._userAppServ.AssignRoleToUser(user, roleId);
           

        [Route("api/v1/Identity/User/AssignToPermission")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task AssignToPermission([FromBody] UserDto user, [FromQuery]int permissionId) =>
            await this._userAppServ.AssignPermissionToUser(user, permissionId);
         


        [Route("api/v1/Identity/User/AssignToTenant")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task AssignToTenant([FromBody] UserDto user, [FromQuery]int tenantId) =>
            await this._userAppServ.AssignTenantToUser(user, tenantId);
           

    }
}
