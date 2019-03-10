using System;
using Microsoft.Extensions.Options;
using ResilienceServer.Web.Models;
using ResilienceServer.Web.Options;

namespace ResilienceServer.Web.ResilienceServices
{
    public class MightFailResilienceService : ResilienceBaseService, IMightFailResilienceService
    {
        private readonly double _failrate;
        private readonly Random _random;

        public MightFailResilienceService(IOptions<ResilienceOptions> resilienceOptions)
        {
            var mightFailOptions = resilienceOptions.Value.MightFail;

            _failrate = mightFailOptions.FailRate;
            _random = new Random();
        }

        public ResilienceResult GetResult()
        {
            return ShouldSucceed() ? SuccessfullResult() : FailedResult();
        }

        private bool ShouldSucceed()
        {
            var number = _random.NextDouble();
            return number > _failrate;
        }
    }
}
