using System;

namespace TaskScheduler
{
    public interface IDateTimeProvider
    {
        DateTime NowUtc { get; }
    }
}