using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CustomerSample.Application.Abstractions;
using CustomerSample.Common.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerSample.API.Host.Controllers
{
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly ICustomerAppService _customerAppServ;
        public BrandController(ICustomerAppService customerAppServ)
        {
            _customerAppServ = customerAppServ ?? throw new ArgumentNullException(nameof(customerAppServ));
        }

        [Route("api/v1/Customer/Brand/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBrandByIdAsync(int id) =>
             Ok(await this._customerAppServ.GetBrandByIdAsync(id));

        [Route("api/v1/Customer/Brand")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update([FromBody] BrandDto brand) =>
          Ok(await this._customerAppServ.AddNewBrand(brand));
    }
}