using System;

namespace TaskScheduler
{
    public class StandardDateTimeProvider : IDateTimeProvider
    {
        public DateTime NowUtc
        {
            get
            {
                return DateTime.UtcNow;
            }
        }
    }
}