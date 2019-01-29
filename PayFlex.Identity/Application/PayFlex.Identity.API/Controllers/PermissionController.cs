using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Permission;

 
namespace PayFlex.Identity.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionAppService _permissionAppServ;
        public PermissionController(IPermissionAppService permissionAppServ)
        {
            this._permissionAppServ = permissionAppServ ?? throw new ArgumentNullException(nameof(permissionAppServ));
        }

        [Route("api/v1/Identity/Permission/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PermissionDto> GetPermissionByIdAsync(int id) =>
              await this._permissionAppServ.GetPermissionByIdAsync(id);


        [Route("api/v1/Identity/Permissions")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<PermissionDto>> GetAllPermissionsAsync() =>
            await this._permissionAppServ.GetAllPermissionsAsync();


        [Route("api/v1/Identity/Permission")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<PermissionDto> AddPermission([FromBody] PermissionDto permission) =>
            await this._permissionAppServ.AddPermission(permission);


        [Route("api/v1/Identity/Permission")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Update([FromBody] PermissionDto permission) =>
             await this._permissionAppServ.UpdatePermissionAsync(permission);
        
        
        [Route("api/v1/Identity/Permission/{id}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Delete([FromRoute]int id) =>
            await this._permissionAppServ.DeletePermissionAsync(id);
        

    }
}
