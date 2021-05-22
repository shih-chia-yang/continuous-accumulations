using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications.Contracts;
using marketplace.domain.kernal.commands;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace marketplace.api.Controllers
{
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly ICommandHandler<ClassifiedAds.V1.Create> _created;

        public ClassifiedAdController(ICommandHandler<ClassifiedAds.V1.Create> created)
        {
            _created = created; 
        }

        [HttpPost]
        public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
        {
            await _created.Handle(request);
            return Ok();
        }
    }
}