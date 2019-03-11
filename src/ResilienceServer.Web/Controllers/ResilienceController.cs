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
        private readonly IFragileResilienceService _fragileResilienceService;

        public ResilienceController(IMightFailResilienceService mightFailResilienceService, IWaitForItResilienceService waitForItResilienceService, IFragileResilienceService fragileResilienceService)
        {
            _mightFailResilienceService = mightFailResilienceService;
            _waitForItResilienceService = waitForItResilienceService;
            _fragileResilienceService = fragileResilienceService;
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

        [HttpGet("fragile")]
        public ActionResult<ResilienceResult> Fragile()
        {
            return ResilienceActionResult(_fragileResilienceService.GetResult());
        }

        private ActionResult<ResilienceResult> ResilienceActionResult(ResilienceResult resilienceResult)
        {
            return resilienceResult is FailedResilienceResult
                ? (ActionResult<ResilienceResult>) new StatusCodeResult((int) HttpStatusCode.InternalServerError)
                : resilienceResult;
        }
    }
}
