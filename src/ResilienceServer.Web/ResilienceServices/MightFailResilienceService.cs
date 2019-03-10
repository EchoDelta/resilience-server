using System;
using Microsoft.Extensions.Options;
using ResilienceServer.Web.Options;

namespace ResilienceServer.Web.ResilienceServices
{
    public class MightFailResilienceService : IMightFailResilienceService
    {
        private readonly double _failrate;
        private readonly Random _random;

        public MightFailResilienceService(IOptions<ResilienceOptions> resilienceOptions)
        {
            var mightFailOptions = resilienceOptions.Value.MightFail;

            _failrate = mightFailOptions.FailRate;
            _random = new Random();
        }

        public bool ShouldNextSucceed()
        {
            var number = _random.NextDouble();
            return number > _failrate;
        }
    }
}
