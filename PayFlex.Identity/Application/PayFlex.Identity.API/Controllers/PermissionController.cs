using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Permission;

 
namespace PayFlex.Identity.API.Controllers
{
    [ApiController]
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
        public async Task<IActionResult> GetPermissionByIdAsync(int id) =>
              Ok(await this._permissionAppServ.GetPermissionByIdAsync(id));

        [Route("api/v1/Identity/Permissions")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPermissionsAsync() =>
            Ok(await this._permissionAppServ.GetAllPermissionsAsync());

        [Route("api/v1/Identity/Permission")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddPermission([FromBody] PermissionDto permission) =>
            Ok(await this._permissionAppServ.AddPermission(permission));

        [Route("api/v1/Identity/Permission")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] PermissionDto permission)
        {
             await this._permissionAppServ.UpdatePermissionAsync(permission);
            return Ok(true);
        }
            
    }
}
