using System.Net;
using Microsoft.AspNetCore.Mvc;
using ResilienceServer.Web.Models;
using ResilienceServer.Web.ResilienceServices;

namespace ResilienceServer.Web.Controllers
{
    [Route("resilience")]
    [ApiController]
    public class ResilienceController : ControllerBase
    {
        private readonly IMightFailResilienceService _mightFailResilienceService;
        private readonly IWaitForItResilienceService _waitForItResilienceService;

        public ResilienceController(IMightFailResilienceService mightFailResilienceService, IWaitForItResilienceService waitForItResilienceService)
        {
            _mightFailResilienceService = mightFailResilienceService;
            _waitForItResilienceService = waitForItResilienceService;
        }

        [HttpGet("stable")]
        public ActionResult<ResilienceResult> Stable()
        {
            return new ResilienceResult();
        }

        [HttpGet("mightfail")]
        public ActionResult<ResilienceResult> MightFail()
        {
            return ResilienceActionResult(_mightFailResilienceService.GetResult());
        }

        [HttpGet("waitforit")]
        public ActionResult<ResilienceResult> WaitForIt()
        {
            return ResilienceActionResult(_waitForItResilienceService.GetResult());
        }

        private ActionResult<ResilienceResult> ResilienceActionResult(ResilienceResult resilienceResult)
        {
            return resilienceResult is FailedResilienceResult
                ? (ActionResult<ResilienceResult>) new StatusCodeResult((int) HttpStatusCode.InternalServerError)
                : resilienceResult;
        }
    }
}
