using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications.Command;
using marketplace.api.Applications.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marketplace.api.Controllers
{
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly ClassifiedAdCreatedCommand _created;

        public ClassifiedAdController(ClassifiedAdCreatedCommand created)
        {
            _created = created;
        }

        [HttpPost]
        public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
        {
            _created.Handle(request);
            return Ok();
        }
    }
}