using System;
using System.Collections.Generic;
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

        public ResilienceController(IMightFailResilienceService mightFailResilienceService)
        {
            _mightFailResilienceService = mightFailResilienceService;
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
