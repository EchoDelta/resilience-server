using System;

namespace ResilienceServer.Web.Models
{
    public class ResilienceResult
    {
        public ResilienceResult()
        {
            Timestamp = DateTimeOffset.Now;
        }

        public DateTimeOffset Timestamp { get; set; }
    }
}