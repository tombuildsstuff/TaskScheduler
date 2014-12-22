using System;

namespace TaskScheduler
{
    public interface IDateService
    {
        DateTime Evaluate(DateTime utcNow, string utcRunningTime);

        DateTime NowUtc { get; }
    }
}