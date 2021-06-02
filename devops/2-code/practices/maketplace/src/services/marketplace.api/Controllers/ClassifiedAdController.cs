using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications;
using marketplace.api.Applications.Contracts;
using marketplace.domain.kernel.commands;
using Microsoft.AspNetCore.Mvc;
using static marketplace.api.Applications.Contracts.ClassifiedAds;
using Serilog;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using marketplace.infrastructure;
using marketplace.api.Registry;

namespace marketplace.api.Controllers
{
    [EnableCors(StartupExtensionMethods.CorsPolicy)]
    [Produces("application/json")]
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly IAppService _service;

        private static ILogger Log = Serilog.Log.ForContext<ClassifiedAdController>();

        public ClassifiedAdController(IAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Add (ClassifiedAds.V1.Create request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);

        [Route("name")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> SetTitle(ClassifiedAds.V1.SetTitle request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);

        [Route("text")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> UpdateText(ClassifiedAds.V1.UpdateText request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);

        [Route("price")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> UpdatePrice(ClassifiedAds.V1.UpdatePrice request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);

        [Route("picture")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> AddPicture(ClassifiedAds.V1.AddPicture request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);

        [Route("resize")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> ResizePicture(ClassifiedAds.V1.ResizePicture request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);

        [Route("publish")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  Task<IActionResult> RequestToPublish(ClassifiedAds.V1.RequestToPublish request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);
    }
}