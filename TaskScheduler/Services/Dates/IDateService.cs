using System;

namespace TaskScheduler.Services.Dates
{
    public interface IDateService
    {
        DateTime Evaluate(DateTime utcNow, string utcRunningTime);

        DateTime NowUtc { get; }
    }
}