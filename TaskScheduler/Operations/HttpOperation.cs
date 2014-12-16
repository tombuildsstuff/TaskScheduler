using System;
using System.Net;
using Newtonsoft.Json;

namespace TaskScheduler.Operations
{
    public class HttpOperation : IOperation
    {
        public OperationResponse Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<HttpParameters>(parameters);
            var client = (HttpWebRequest)WebRequest.Create(deserializedParameters.Url);
            client.Method = deserializedParameters.Verb ?? "POST";

            if (deserializedParameters.TimeoutInSeconds.HasValue)
                client.Timeout = (int) TimeSpan.FromSeconds(deserializedParameters.TimeoutInSeconds.Value).TotalMilliseconds;
            
            client.ContentLength = 0;
            var response = (HttpWebResponse)client.GetResponse();
            return response.StatusCode == HttpStatusCode.OK ? OperationResponse.Finished : OperationResponse.FailedToComplete;
        }

        private class HttpParameters
        {
            public int? TimeoutInSeconds { get; set; }

            public string Url { get; set; }

            public string Verb { get; set; }
        }
    }
}