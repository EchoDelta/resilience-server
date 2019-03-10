using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResilienceServer.Web.Models
{
    public class FailedResilienceResult : ResilienceResult
    {
        public FailedResilienceResult()
        {
            Timestamp = DateTimeOffset.MinValue;
        }
    }
}
