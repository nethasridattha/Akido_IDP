using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace DTPortal.IDP.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorController : Controller
    {
        private readonly ILogger<ErrorController> _logger;

        public ErrorController(ILogger<ErrorController> logger)
        {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            ViewResult view;
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Sorry, the resource you requested could not be found";
                    _logger.LogWarning($"404 Error Occurred. Path = {statusCodeResult.OriginalPath}" +
                        $"and QueryString = {statusCodeResult.OriginalQueryString}");

                    view = View("NotFound");
                    break;

                default:
                    _logger.LogWarning($"Unknown Error Occurred. Path = {statusCodeResult.OriginalPath}" +
                        $"and QueryString = {statusCodeResult.OriginalQueryString}");

                    view = View("DefaultError");
                    break;
            }
            return view;
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            // Log the exception

            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The path{exceptionDetails.Path} threw an exception " +
                $"{exceptionDetails.Error}");
            ViewBag.ErrorMessage = exceptionDetails;
            return View();
        }
    }
}
