using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
 
namespace PayFlex.Identity.API.Controllers
{ 
    [Route("/")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        [HttpGet]
        [Route("healthcheck")]
        public IActionResult Ping() => Ok(new { Status = "Healty" });
    }
}
