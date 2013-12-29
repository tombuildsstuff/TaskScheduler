using System;

namespace TaskScheduler
{
    public interface ITimeSpanEvaluator
    {
        DateTime Evaluate(DateTime utcNow, string utcRunningTime);
    }
}