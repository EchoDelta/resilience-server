using System;
using System.Threading;
using Microsoft.Extensions.Options;
using ResilienceServer.Web.Models;
using ResilienceServer.Web.Options;

namespace ResilienceServer.Web.ResilienceServices
{
    public class FragileResilienceService : ResilienceBaseService, IFragileResilienceService
    {
        private readonly double _failRate;
        private readonly double _maxDownTime;
        private readonly double _downTimeCallInc;
        private readonly double _waitMean;
        private readonly double _waitStandardDeviation;

        private readonly Random _random;

        private DateTimeOffset _downTime = DateTimeOffset.MinValue;
        private int _downCallCount = 0;

        public FragileResilienceService(IOptions<ResilienceOptions> resilienceOptions)
        {
            _random = new Random();

            var fragileOptions = resilienceOptions.Value.Fragile;

            _failRate = fragileOptions.FailRate;
            _maxDownTime = fragileOptions.MaxDownTime;
            _downTimeCallInc = fragileOptions.DownTimeCallInc;
            _waitMean = fragileOptions.WaitMeanTime;
            _waitStandardDeviation = fragileOptions.WaitStandardDeviation;
        }

        public ResilienceResult GetResult()
        {
            if (!IsDown())
            {
                if (_random.NextDouble() < _failRate)
                {
                    DownCall();
                    return FailedResult();
                }

                _downCallCount = 0;
                return SuccessfullResult();
            }

            DownCall();
            Thread.Sleep(GetWaitTime());
            return FailedResult();
        }

        private void DownCall()
        {
            _downCallCount++;
            var currentDiffSeconds = Math.Max(0, (_downTime - DateTimeOffset.Now).TotalSeconds);
            _downTime = DateTimeOffset.Now.AddSeconds(Math.Min(_maxDownTime, currentDiffSeconds + _downTimeCallInc * _downCallCount));
        }

        private bool IsDown()
        {
            return (_downTime - DateTimeOffset.Now).Ticks > 0;
        }

        private TimeSpan GetWaitTime()
        {
            var u1 = 1.0 - _random.NextDouble();
            var u2 = 1.0 - _random.NextDouble();
            var waitTime = _waitMean + _downCallCount + _waitStandardDeviation * Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);

            return TimeSpan.FromSeconds(Math.Max(waitTime, 0.0));
        }
    }
}
