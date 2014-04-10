using System;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TaskScheduler.EventBus;
using TaskScheduler.Events;

namespace TaskScheduler.Operations
{
    public class HttpOperation : IOperation
    {
        private class HttpParameters
        {
            public string Url { get; set; }
            public string Verb { get; set; }
        }

        public void Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<HttpParameters>(parameters);
            var client = new WebClient();
            client.Proxy = new WebProxy("192.168.220.249", 3128);
            client.Headers[HttpRequestHeader.ContentType] = "application/json";
            client.UploadString(deserializedParameters.Url, "POST", "{}");
        }
    }
}