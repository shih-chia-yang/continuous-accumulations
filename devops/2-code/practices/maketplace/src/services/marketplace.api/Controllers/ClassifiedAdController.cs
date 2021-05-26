using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications;
using marketplace.api.Applications.Contracts;
using marketplace.domain.kernal.commands;
using Microsoft.AspNetCore.Mvc;
using static marketplace.api.Applications.Contracts.ClassifiedAds;

namespace marketplace.api.Controllers
{
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly ICommandHandler<ClassifiedAds.V1.Create> _created;

        private readonly IAppService _service;

        public ClassifiedAdController(
            ICommandHandler<ClassifiedAds.V1.Create> created,
            IAppService service)
        {
            _created = created;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Add (ClassifiedAds.V1.Create request)
        {
            await _service.Handle(request);
            // await _created.Handle(request);
            return Ok();
        }

        [Route("name")]
        [HttpPut]
        public async Task<IActionResult> SetTitle(V1.SetTitle request)
        {
            await _service.Handle(request);
            return Ok();
        }

        [Route("text")]
        [HttpPut]
        public async Task<IActionResult> UpdateText(V1.UpdateText request)
        {
            await _service.Handle(request);
            return Ok();
        }

        [Route("price")]
        [HttpPut]
        public async Task<IActionResult> UpdatePrice(V1.UpdatePrice request)
        {
            await _service.Handle(request);
            return Ok();
        }

        [Route("publish")]
        [HttpPut]
        public async Task<IActionResult> RequestToPublish(V1.RequestToPublish request)
        {
            await _service.Handle(request);
            return Ok();
        }
    }
}