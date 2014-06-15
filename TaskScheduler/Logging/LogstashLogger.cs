using System;
using System.Collections.Generic;

namespace TaskScheduler.Logging
{
    public class ErrorLogstashLog : LogStashLog
    {
        public Exception Exception { get; set; }
        public Dictionary<string,object> AdditionalData { get; set; }

        public override string Type
        {
            get { return "Error"; }
        }
    }
}