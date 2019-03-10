using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResilienceServer.Web.Options
{
    public class ResilienceOptions
    {
        public MightFailOptions MightFail { get; set; }
        public WaitForItOptions WaitForIt { get; set; }
    }

    public class WaitForItOptions
    {
        public double WaitMean { get; set; }
        public double WaitStandardDeviation { get; set; }
    }
}
