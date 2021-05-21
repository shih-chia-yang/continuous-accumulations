using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marketplace.api.Controllers
{
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        public ClassifiedAdController()
        {
            
        }

        [HttpPost]
        public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
        {
            return Ok();
        }
    }
}