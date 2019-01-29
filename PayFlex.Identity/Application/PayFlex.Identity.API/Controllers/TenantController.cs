using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayFlex.Identity.Application.Contracts.Services;
using PayFlex.Identity.Shared.Dtos.Tenant;

 
namespace PayFlex.Identity.API.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TenantController : ControllerBase
    {
        private readonly ITenanAppService _tenantAppServ;
        public TenantController(ITenanAppService tenantAppServ)
        {
            _tenantAppServ = tenantAppServ ?? throw new ArgumentNullException(nameof(tenantAppServ));
        }

        [Route("api/v1/Identity/Tenant/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<TenantDto> GetTenantByIdAsync(int id) =>
          await this._tenantAppServ.GetTenantByIdAsync(id);

        [Route("api/v1/Identity/Tenants")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IEnumerable<TenantDto>> GetAllTenantsAsync() =>
          await this._tenantAppServ.GetAllTenantsAsync();

        [Route("api/v1/Identity/Tenant")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<TenantDto> Create([FromBody] TenantDto tenant) =>
            await this._tenantAppServ.AddTenantAsync(tenant);
           

        [Route("api/v1/Identity/Tenant")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Update([FromBody] TenantDto tenant) =>
            await this._tenantAppServ.UpdateTenantAsync(tenant);
            
    }
}
