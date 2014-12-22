using System;

namespace TaskScheduler.Entities
{
    public class ErrorEntry
    {
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime DateTime { get; set; }
    }
}