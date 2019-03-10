using System;

namespace ResilienceServer.Web.ResilienceServices
{
    public interface IWaitForItResilienceService
    {
        TimeSpan NextWaitTime();
    }
}
