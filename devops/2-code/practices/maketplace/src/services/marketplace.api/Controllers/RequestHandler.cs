using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace marketplace.api.Controllers
{
    public static class RequestHandler
    {
        public static async Task<IActionResult> HandleRequest<TRequest>(TRequest request,Func<TRequest,Task> handler,ILogger log)
        {
            try
            {
                log.Debug($"Handling Http request of type {typeof(TRequest).Name}");
                await handler(request);
                return new OkResult();
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error handing the request");
                return new BadRequestObjectResult(new {error=ex.Message,stackTrace=ex.StackTrace });
            }
        }
    }
}