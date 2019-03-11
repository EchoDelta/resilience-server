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
        public FragileOptions Fragile { get; set; }
    }
}
