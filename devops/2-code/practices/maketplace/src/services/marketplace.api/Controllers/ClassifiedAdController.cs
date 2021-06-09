using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using marketplace.api.Applications.Contracts;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using marketplace.api.Registry;
using marketplace.api.Applications.Queries;
using marketplace.api.ViewModels;
using marketplace.api.Applications.Services.ClassifiedAds;

namespace marketplace.api.Controllers
{
    [EnableCors(StartupExtensionMethods.CorsPolicy)]
    [Produces("application/json")]
    [Route("api/ad")]
    [ApiController]
    public class ClassifiedAdController : ControllerBase
    {
        private readonly IClassifiedAdAppService _service;

        private readonly IClassifiedAdQueries _queries;

        private readonly IEnumerable<ClassifiedAdDetailsViewModel> _items;

        private static ILogger Log = Serilog.Log.ForContext<ClassifiedAdController>();

        public ClassifiedAdController(
            IClassifiedAdAppService service,
            IClassifiedAdQueries queries,
            IEnumerable<ClassifiedAdDetailsViewModel> items
        )
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _items = items ?? throw new ArgumentNullException(nameof(items));
        }

        [Route("list")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassifiedAdListItemViewModel>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ClassifiedAdListItemViewModel>> Get(
            [FromQuery]GetPublishedClassifiedAds request)
                => await _queries.Query(request);

        [Route("list/pending")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassifiedAdListItemViewModel>),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ClassifiedAdListItemViewModel>> Get(
            [FromQuery]GetPendingReviewClassifiedAds request)
                => await _queries.Query(request);

        [Route("MyAds/{OwnerId}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ClassifiedAdListItemViewModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IEnumerable<ClassifiedAdListItemViewModel>> 
            GetAsync([FromRoute ]string ownerId,[FromQuery]int page,[FromQuery]int pageSize)
            {
            var request = new GetOwnersClassifiedAd(ownerId,page,pageSize);
            return await _queries.Query(request);
            }

        [Route("{ClassifiedAdId}")]
        [HttpGet]
        [ProducesResponseType(typeof(ClassifiedAdDetailsViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ClassifiedAdDetailsViewModel> GetAsync([FromRoute] GetPublicClassifiedAd request)
        => await _queries.Query(request);

        [HttpGet]
        [ProducesResponseType(typeof(ClassifiedAdDetailsViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Get([FromQuery] GetPublicClassifiedAd request)
        // => await _queries.Query(request);
            => RequestHandler.HandleQuery(()=>_items.Query(request),Log);


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

        [Route("requestpublish")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public  Task<IActionResult> RequestToPublish(ClassifiedAds.V1.RequestToPublish request)
            => RequestHandler.HandleRequest(request, _service.Handle,Log);

        [Route("publish")]
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public Task<IActionResult> Publish(ClassifiedAds.V1.Publish request)
            => RequestHandler.HandleRequest(request, _service.Handle, Log);
    }
}