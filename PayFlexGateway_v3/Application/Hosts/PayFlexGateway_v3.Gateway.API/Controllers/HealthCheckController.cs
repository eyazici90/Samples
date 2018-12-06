using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PayFlexGateway_v3.Gateway.API.Controllers
{
    [Route("/")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    { 
        [HttpGet]
        [Route("healthcheck")]
        public IActionResult HealthCheck() => Ok(new { Status = "Healty" });
    }
}
