using ResilienceServer.Web.Models;

namespace ResilienceServer.Web.ResilienceServices
{
    public class ResilienceBaseService
    {
        protected ResilienceResult SuccessfullResult()
        {
            return new ResilienceResult();
        }

        protected ResilienceResult FailedResult()
        {
            return new FailedResilienceResult();
        }
    }
}