namespace ResilienceServer.Web.ResilienceServices
{
    public interface IMightFailResilienceService
    {
        bool ShouldNextSucceed();
    }
}