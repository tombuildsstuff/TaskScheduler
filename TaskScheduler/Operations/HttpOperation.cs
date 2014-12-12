using System.Net;
using Newtonsoft.Json;

namespace TaskScheduler.Operations
{
    public class HttpOperation : IOperation
    {
        public ResponseStatus Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<HttpParameters>(parameters);
            var client = (HttpWebRequest)WebRequest.Create(deserializedParameters.Url);
            client.Method = deserializedParameters.Verb ?? "POST";

            if (deserializedParameters.Timeout.HasValue)
                client.Timeout = deserializedParameters.Timeout.Value;
            
            client.ContentLength = 0;
            var response = (HttpWebResponse)client.GetResponse();
            return response.StatusCode == HttpStatusCode.OK ? ResponseStatus.Finished : ResponseStatus.FailedToComplete;
        }

        private class HttpParameters
        {
            public int? Timeout { get; set; }

            public string Url { get; set; }

            public string Verb { get; set; }
        }
    }
}