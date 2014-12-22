using System;
using Newtonsoft.Json;

namespace TaskScheduler.Logging.Messages
{
    public abstract class LogStashLog
    {
        [JsonProperty("type")]
        public string Type
        {
            get { return string.Format("TaskScheduler-{0}-{1}", LogName, Version).ToLower(); }
        }

        [JsonProperty("logmessage")]
        public abstract string Message { get; }

        [JsonProperty("host")]
        public string Host { get; set; }

        [JsonProperty("logname")]
        protected abstract string LogName { get; }

        [JsonProperty("@timestamp")]
        public DateTime TimeStamp { get; set; }

        [JsonProperty("version")]
        protected string Version
        {
            get { return "1"; }
        }
    }
}