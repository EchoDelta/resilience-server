namespace ResilienceServer.Web.Options
{
    public class FragileOptions
    {
        public double FailRate { get; set; }
        public double MaxDownTime { get; set; }
        public double DownTimeCallInc { get; set; }
        public double WaitMeanTime { get; set; }
        public double WaitStandardDeviation { get; set; }
    }
}