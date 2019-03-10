using System.Net;
using System.Threading;
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
            return _mightFailResilienceService.ShouldNextSucceed() 
                ? SuccessResult()
                : FailResult();
        }

        [HttpGet("waitforit")]
        public ActionResult<ResilienceResult> WaitForIt()
        {
            Thread.Sleep(_waitForItResilienceService.NextWaitTime());
            return SuccessResult();
        }

        private ActionResult<ResilienceResult> SuccessResult()
        {
            return new ResilienceResult();
        }

        private ActionResult FailResult()
        {
            return new StatusCodeResult((int) HttpStatusCode.InternalServerError);
        }
    }
}
