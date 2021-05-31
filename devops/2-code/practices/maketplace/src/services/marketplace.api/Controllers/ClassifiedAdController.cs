using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications;
using marketplace.api.Applications.Contracts;
using marketplace.domain.kernal.commands;
using Microsoft.AspNetCore.Mvc;
using static marketplace.api.Applications.Contracts.ClassifiedAds;
using Serilog;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;

namespace marketplace.api.Controllers
{
    [EnableCors("CorsPolicy")]
    [Produces("application/json")]
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly ICommandHandler<ClassifiedAds.V1.Create> _created;

        private readonly IAppService _service;

        private static ILogger Log = Serilog.Log.ForContext<ClassifiedAdController>();

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

        [Route("~/api/ad/name")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> SetTitle(V1.SetTitle request)
            => HandleRequest(request, _service.Handle);

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
        private async Task<IActionResult> HandleRequest<T>(T request, Func<T, Task> handler)
        {
            try
            {
                Log.Debug("Handling HTTP request of type {type}", typeof(T).Name);
                await handler(request);
                return Ok();
            }
            catch (Exception e)
            {
                Log.Error(e, "Error handling the request");
                return new BadRequestObjectResult(new {error = e.Message, stackTrace = e.StackTrace});
            }
        }
    }
}