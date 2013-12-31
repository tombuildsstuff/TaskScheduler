using System;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;

namespace TaskScheduler.Operations
{
    public class PostOperation : IOperation
    {
        private class PostParameters
        {
            public string Url { get; set; }
            public int Timeout { get; set; }
            public string Body { get; set; }
        }

        public void Execute(string parameters)
        {
            var deserializedParameters = JsonConvert.DeserializeObject<PostParameters>(parameters);
            var client = WebRequest.Create(deserializedParameters.Url);
            
            var buf = string.IsNullOrEmpty(deserializedParameters.Body) ? new byte[0] : Encoding.UTF8.GetBytes(deserializedParameters.Body);

            client.Method = "POST";
            client.ContentType = "text/json";
            client.ContentLength = buf.Length;
            client.GetRequestStream().Write(buf, 0, buf.Length);
            client.Timeout = deserializedParameters.Timeout;
            client.GetResponse();
        }
    }
}