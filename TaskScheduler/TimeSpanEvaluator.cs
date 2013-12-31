using System;
using System.Text.RegularExpressions;

namespace TaskScheduler
{
    public class TimeSpanEvaluator : ITimeSpanEvaluator
    {
        public DateTime Evaluate(DateTime utcNow, string utcRunningTime)
        {
            var monthlyRegex = new Regex(@"(monthly on) (\d+)(.+) at (\d+):(\d+)", RegexOptions.IgnoreCase);
            var result = monthlyRegex.Match(utcRunningTime);
            if (result.Success) return ParseMonthlyEvent(utcNow, result);
            var dailyRegex = new Regex(@"(daily at) (\d+):(\d+)", RegexOptions.IgnoreCase);
            result = dailyRegex.Match(utcRunningTime);
            return ParseDailyEvent(utcNow, result);
      
        }

        private static DateTime ParseDailyEvent(DateTime utcNow, Match utcRunningTime)
        {
            var hours = int.Parse(utcRunningTime.Groups[2].Value);
            var minutes = int.Parse(utcRunningTime.Groups[3].Value);
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