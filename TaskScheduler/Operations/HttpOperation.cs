using System;
using System.Configuration;
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
            var client = WebRequest.Create(deserializedParameters.Url);
            client.Method = "POST";
            //client.Proxy = new WebProxy("192.168.220.249", 3128);
            client.ContentLength = 0;
            client.GetResponse();
        }
    }
}