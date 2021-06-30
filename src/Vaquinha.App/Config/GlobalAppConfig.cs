using System;

namespace Vaquinha.App.Config
{
    public class GlobalAppConfig
    {
        public int GoalCrowfunding { get; set; }
        public DateTime DeadlineCrowfunding { get; set; }     
       
        public RetryPolicy Polly { get; set; }  
    }

    public class RetryPolicy
    {
        public int QuantityRetry { get; set; }
        public int WaitingInSegs { get; set; }
    }
}