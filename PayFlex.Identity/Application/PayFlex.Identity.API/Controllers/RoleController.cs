using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Role;
using PayFlex.Identity.Shared.Dtos.User;

namespace PayFlex.Identity.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RoleController : ControllerBase
    {
        private readonly IRoleAppService _roleAppServ;
        public RoleController(IRoleAppService roleAppServ)
        {
            _roleAppServ = roleAppServ ?? throw new ArgumentNullException(nameof(roleAppServ));
        }

        [Route("api/v1/Identity/Role/{id}/Users")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<UserAssignedToRoleDto>> GetUserAssignedToRoleByRoleId(int id) =>
          await this._roleAppServ.GetUserAssignedToRoleByRoleId(id);


        [Route("api/v1/Identity/Role")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<RoleDto> Create([FromBody] RoleDto role) =>
          await this._roleAppServ.AddRoleAsync(role);


        [Route("api/v1/Identity/Role/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<RoleDto> GetRoleByIdAsync([FromRoute]int id) =>
          await this._roleAppServ.GetRoleByIdAsync(id);

        [Route("api/v1/Identity/Roles")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<RoleDto>> GetAllRolesAsync() =>
          await this._roleAppServ.GetAllRolesAsync();


        [Route("api/v1/Identity/Role")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Update([FromBody] RoleDto role) =>
            await this._roleAppServ.UpdateRoleAsync(role);
          

        [Route("api/v1/Identity/Role/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Delete([FromRoute]int id) =>
            await this._roleAppServ.DeleteRoleAsync(id);

    }
}
