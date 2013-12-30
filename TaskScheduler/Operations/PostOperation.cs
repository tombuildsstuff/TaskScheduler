using System.Net;
using Newtonsoft.Json;

namespace TaskScheduler.Operations
{
    public class PostOperation : IOperation
    {
        internal class PostParameters
        {
            public string Url { get; set; }
            public int Timeout { get; set; }
        }

        public void Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<PostParameters>(parameters);
            var client = WebRequest.Create(deserializedParameters.Url);
            client.Timeout = deserializedParameters.Timeout;
            client.GetResponse();
        }
    }
}