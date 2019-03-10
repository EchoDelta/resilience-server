﻿using System;
using Microsoft.Extensions.Options;
using ResilienceServer.Web.Options;

namespace ResilienceServer.Web.ResilienceServices
{
    public class WaitForItResilienceService : IWaitForItResilienceService
    {
        private readonly Random _random;
        private readonly double _mean;
        private readonly double _sd;

        public WaitForItResilienceService(IOptions<ResilienceOptions> resilienceOptions)
        {
            var waitForItOptions = resilienceOptions.Value.WaitForIt;

            _mean = waitForItOptions.WaitMean;
            _sd = waitForItOptions.WaitStandardDeviation;
            _random = new Random();
        }

        public TimeSpan NextWaitTime()
        {
            var u1 = 1.0 - _random.NextDouble();
            var u2 = 1.0 - _random.NextDouble();
            var waitTime = _mean + _sd * Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);


            return TimeSpan.FromSeconds(Math.Max(waitTime, 0.0));
        }
    }
}