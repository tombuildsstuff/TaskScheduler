using System;
using System.Text.RegularExpressions;

namespace TaskScheduler
{
    public class TimeSpanEvaluator : ITimeSpanEvaluator
    {
        public DateTime Evaluate(DateTime utcNow, string utcRunningTime)
        {
            var regex = new Regex(@"(monthly on) (\d+)(.+) at (\d+):(\d+)", RegexOptions.IgnoreCase);
            var result = regex.Match(utcRunningTime);

            var nextEvent = result.Success
                ? ParseMonthlyEvent(utcNow, result)
                : ParseDailyEvent(utcNow, utcRunningTime);
            return nextEvent;
        }

        private static DateTime ParseDailyEvent(DateTime utcNow, string utcRunningTime)
        {
            var hours = int.Parse(utcRunningTime.Split(':')[0]);
            var minutes = int.Parse(utcRunningTime.Split(':')[1]);
            var nextEvent = utcNow.Date.AddHours(hours).AddMinutes(minutes);
            if (nextEvent <= utcNow)
            {
                nextEvent = nextEvent.AddDays(1);
            }
            return nextEvent;
        }

        private static DateTime ParseMonthlyEvent(DateTime utcNow, Match result)
        {
            var hours = int.Parse(result.Groups[4].Value);
            var minutes = int.Parse(result.Groups[5].Value);
            var day = int.Parse(result.Groups[2].Value);
            var datetime = string.Format("{0}-{1}-{2}T{3}:{4}:00", utcNow.Year, utcNow.Month, day.ToString("00"),
                hours.ToString("00"), minutes.ToString("00"));
            var nextEvent = DateTime.Parse(datetime);
            if (nextEvent < utcNow) nextEvent = nextEvent.AddMonths(1);
            return nextEvent;
        }
    }
}