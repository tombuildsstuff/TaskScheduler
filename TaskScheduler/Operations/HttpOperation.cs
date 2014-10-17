using System.Net;
using Newtonsoft.Json;

namespace TaskScheduler.Operations
{
    public class HttpOperation : IOperation
    {
        private class HttpParameters
        {
            public int? Timeout { get; set; }

            public string Url { get; set; }

            public string Verb { get; set; }
        }

        public void Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<HttpParameters>(parameters);
            var client = WebRequest.Create(deserializedParameters.Url);
            client.Method = "POST";
            //client.Proxy = new WebProxy("192.168.220.249", 3128);

            if (deserializedParameters.Timeout.HasValue)
                client.Timeout = deserializedParameters.Timeout.Value;
            
            client.ContentLength = 0;
            client.GetResponse();
        }
    }
}