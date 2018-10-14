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

        [Route("api/Customer/Brands/{id}")]
        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBrandByIdAsync(int id) =>
                 Ok(await this._customerAppServ.GetBrandByIdAsync(id));


        [Route("api/Customer/Brand/ChangeName")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeBrandName([FromBody] BrandDto request) =>
           Ok(await this._customerAppServ.ChangeBrandName(request));

        [Route("api/Customer/ChangeMerchantVknByBrand")]
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeMerchantVknByBrand([FromBody] MerchantDto request) =>
           Ok(await this._customerAppServ.ChangeMerchantVknByBrand(request));


        [Route("api/Customer/Brand")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddNewBrand([FromBody] BrandDto request) =>
            Ok(await this._customerAppServ.AddNewBrand(request));


        [Route("api/Customer/Merchant")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddMerchantToBrand([FromBody] MerchantDto request) =>
           Ok(await this._customerAppServ.AddMerchantToBrand(request));
    }
}